using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.PayrollDTOs
{
    public class PayrollRecordDto
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public DateTime PayPeriodStart { get; set; }
        public DateTime PayPeriodEnd { get; set; }
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public decimal TaxWithheld { get; set; }
        public decimal OtherDeductions { get; set; }
    }
}
