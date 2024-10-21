using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.TimeOffDTOs
{
    public class TimeOffBalanceDto
    {
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = string.Empty;
        public decimal VacationDays { get; set; }
        public decimal SickDays { get; set; }
        public decimal PersonalDays { get; set; }
    }
}
