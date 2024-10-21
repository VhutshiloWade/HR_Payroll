using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.TimeOffDTOs
{
    public class UpdateTimeOffRequestDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeOffType { get; set; } = string.Empty;
        public string Reason { get; set; } = string.Empty;
    }
}
