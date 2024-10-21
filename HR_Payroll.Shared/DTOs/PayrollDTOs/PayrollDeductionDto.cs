using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayrollDeductionDto
    {
        public int Id { get; set; }
        public string DeductionType { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
