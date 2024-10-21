using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class UpdatePayrollRecordDto
    {
        public decimal GrossPay { get; set; }
        public decimal TaxWithheld { get; set; }
        public decimal OtherDeductions { get; set; }
    }
}
