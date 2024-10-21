using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.EmployeeDTOs
{
    public class EmployeeSearchDto
    {
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
    }
}
