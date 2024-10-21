using HR_Payroll.Core.Entities;
using HR_Payroll.Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HR_Payroll.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DepartmentsController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentsController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Department>>> GetAllDepartments()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
            return Ok(departments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Department>> GetDepartmentById(int id)
        {
            try
            {
                var department = await _departmentService.GetDepartmentByIdAsync(id);
                return Ok(department);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Department>> CreateDepartment(Department department)
        {
            var createdDepartment = await _departmentService.CreateDepartmentAsync(department);
            return CreatedAtAction(nameof(GetDepartmentById), new { id = createdDepartment.Id }, createdDepartment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(int id, Department department)
        {
            if (id != department.Id)
            {
                return BadRequest("Department ID mismatch");
            }

            try
            {
                await _departmentService.UpdateDepartmentAsync(department);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                await _departmentService.DeleteDepartmentAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("{id}/employees")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployeesByDepartment(int id)
        {
            try
            {
                var employees = await _departmentService.GetEmployeesByDepartmentAsync(id);
                return Ok(employees);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
