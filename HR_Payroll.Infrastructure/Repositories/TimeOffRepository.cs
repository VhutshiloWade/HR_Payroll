using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Repositories;
using HR_Payroll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HR_Payroll.Infrastructure.Repositories
{
    public class TimeOffRepository : ITimeOffRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeOffRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TimeOffRequest>> GetAllAsync()
        {
            return await _context.TimeOffRequests.ToListAsync();
        }

        public async Task<TimeOffRequest?> GetByIdAsync(int id)
        {
            return await _context.TimeOffRequests.FindAsync(id);
        }

        public async Task<IEnumerable<TimeOffRequest>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.TimeOffRequests
                .Where(t => t.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<TimeOffRequest>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.TimeOffRequests
                .Where(t => t.StartDate >= startDate && t.EndDate <= endDate)
                .ToListAsync();
        }

        public async Task<TimeOffRequest> AddAsync(TimeOffRequest timeOffRequest)
        {
            await _context.TimeOffRequests.AddAsync(timeOffRequest);
            await _context.SaveChangesAsync();
            return timeOffRequest;
        }

        public async Task UpdateAsync(TimeOffRequest timeOffRequest)
        {
            _context.Entry(timeOffRequest).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var timeOffRequest = await _context.TimeOffRequests.FindAsync(id);
            if (timeOffRequest != null)
            {
                _context.TimeOffRequests.Remove(timeOffRequest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<TimeOffBalance?> GetTimeOffBalanceAsync(int employeeId)
        {
            return await _context.TimeOffBalances
                .FirstOrDefaultAsync(t => t.EmployeeId == employeeId);
        }

        public async Task UpdateTimeOffBalanceAsync(TimeOffBalance timeOffBalance)
        {
            _context.Entry(timeOffBalance).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
