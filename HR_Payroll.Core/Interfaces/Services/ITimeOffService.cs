using HR_Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Interfaces.Services
{
    public interface ITimeOffService
    {
        Task<IEnumerable<TimeOffRequest>> GetAllTimeOffRequestsAsync();
        Task<TimeOffRequest> GetTimeOffRequestByIdAsync(int id);
        Task<IEnumerable<TimeOffRequest>> GetTimeOffRequestsByEmployeeIdAsync(int employeeId);
        Task<IEnumerable<TimeOffRequest>> GetTimeOffRequestsByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<TimeOffRequest> CreateTimeOffRequestAsync(TimeOffRequest timeOffRequest);
        Task UpdateTimeOffRequestAsync(TimeOffRequest timeOffRequest);
        Task DeleteTimeOffRequestAsync(int id);
        Task<TimeOffBalance> GetTimeOffBalanceAsync(int employeeId);
        Task UpdateTimeOffBalanceAsync(TimeOffBalance timeOffBalance);
    }
}
