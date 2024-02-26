using System;
using System.Collections.Generic;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class Project
    {
        public Project()
        {
            Tasks = new HashSet<Task>();
        }

        public int ProjectId { get; set; }
        public string Name { get; set; } = null!;
        public decimal InitialBudget { get; set; }
        public bool IsFinished { get; set; }
        public decimal CurrentBudget { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
