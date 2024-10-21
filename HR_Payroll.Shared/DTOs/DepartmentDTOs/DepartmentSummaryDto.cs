using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.DepartmentDTOs
{
    public class DepartmentSummaryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int EmployeeCount { get; set; }
    }
}
