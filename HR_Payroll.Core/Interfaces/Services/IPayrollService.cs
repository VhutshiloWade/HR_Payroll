using HR_Payroll.Core.Entities;
using HR_Payroll.Shared.DTOs.PayrollDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Interfaces.Services
{
    public interface IPayrollService
    {
        /* Task<IEnumerable<PayrollRecord>> GetAllPayrollRecordsAsync();
         Task<PayrollRecord> GetPayrollRecordByIdAsync(int id);
         Task<IEnumerable<PayrollRecord>> GetPayrollRecordsByEmployeeIdAsync(int employeeId);
         Task<IEnumerable<PayrollRecord>> GetPayrollRecordsByDateRangeAsync(DateTime startDate, DateTime endDate);
         Task<PayrollRecord> CreatePayrollRecordAsync(PayrollRecord payrollRecord);
         Task UpdatePayrollRecordAsync(PayrollRecord payrollRecord);
         Task DeletePayrollRecordAsync(int id);
         Task<IEnumerable<PayrollDeduction>> GetDeductionsForPayrollAsync(int payrollRecordId);
         Task<IEnumerable<PayrollBenefit>> GetBenefitsForPayrollAsync(int payrollRecordId);*/

        Task<IEnumerable<PayrollRecord>> GetAllPayrollRecordsAsync(int page, int pageSize);
        Task<PayrollRecord> GetPayrollRecordByIdAsync(int id);
        Task<PayrollRecord> CreatePayrollRecordAsync(PayrollRecord payrollRecord);
        Task UpdatePayrollRecordAsync(PayrollRecord payrollRecord);
        Task DeletePayrollRecordAsync(int id);
        Task<PayslipDto> GeneratePayslipAsync(GeneratePayslipDto generatePayslipDto);
        Task<PayrollProcessingResultDto> ProcessPayrollAsync(ProcessPayrollDto processPayrollDto);
        Task<PayrollRecord> ApplyPayrollAdjustmentAsync(PayrollAdjustmentDto payrollAdjustmentDto);
        Task<PayrollSummaryDto> GetPayrollSummaryAsync(DateTime startDate, DateTime endDate);
    }
}
