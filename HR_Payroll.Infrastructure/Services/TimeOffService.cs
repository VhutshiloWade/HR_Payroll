using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Repositories;
using HR_Payroll.Core.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Infrastructure.Services
{
    public class TimeOffService : ITimeOffService
    {
        private readonly ITimeOffRepository _timeOffRepository;

        public TimeOffService(ITimeOffRepository timeOffRepository)
        {
            _timeOffRepository = timeOffRepository;
        }

        public async Task<IEnumerable<TimeOffRequest>> GetAllTimeOffRequestsAsync()
        {
            return await _timeOffRepository.GetAllAsync();
        }

        public async Task<TimeOffRequest> GetTimeOffRequestByIdAsync(int id)
        {
            var timeOffRequest = await _timeOffRepository.GetByIdAsync(id);
            if (timeOffRequest == null)
            {
                throw new KeyNotFoundException($"TimeOffRequest with ID {id} not found.");
            }
            return timeOffRequest;
        }

        public async Task<IEnumerable<TimeOffRequest>> GetTimeOffRequestsByEmployeeIdAsync(int employeeId)
        {
            return await _timeOffRepository.GetByEmployeeIdAsync(employeeId);
        }

        public async Task<IEnumerable<TimeOffRequest>> GetTimeOffRequestsByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _timeOffRepository.GetByDateRangeAsync(startDate, endDate);
        }

        public async Task<TimeOffRequest> CreateTimeOffRequestAsync(TimeOffRequest timeOffRequest)
        {
            return await _timeOffRepository.AddAsync(timeOffRequest);
        }

        public async Task UpdateTimeOffRequestAsync(TimeOffRequest timeOffRequest)
        {
            await _timeOffRepository.UpdateAsync(timeOffRequest);
        }

        public async Task DeleteTimeOffRequestAsync(int id)
        {
            await _timeOffRepository.DeleteAsync(id);
        }

        public async Task<TimeOffBalance> GetTimeOffBalanceAsync(int employeeId)
        {
            var timeOffBalance = await _timeOffRepository.GetTimeOffBalanceAsync(employeeId);
            if (timeOffBalance == null)
            {
                throw new KeyNotFoundException($"TimeOffBalance for employee with ID {employeeId} not found.");
            }
            return timeOffBalance;
        }

        public async Task UpdateTimeOffBalanceAsync(TimeOffBalance timeOffBalance)
        {
            await _timeOffRepository.UpdateTimeOffBalanceAsync(timeOffBalance);
        }
    }
}
