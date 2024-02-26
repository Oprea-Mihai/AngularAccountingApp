using System;
using System.Collections.Generic;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class WeeklyCost
    {
        public WeeklyCost()
        {
            EmployeeTasks = new HashSet<EmployeeTask>();
        }

        public int WeeklyCostId { get; set; }
        public int TotalHours { get; set; }
        public decimal GrossAmmount { get; set; }
        public int EmployeeId { get; set; }
        public int WeekId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Week Week { get; set; } = null!;
        public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }
    }
}
