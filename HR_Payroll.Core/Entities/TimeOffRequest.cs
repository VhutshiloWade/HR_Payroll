using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class TimeOffRequest : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string TimeOffType { get; set; } = string.Empty; // e.g., Vacation, Sick Leave, Personal Day
        public string Status { get; set; } = string.Empty; // e.g., Pending, Approved, Rejected
        public string Reason { get; set; } = string.Empty;
        public string ApprovedBy { get; set; } = string.Empty;
        public DateTime? ApprovalDate { get; set; }
        public string Comments { get; set; } = string.Empty;
    }
}
