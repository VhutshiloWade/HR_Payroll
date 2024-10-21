using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class ProcessPayrollDto
    {
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public List<int>? EmployeeIds { get; set; } // Optional: Process for specific employees
        public int? DepartmentId { get; set; } // Optional: Process for a specific department
    }
}
