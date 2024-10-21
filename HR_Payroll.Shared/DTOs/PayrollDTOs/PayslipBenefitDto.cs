using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayslipBenefitDto
    {
        public string BenefitType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
    }
}
