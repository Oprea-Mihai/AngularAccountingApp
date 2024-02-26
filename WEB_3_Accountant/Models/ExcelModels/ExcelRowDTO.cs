namespace WEB_3_Accountant.Models.ExcelModels
{
    public class ExcelRowDTO
    {
        public string Name { get; set; } = "";
        public string TaxPercentage { get; set; }
        public string HourlyRate { get; set; }
        public List<DayOfWorkDTO> DaysOfWork { get; set; } = new List<DayOfWorkDTO>();
        public string TotalHours { get; set; }
        public string GrossAmount { get; set; }
        public string NetAmount { get; set; }
    }
}
