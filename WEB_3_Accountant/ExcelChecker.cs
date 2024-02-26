using System.Linq;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;
using WEB_3_Accountant.Models.ExcelModels;

namespace WEB_3_Accountant
{
    public class ExcelChecker
    {
        public IUnitOfWork unitOfWork { get; set; }
        public List<ErrorData> errorData { get; set; } = new List<ErrorData>();

        public ExcelChecker(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void ValidateExcelCalc(ExcelDTO excel)
        {

            decimal totalGross = 0;
            decimal totalNet = 0;
            int rowNumber = 0;
            foreach (ExcelRowDTO row in excel.ExcelRow)
            {
                List<string> specialTasks = new List<string>();
                int workedHours = 0;
                decimal income = 0;
                decimal netIncome = 0;
                foreach (DayOfWorkDTO day in row.DaysOfWork)
                {
                    int dayWorkedHours;
                    if (Int32.TryParse(day.WorkedHours, out dayWorkedHours) == true)
                        workedHours += dayWorkedHours;
                    else if (day.WorkedHours == "" && day.Task != "")
                        if (!specialTasks.Contains(day.Task))
                            specialTasks.Add(day.Task);
                }

                if (specialTasks.Count != 0)
                {
                    foreach (string task in specialTasks)
                        if(unitOfWork.Task.GetSpecialTaskValueByName(task)>=0)
                        income += unitOfWork.Task.GetSpecialTaskValueByName(task);
                }

                if (workedHours != Int32.Parse(row.TotalHours))
                {
                    ErrorData errData = new ErrorData();
                    errData.CorrectValue = workedHours;
                    errData.FieldName = "total hours";
                    errData.RowNumber = rowNumber;
                    errorData.Add(errData);
                }

                income += workedHours * decimal.Parse(row.HourlyRate);
                var roundedIncome = Decimal.Round(income);
                if (roundedIncome != Decimal.Round(decimal.Parse(row.GrossAmount)))
                {
                    ErrorData errData = new ErrorData();
                    errData.CorrectValue = income;
                    errData.FieldName = "gross amount";
                    errData.RowNumber = rowNumber;
                    errorData.Add(errData);
                }

                netIncome = income - ((income * decimal.Parse(row.TaxPercentage)) / 100);
                var roundedNetIncome = Decimal.Round(netIncome);
                if ((roundedNetIncome) != Decimal.Round(decimal.Parse(row.NetAmount)))
                {
                    ErrorData errData = new ErrorData();
                    errData.CorrectValue = netIncome;
                    errData.FieldName = "-tax";
                    errData.RowNumber = rowNumber;
                    errorData.Add(errData);
                }

                totalGross += income;
                totalNet += netIncome;
                rowNumber++;
            }

            if (Decimal.Round(totalGross) != Decimal.Round(decimal.Parse(excel.TotalGrossAmount)))
            {
                ErrorData errData = new ErrorData();
                errData.CorrectValue = totalGross;
                errData.FieldName = "total gross";
                errData.RowNumber = -1;
                errorData.Add(errData);
            }

            if (Decimal.Round(totalNet) != Decimal.Round(decimal.Parse(excel.TotalNetAmount)))
            {
                ErrorData errData = new ErrorData();
                errData.CorrectValue = totalNet;
                errData.FieldName = "total net";
                errData.RowNumber = -1;
                errorData.Add(errData);
            }
        }


        public void ValidateExcelTasksAndProjects(ExcelDTO excel)
        {
            int rowNumber = 0;
            foreach (ExcelRowDTO row in excel.ExcelRow)
            {
                int colNumber = 0;
                foreach (DayOfWorkDTO day in row.DaysOfWork)
                {                  

                    if (day.WorkedHours != "Off")
                    {
                        bool projectOk = true;
                        if (day.Project != "")
                            if (unitOfWork.Project.GetByName(day.Project) == null)
                            {
                                projectOk = false;
                                ErrorData errData = new ErrorData();
                                errData.FieldName = "project";
                                errData.RowNumber = rowNumber;
                                errData.ColNumber = colNumber;
                                errorData.Add(errData);
                            }

                        if (day.WorkedHours != "" && day.WorkedHours != String.Empty)
                        {
                            if (unitOfWork.Task.GetTaskByName(day.Task) == null)
                            {
                                ErrorData errData = new ErrorData();
                                errData.FieldName = "task";
                                errData.RowNumber = rowNumber;
                                errData.ColNumber = colNumber;
                                errorData.Add(errData);
                            }
                            else
                            {
                                if (projectOk)
                                {
                                    if (!unitOfWork.Project.GetAssignedTasks(day.Project).Contains(unitOfWork.Task.GetTaskByName(day.Task)))
                                    {
                                        ErrorData errData = new ErrorData();
                                        errData.FieldName = "task";
                                        errData.RowNumber = rowNumber;
                                        errData.ColNumber = colNumber;
                                        errorData.Add(errData);
                                    }
                                }
                            }
                        }

                        else if (day.WorkedHours == "" && day.Task != "")
                            if (unitOfWork.Task.GetSpecialTaskValueByName(day.Task) == -1)
                            {
                                ErrorData errData = new ErrorData();
                                errData.FieldName = "task";
                                errData.RowNumber = rowNumber;
                                errData.ColNumber = colNumber;
                                errorData.Add(errData);
                            }
                    }
                    colNumber++;
                }
                rowNumber++;
            }
        }
    }
}