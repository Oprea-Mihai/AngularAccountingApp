namespace WEB_3_Accountant.Models
{
    public class WeekHistoryDTO
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public IEnumerable<EmployeeModel> Employees { get; set; }
        public IEnumerable<PaymentProjectDTO> Projects { get; set; }
        public decimal TotalGrossAmount { get; set; }
        public decimal TotalNetAmount { get; set; }
    }
}
