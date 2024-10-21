using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayslipDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public string EmployeeNumber { get; set; } = string.Empty;
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal TaxWithheld { get; set; }
        public List<PayslipDeductionDto>? Deductions { get; set; }
        public List<PayslipBenefitDto>? Benefits { get; set; }
        public decimal TotalDeductions { get; set; }
        public decimal TotalBenefits { get; set; }
        public string PaymentMethod { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty; // Last 4 digits only for security
        public DateTime PaymentDate { get; set; }
    }
}
