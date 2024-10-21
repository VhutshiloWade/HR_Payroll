using HR_Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Interfaces.Repositories
{
    public interface ITimeOffRepository
    {
        Task<IEnumerable<TimeOffRequest>> GetAllAsync();
        Task<TimeOffRequest?> GetByIdAsync(int id);
        Task<IEnumerable<TimeOffRequest>> GetByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<TimeOffRequest>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<TimeOffRequest> AddAsync(TimeOffRequest timeOffRequest);
        Task UpdateAsync(TimeOffRequest timeOffRequest);
        Task DeleteAsync(int id);
        Task<TimeOffBalance?> GetTimeOffBalanceAsync(int employeeId);
        Task UpdateTimeOffBalanceAsync(TimeOffBalance timeOffBalance);
    }
}
