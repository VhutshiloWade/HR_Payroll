using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayrollRecordDetailDto : PayrollRecordDto
    {
        public List<PayrollDeductionDto>? Deductions { get; set; }
        public List<PayrollBenefitDto>? Benefits { get; set; }
    }
}
