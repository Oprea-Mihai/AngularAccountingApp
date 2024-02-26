using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Models
{
    public class WeekModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public bool IsPaid { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}
