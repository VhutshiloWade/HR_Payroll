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
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _employeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            return employee;
        }

        public async Task<Employee> CreateEmployeeAsync(Employee employee)
        {
            if (!await IsEmailUniqueAsync(employee.Email))
            {
                throw new InvalidOperationException("Email address is already in use.");
            }
            return await _employeeRepository.AddAsync(employee);
        }

        public async Task UpdateEmployeeAsync(Employee employee)
        {
            if (!await _employeeRepository.ExistsAsync(employee.Id))
            {
                throw new KeyNotFoundException($"Employee with ID {employee.Id} not found.");
            }
            if (!await IsEmailUniqueAsync(employee.Email, employee.Id))
            {
                throw new InvalidOperationException("Email address is already in use by another employee.");
            }
            await _employeeRepository.UpdateAsync(employee);
        }

        public async Task DeleteEmployeeAsync(int id)
        {
            if (!await _employeeRepository.ExistsAsync(id))
            {
                throw new KeyNotFoundException($"Employee with ID {id} not found.");
            }
            await _employeeRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(int departmentId)
        {
            return await _employeeRepository.GetByDepartmentAsync(departmentId);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int? excludeEmployeeId = null)
        {
            var existingEmployee = await _employeeRepository.GetByEmailAsync(email);
            return existingEmployee == null || existingEmployee.Id == excludeEmployeeId;
        }
    }
}
