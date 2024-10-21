﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Shared.DTOs.EmployeeDTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string EmployeeNumber { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
        public string DepartmentName { get; set; } = string.Empty;
    }
}