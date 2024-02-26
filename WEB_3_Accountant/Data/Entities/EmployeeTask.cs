using System;
using System.Collections.Generic;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class EmployeeTask
    {
        public int EmployeeTaskId { get; set; }
        public int EmployeeId { get; set; }
        public DateTime Date { get; set; }
        public string? WorkedHours { get; set; }
        public int WeeklyCostId { get; set; }
        public int TaskId { get; set; }

        public virtual Employee Employee { get; set; } = null!;
        public virtual Task Task { get; set; } = null!;
        public virtual WeeklyCost WeeklyCost { get; set; } = null!;
    }
}
