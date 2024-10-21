using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.EmployeeDTOs
{
    public class EmployeePayrollSummaryDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public decimal CurrentSalary { get; set; }
        public decimal YearToDateEarnings { get; set; }
        public decimal YearToDateTaxes { get; set; }
    }
}
