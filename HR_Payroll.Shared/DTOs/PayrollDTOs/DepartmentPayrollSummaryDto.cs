using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class DepartmentPayrollSummaryDto
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
        public decimal TotalGrossPay { get; set; }
        public decimal TotalNetPay { get; set; }
        public decimal TotalTaxWithheld { get; set; }
        public int EmployeeCount { get; set; }
    }
}
