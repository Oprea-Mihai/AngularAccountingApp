namespace WEB_3_Accountant.Models.ExcelModels
{
    public class DayOfWorkDTO
    {
        public DateTime Date { get; set; }
        public string? WorkedHours { get; set; }
        public string? Task { get; set; } = "";
        public string? Project { get; set; } = "";

    }

}
