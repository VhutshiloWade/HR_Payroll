using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class PayrollDeduction : BaseEntity
    {
        public PayrollDeduction()
        {
            DeductionType = string.Empty;
            Description = string.Empty;
        }

        public int PayrollRecordId { get; set; }
        public PayrollRecord? PayrollRecord { get; set; }
        public string DeductionType { get; set; }
        public decimal Amount { get; set; }
        public string Description { get; set; }
    }
}
