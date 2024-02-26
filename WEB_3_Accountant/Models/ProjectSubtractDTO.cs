namespace WEB_3_Accountant.Models
{
    public class ProjectSubtractDTO
    {
        public int ProjectId { get; set; }
        public int WorkedHours { get; set; }
        public decimal HourlyRate { get; set; }
        public decimal TaskPrice { get; set; }
        public bool isSpecial { get; set; }
        public int TaskId { get; set; }
        public int EmployeeId { get; set; }
        public int WeeklyCostId { get; set; }
        public bool isPaid { get; set; }
    }
}
