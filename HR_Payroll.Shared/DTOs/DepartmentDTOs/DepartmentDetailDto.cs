using HR_Payroll.Shared.DTOs.EmployeeDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.DepartmentDTOs
{
    public class DepartmentDetailDto : DepartmentDto
    {
        public int EmployeeCount { get; set; }
        public decimal TotalSalaries { get; set; }
        public List<EmployeeSummaryDto>? Employees { get; set; }
    }
}
