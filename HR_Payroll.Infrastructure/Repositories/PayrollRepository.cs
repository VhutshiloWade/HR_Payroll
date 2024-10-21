using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Repositories;
using HR_Payroll.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Infrastructure.Repositories
{
    public class PayrollRepository : IPayrollRepository
    {
        private readonly ApplicationDbContext _context;

        public PayrollRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PayrollRecord>> GetAllAsync()
        {
            return await _context.PayrollRecords.ToListAsync();
        }

        public async Task<PayrollRecord?> GetByIdAsync(int id)
        {
            return await _context.PayrollRecords.FindAsync(id);
        }

        public async Task<IEnumerable<PayrollRecord>> GetByEmployeeIdAsync(int employeeId)
        {
            return await _context.PayrollRecords
                .Where(p => p.EmployeeId == employeeId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PayrollRecord>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.PayrollRecords
                .Where(p => p.PayPeriodStart >= startDate && p.PayPeriodEnd <= endDate)
                .ToListAsync();
        }

        public async Task<PayrollRecord> AddAsync(PayrollRecord payrollRecord)
        {
            await _context.PayrollRecords.AddAsync(payrollRecord);
            await _context.SaveChangesAsync();
            return payrollRecord;
        }

        public async Task UpdateAsync(PayrollRecord payrollRecord)
        {
            _context.Entry(payrollRecord).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var payrollRecord = await _context.PayrollRecords.FindAsync(id);
            if (payrollRecord != null)
            {
                _context.PayrollRecords.Remove(payrollRecord);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.PayrollRecords.AnyAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<PayrollDeduction>> GetDeductionsForPayrollAsync(int payrollRecordId)
        {
            return await _context.PayrollDeductions
                .Where(d => d.PayrollRecordId == payrollRecordId)
                .ToListAsync();
        }

        public async Task<IEnumerable<PayrollBenefit>> GetBenefitsForPayrollAsync(int payrollRecordId)
        {
            return await _context.PayrollBenefits
                .Where(b => b.PayrollRecordId == payrollRecordId)
                .ToListAsync();
        }
        public async Task<PayrollRecord?> GetByEmployeeAndPeriodAsync(int employeeId, DateTime startDate, DateTime endDate)
        {
            return await _context.PayrollRecords
                .FirstOrDefaultAsync(pr => pr.EmployeeId == employeeId &&
                                           pr.PayPeriodStart >= startDate &&
                                           pr.PayPeriodEnd <= endDate);
        }
    }
}
