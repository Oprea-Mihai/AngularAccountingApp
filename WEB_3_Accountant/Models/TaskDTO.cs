namespace WEB_3_Accountant.Models
{
    public class TaskDTO
    {
        public string Name { get; set; } = null!;
        public decimal? Income { get; set; }
        public int ProjectId { get; set; }
        public bool IsSpecial { get; set; }
    }
}
