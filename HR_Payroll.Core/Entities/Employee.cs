using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class Employee : BaseEntity
    {

        public string EmployeeNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime DateOfBirth { get; set; }
        public DateTime HireDate { get; set; }
        public DateTime? TerminationDate { get; set; }
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string PostalCode { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public string EmergencyContactName { get; set; } = string.Empty;
        public string EmergencyContactPhone { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public string Position { get; set; } = string.Empty;
        public decimal CurrentSalary { get; set; }
        public string EmploymentStatus { get; set; } = string.Empty;
        public string PaymentMethod { get; set; } = string.Empty;
        public string BankAccountNumber { get; set; } = string.Empty;
        public Department? Department {  get; set; } 

        // Navigation properties
        public ICollection<TimeOffRequest> TimeOffRequests { get; set; } = new List<TimeOffRequest>();
        public TimeOffBalance? TimeOffBalance { get; set; }
        public ICollection<PayrollRecord> PayrollRecords { get; set; } = new List<PayrollRecord>();
        public ICollection<SalaryAdjustment> SalaryAdjustments { get; set; } = new List<SalaryAdjustment>();
    }
}
