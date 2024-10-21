using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class PayrollRecord : BaseEntity
    {
        public PayrollRecord()
        {
            Deductions = new List<PayrollDeduction>();
            Benefits = new List<PayrollBenefit>();
            PaymentMethod = string.Empty;
            PaymentStatus = string.Empty;
        }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal TaxWithheld { get; set; }
        public decimal OtherDeductions { get; set; }
        public ICollection<PayrollDeduction> Deductions { get; set; }
        public ICollection<PayrollBenefit> Benefits { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatus { get; set; }
        public DateTime? ProcessedDate { get; set; }
        public DateTime PaymentDate { get; set; }
        public ICollection<SalaryAdjustment> Adjustments { get; set; } = new List<SalaryAdjustment>();

    }
}
