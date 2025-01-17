﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HR_Payroll.Core.Entities
{
    public class Department : BaseEntity
    {

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int? ManagerId { get; set; }
    }
}
