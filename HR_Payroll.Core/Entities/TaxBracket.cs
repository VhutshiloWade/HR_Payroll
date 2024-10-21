using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class TaxBracket : BaseEntity
    {
        
        public decimal StartIncome { get; set; } // Start of the income range for the bracket
        public decimal? EndIncome { get; set; } // End of the income range, nullable for the top bracket
        public decimal FixedAmount { get; set; } // The fixed amount of tax for this bracket
        public decimal Percentage { get; set; } // The percentage of taxable income above the bracket threshold
        public DateTime EffectiveDate { get; set; } // Effective date for the tax bracket (useful for future tax year changes)

        public TaxBracket() { }
    }
}
