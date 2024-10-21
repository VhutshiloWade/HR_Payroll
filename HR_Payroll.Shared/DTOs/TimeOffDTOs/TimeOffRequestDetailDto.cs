using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.TimeOffDTOs
{
    public class TimeOffRequestDetailDto : TimeOffRequestDto
    {
        public string ApprovedBy { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
