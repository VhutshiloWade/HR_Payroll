using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class TimeOffBalance : BaseEntity
    {
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public decimal VacationDays { get; set; }
        public decimal SickDays { get; set; }
        public decimal PersonalDays { get; set; }
        public decimal UnpaidLeavedays { get; set; }
        public int Year { get; set; }
    }
}
