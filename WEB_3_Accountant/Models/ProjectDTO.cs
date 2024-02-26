namespace WEB_3_Accountant.Models
{
    public class ProjectDTO
    {
        public string Name { get; set; } = null!;
        public decimal InitialBudget { get; set; }
        public bool IsFinished { get; set; }
        public decimal CurrentBudget { get; set; }
        public decimal SpentMoney { get; set; }
    }
}
