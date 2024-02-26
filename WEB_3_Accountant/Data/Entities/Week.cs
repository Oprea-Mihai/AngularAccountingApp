using System;
using System.Collections.Generic;

namespace WEB_3_Accountant.Data.Entities
{
    public partial class Week
    {
        public Week()
        {
            WeeklyCosts = new HashSet<WeeklyCost>();
        }

        public int WeekId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
        public bool IsPaid { get; set; }

        public virtual ICollection<WeeklyCost> WeeklyCosts { get; set; }
    }
}
