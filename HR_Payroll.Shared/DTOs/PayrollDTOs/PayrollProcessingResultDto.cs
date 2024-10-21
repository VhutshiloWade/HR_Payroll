using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayrollProcessingResultDto
    {
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public int TotalEmployeesProcessed { get; set; }
        public decimal TotalGrossPay { get; set; }
        public decimal TotalNetPay { get; set; }
        public decimal TotalTaxWithheld { get; set; }
        public List<PayrollProcessingErrorDto>? Errors { get; set; }
    }
}
