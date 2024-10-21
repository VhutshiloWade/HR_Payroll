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
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department> GetDepartmentByIdAsync(int id)
        {
            var department = await _departmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }
            return department;
        }

        public async Task<Department> CreateDepartmentAsync(Department department)
        {
            return await _departmentRepository.AddAsync(department);
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            if (!await _departmentRepository.ExistsAsync(department.Id))
            {
                throw new KeyNotFoundException($"Department with ID {department.Id} not found.");
            }
            await _departmentRepository.UpdateAsync(department);
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            if (!await _departmentRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Department with ID {id} not found.");
            }
            await _departmentRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            if (!await _departmentRepository.ExistsAsync(departmentId))
            {
                throw new KeyNotFoundException($"Department with ID {departmentId} not found.");
            }
            return await _departmentRepository.GetEmployeesByDepartmentAsync(departmentId);
        }
    }
}
