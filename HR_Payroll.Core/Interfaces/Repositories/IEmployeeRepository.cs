using HR_Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task<Employee?> GetByEmailAsync(string email);
        Task<IEnumerable<Employee>> GetByDepartmentAsync(int departmentId);
        Task<Employee> AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
        Task<int> GetTotalCountAsync();
    }
}
