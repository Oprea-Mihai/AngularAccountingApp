namespace WEB_3_Accountant.Models
{
    public class ErrorData
    {
        public string FieldName { get; set; } = "";
        public decimal CorrectValue { get; set; } = default(decimal);
        public int RowNumber { get; set; } = default;
        public int ColNumber { get; set; } = default;
    }
}
