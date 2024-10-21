using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayrollAdjustmentDto
    {
        public int EmployeeId { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public string AdjustmentReason { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
    }
}
