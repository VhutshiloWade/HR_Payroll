using HR_Payroll.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Interfaces.Repositories
{
    public interface IDepartmentRepository
    {
        
            Task<IEnumerable<Department>> GetAllAsync();
            Task<Department?> GetByIdAsync(int id);
            Task<Department> AddAsync(Department department);
            Task UpdateAsync(Department department);
            Task DeleteAsync(int id);
            Task<bool> ExistsAsync(int id);
            Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId);
        
    }
}
