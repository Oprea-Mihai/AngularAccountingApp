using Moq;
using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models.ExcelModels;
using Task = WEB_3_Accountant.Data.Entities.Task;

namespace WEB_3_Accountant.tests
{
    public class ExcelCheckerShould
    {
        public ExcelChecker excelChecker { get; set; }
        ExcelDTO excel;

        public ExcelCheckerShould()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Project.GetByName("Test project")).Returns(new Project());
            unitOfWorkMock.Setup(x => x.Task.GetTaskByName("Another task")).Returns(new Task());
            excelChecker = new ExcelChecker(unitOfWorkMock.Object);

            excel = new ExcelDTO();
            DayOfWorkDTO day = new DayOfWorkDTO();
            day.Date = DateTime.Now;
            day.Project = "Test project";
            day.WorkedHours = "4";
            day.Task = "Test task";
            ExcelRowDTO row = new ExcelRowDTO();
            row.TotalHours = "4";
            row.DaysOfWork.Add(day);
            row.TaxPercentage = "35";
            row.HourlyRate = "30";
            row.GrossAmount = "120";
            row.NetAmount = "78";
            excel.ExcelRow.Add(row);
            excel.TotalNetAmount = "78";
            excel.TotalGrossAmount = "120";
        }

        [Fact]
        public void ValidateExcelCalc_ShouldReturnCorrectListOfErrors()
        {
            excel.ExcelRow[0].TotalHours = "6";
            excel.TotalGrossAmount = "122";
            excelChecker.ValidateExcelCalc(excel);

            Assert.Equal(2, excelChecker.errorData.Count());

            Assert.Equal("total hours", excelChecker.errorData[0].FieldName);
            Assert.Equal(4, excelChecker.errorData[0].CorrectValue);

            Assert.Equal("total gross", excelChecker.errorData[1].FieldName);
            Assert.Equal(120, excelChecker.errorData[1].CorrectValue);
        }

        [Fact]
        public void ValidateExcelTasksAndProjects_ShouldFindWrongTaskAndProjectNames()
        {
            excelChecker.ValidateExcelTasksAndProjects(excel);

            Assert.Equal("task", excelChecker.errorData[0].FieldName);
        }

    }
}
