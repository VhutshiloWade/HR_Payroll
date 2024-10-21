using HR_Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Interfaces.Repositories
{
    public interface IPayrollRepository
    {
        Task<IEnumerable<PayrollRecord>> GetAllAsync();
        Task<PayrollRecord?> GetByIdAsync(int id);
        Task<IEnumerable<PayrollRecord>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<PayrollRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<PayrollRecord> AddAsync(PayrollRecord payrollRecord);
        Task UpdateAsync(PayrollRecord payrollRecord);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<IEnumerable<PayrollDeduction>> GetDeductionsForPayrollAsync(int payrollRecordId);
        Task<IEnumerable<PayrollBenefit>> GetBenefitsForPayrollAsync(int payrollRecordId);
        Task<PayrollRecord?> GetByEmployeeAndPeriodAsync(int employeeId, DateTime startDate, DateTime endDate);
    }
}
