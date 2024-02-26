using System;
using System.Collections.Generic;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class Task
    {
        public Task()
        {
            EmployeeTasks = new HashSet<EmployeeTask>();
        }

        public int TaskId { get; set; }
        public string Name { get; set; } = null!;
        public decimal? Income { get; set; }
        public int ProjectId { get; set; }
        public bool IsSpecial { get; set; }

        public virtual Project Project { get; set; } = null!;
        public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }
    }
}
