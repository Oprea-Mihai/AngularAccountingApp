using Newtonsoft.Json;

namespace WEB_3_Accountant.Models.ExcelModels
{
    public class ExcelDTO
    {
        [JsonProperty("excelRow")]
        public List<ExcelRowDTO> ExcelRow { get; set; } = new List<ExcelRowDTO>();

        [JsonProperty("totalGrossAmount")]
        public string TotalGrossAmount { get; set; }

        [JsonProperty("totalNetAmount")]
        public string TotalNetAmount { get; set; }
    }
}
