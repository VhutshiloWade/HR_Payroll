using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Repositories;
using HR_Payroll.Core.Interfaces.Services;
using HR_Payroll.Infrastructure.Repositories;
using HR_Payroll.Shared.DTOs.PayrollDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Infrastructure.Services
{
    public class PayrollService : IPayrollService
    {
        private readonly IPayrollRepository _payrollRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public PayrollService(IPayrollRepository payrollRepository, IEmployeeRepository employeeRepository)
        {
            _payrollRepository = payrollRepository;
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<PayrollRecord>> GetAllPayrollRecordsAsync(int page, int pageSize)
        {
            return await _payrollRepository.GetAllAsync();
        }

        public async Task<PayrollRecord> GetPayrollRecordByIdAsync(int id)
        {
            var payrollRecord = await _payrollRepository.GetByIdAsync(id);
            if (payrollRecord == null)
            {
                throw new KeyNotFoundException($"PayrollRecord with ID {id} not found.");
            }
            return payrollRecord;
        }

        public async Task<IEnumerable<PayrollRecord>> GetPayrollRecordsByEmployeeIdAsync(int employeeId)
        {
            return await _payrollRepository.GetByEmployeeIdAsync(employeeId);
        }

        public async Task<IEnumerable<PayrollRecord>> GetPayrollRecordsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _payrollRepository.GetByDateRangeAsync(startDate, endDate);
        }

        public async Task<PayrollRecord> CreatePayrollRecordAsync(PayrollRecord payrollRecord)
        {
            return await _payrollRepository.AddAsync(payrollRecord);
        }

        public async Task UpdatePayrollRecordAsync(PayrollRecord payrollRecord)
        {
            if (!await _payrollRepository.ExistsAsync(payrollRecord.Id))
            {
                throw new KeyNotFoundException($"PayrollRecord with ID {payrollRecord.Id} not found.");
            }
            await _payrollRepository.UpdateAsync(payrollRecord);
        }

        public async Task DeletePayrollRecordAsync(int id)
        {
            if (!await _payrollRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"PayrollRecord with ID {id} not found.");
            }
            await _payrollRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<PayrollDeduction>> GetDeductionsForPayrollAsync(int payrollRecordId)
        {
            return await _payrollRepository.GetDeductionsForPayrollAsync(payrollRecordId);
        }

        public async Task<IEnumerable<PayrollBenefit>> GetBenefitsForPayrollAsync(int payrollRecordId)
        {
            return await _payrollRepository.GetBenefitsForPayrollAsync(payrollRecordId);
        }

        public async Task<PayslipDto> GeneratePayslipAsync(GeneratePayslipDto generatePayslipDto)
        {
            var employee = await _employeeRepository.GetByIdAsync(generatePayslipDto.EmployeeId);
            if (employee == null)
                throw new KeyNotFoundException($"Employee with ID {generatePayslipDto.EmployeeId} not found.");

            var payrollRecord = await _payrollRepository.GetByEmployeeAndPeriodAsync(
                generatePayslipDto.EmployeeId,
                generatePayslipDto.PayPeriodStart,
                generatePayslipDto.PayPeriodEnd);

            if (payrollRecord == null)
                throw new KeyNotFoundException("Payroll record not found for the specified period.");

            // Assuming PayrollRecord entity includes Deductions and Benefits
            var payslip = new PayslipDto
            {
                EmployeeId = employee.Id,
                EmployeeName = $"{employee.FirstName} {employee.LastName}",
                EmployeeNumber = employee.EmployeeNumber,
                PayPeriodStart = payrollRecord.PayPeriodStart,
                PayPeriodEnd = payrollRecord.PayPeriodEnd,
                GrossPay = payrollRecord.GrossPay,
                NetPay = payrollRecord.NetPay,
                TaxWithheld = payrollRecord.TaxWithheld,
                Deductions = payrollRecord.Deductions.Select(d => new PayslipDeductionDto
                {
                    DeductionType = d.DeductionType,
                    Amount = d.Amount
                }).ToList(),
                Benefits = payrollRecord.Benefits.Select(b => new PayslipBenefitDto
                {
                    BenefitType = b.BenefitType,
                    Amount = b.Amount
                }).ToList(),
                TotalDeductions = payrollRecord.Deductions.Sum(d => d.Amount),
                TotalBenefits = payrollRecord.Benefits.Sum(b => b.Amount),
                PaymentMethod = employee.PaymentMethod,
                BankAccountNumber = !string.IsNullOrEmpty(employee.BankAccountNumber)
                ? employee.BankAccountNumber.Substring(Math.Max(0, employee.BankAccountNumber.Length - 4))
                : string.Empty,
                PaymentDate = payrollRecord.PaymentDate
            };

            return payslip;
        }

        public async Task<PayrollProcessingResultDto> ProcessPayrollAsync(ProcessPayrollDto processPayrollDto)
        {
            IEnumerable<Employee> employees;
            if (processPayrollDto.EmployeeIds != null && processPayrollDto.EmployeeIds.Any())
            {
                var employeeTasks = processPayrollDto.EmployeeIds.Select(id => _employeeRepository.GetByIdAsync(id));
                var employeeResults = await Task.WhenAll(employeeTasks);
                employees = employeeResults.Where(e => e != null).Cast<Employee>().ToList();
            }
            else
            {
                employees = await _employeeRepository.GetAllAsync();
                if (processPayrollDto.DepartmentId.HasValue)
                {
                    employees = employees.Where(e => e.DepartmentId == processPayrollDto.DepartmentId.Value);
                }
            }

            var result = new PayrollProcessingResultDto
            {
                PayPeriodStart = processPayrollDto.PayPeriodStart,
                PayPeriodEnd = processPayrollDto.PayPeriodEnd,
                TotalEmployeesProcessed = 0,
                TotalGrossPay = 0,
                TotalNetPay = 0,
                TotalTaxWithheld = 0,
                Errors = new List<PayrollProcessingErrorDto>()
            };

            foreach (var employee in employees)
            {
                try
                {
                    var payrollRecord = await CalculatePayrollForEmployee(employee, processPayrollDto.PayPeriodStart, processPayrollDto.PayPeriodEnd);
                    await _payrollRepository.AddAsync(payrollRecord);

                    result.TotalEmployeesProcessed++;
                    result.TotalGrossPay += payrollRecord.GrossPay;
                    result.TotalNetPay += payrollRecord.NetPay;
                    result.TotalTaxWithheld += payrollRecord.TaxWithheld;
                }
                catch (Exception ex)
                {
                    result.Errors.Add(new PayrollProcessingErrorDto
                    {
                        EmployeeId = employee.Id,
                        EmployeeName = $"{employee.FirstName} {employee.LastName}",
                        ErrorMessage = ex.Message
                    });
                }
            }

            return result;
        }

        private Task<PayrollRecord> CalculatePayrollForEmployee(Employee employee, DateTime payPeriodStart, DateTime payPeriodEnd)
        {
            // Implement payroll calculation logic here
            // This is a placeholder implementation
            var workDays = (payPeriodEnd - payPeriodStart).Days + 1;
            var grossPay = (employee.CurrentSalary / 365) * workDays;
            var taxRate = 0.2m; // This should be calculated based on tax brackets
            var taxWithheld = grossPay * taxRate;
            var netPay = grossPay - taxWithheld;

            return Task.FromResult(new PayrollRecord
            {
                EmployeeId = employee.Id,
                PayPeriodStart = payPeriodStart,
                PayPeriodEnd = payPeriodEnd,
                GrossPay = grossPay,
                NetPay = netPay,
                TaxWithheld = taxWithheld,
            });
        }

        public async Task<PayrollRecord> ApplyPayrollAdjustmentAsync(PayrollAdjustmentDto payrollAdjustmentDto)
        {
            var payrollRecord = await _payrollRepository.GetByEmployeeAndPeriodAsync(
                payrollAdjustmentDto.EmployeeId,
                payrollAdjustmentDto.PayPeriodStart,
                payrollAdjustmentDto.PayPeriodEnd);

            if (payrollRecord == null)
                throw new KeyNotFoundException("Payroll record not found for the specified period.");

            payrollRecord.GrossPay += payrollAdjustmentDto.AdjustmentAmount;

            // Recalculate tax and net pay
            var taxRate = 0.2m; // This should be calculated based on tax brackets
            var additionalTax = payrollAdjustmentDto.AdjustmentAmount * taxRate;
            payrollRecord.TaxWithheld += additionalTax;
            payrollRecord.NetPay += payrollAdjustmentDto.AdjustmentAmount - additionalTax;

            // Add an adjustment record
            payrollRecord.Adjustments.Add(new SalaryAdjustment
            {
                Amount = payrollAdjustmentDto.AdjustmentAmount,
                Reason = payrollAdjustmentDto.AdjustmentReason,
                EffectiveDate = DateTime.UtcNow
            });

            await _payrollRepository.UpdateAsync(payrollRecord);

            return payrollRecord;
        }

        public async Task<PayrollSummaryDto> GetPayrollSummaryAsync(DateTime startDate, DateTime endDate)
        {
            var payrollRecords = await _payrollRepository.GetByDateRangeAsync(startDate, endDate);

            var summary = new PayrollSummaryDto
            {
                StartDate = startDate,
                EndDate = endDate,
                TotalGrossPay = payrollRecords.Sum(pr => pr.GrossPay),
                TotalNetPay = payrollRecords.Sum(pr => pr.NetPay),
                TotalTaxWithheld = payrollRecords.Sum(pr => pr.TaxWithheld),
                EmployeeCount = payrollRecords.Select(pr => pr.EmployeeId).Distinct().Count(),
                DepartmentSummaries = await GetDepartmentSummaries(payrollRecords)
            };

            return summary;
        }

        private async Task<List<DepartmentPayrollSummaryDto>> GetDepartmentSummaries(IEnumerable<PayrollRecord> payrollRecords)
        {
            var employeeIds = payrollRecords.Select(pr => pr.EmployeeId).Distinct();
            var employees = await Task.WhenAll(employeeIds.Select(id => _employeeRepository.GetByIdAsync(id)));

            var departmentSummaries = payrollRecords
                .GroupJoin(employees,
                    pr => pr.EmployeeId,
                    e => e?.Id,
                    (pr, e) => new { PayrollRecord = pr, Employee = e.FirstOrDefault() })
                .GroupBy(x => x.Employee?.DepartmentId)
                .Select(g => new DepartmentPayrollSummaryDto
                {
                    DepartmentId = g.Key ?? 0,
                    DepartmentName = g.FirstOrDefault()?.Employee?.Department?.Name ?? "Unknown",
                    TotalGrossPay = g.Sum(x => x.PayrollRecord.GrossPay),
                    TotalNetPay = g.Sum(x => x.PayrollRecord.NetPay),
                    TotalTaxWithheld = g.Sum(x => x.PayrollRecord.TaxWithheld),
                    EmployeeCount = g.Select(x => x.PayrollRecord.EmployeeId).Distinct().Count()
                })
                .ToList();

            return departmentSummaries;
        }
    }
}
