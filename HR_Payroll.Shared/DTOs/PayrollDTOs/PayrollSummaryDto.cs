using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayrollSummaryDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalGrossPay { get; set; }
        public decimal TotalNetPay { get; set; }
        public decimal TotalTaxWithheld { get; set; }
        public int EmployeeCount { get; set; }
        public List<DepartmentPayrollSummaryDto>? DepartmentSummaries { get; set; }
    }
}
