namespace WEB_3_Accountant.Models
{
    public class EmployeeModel
    {
        public string Name { get; set; } = string.Empty;
        public int TotalHours { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int TaxPercentage { get; set; }
    }
}