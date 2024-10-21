using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.TimeOffDTOs
{
    public class TimeOffBalanceUpdateDto
    {
        public decimal VacationDaysAdjustment { get; set; }
        public decimal SickDaysAdjustment { get; set; }
        public decimal PersonalDaysAdjustment { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
