using System;
using System.Collections.Generic;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeTasks = new HashSet<EmployeeTask>();
            WeeklyCosts = new HashSet<WeeklyCost>();
        }

        public int EmployeeId { get; set; }
        public string Name { get; set; } = null!;
        public decimal HourlyRate { get; set; }
        public int TaxPercentage { get; set; }

        public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }
        public virtual ICollection<WeeklyCost> WeeklyCosts { get; set; }
    }
}
