using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class SalaryAdjustment : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public decimal PreviousSalary { get; set; }
        public decimal NewSalary { get; set; }
        public decimal AdjustmentAmount { get; set; }
        public string AdjustmentType { get; set; } = string.Empty; // e.g., Raise, Promotion, Cost of Living
        public DateTime EffectiveDate { get; set; }
        public string Reason { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        
    }
}
