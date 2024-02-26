using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models.ExcelModels;

namespace WEB_3_Accountant.Models
{
    public class ExcelToEnitities
    {


        public void getEmployees(ExcelDTO excel, IUnitOfWork unitOfWork)
        {
            foreach (ExcelRowDTO row in excel.ExcelRow)
            {
                Employee employee = new Employee();
                employee.Name = row.Name;
                employee.TaxPercentage = Int32.Parse(row.TaxPercentage);
                employee.HourlyRate = Decimal.Parse(row.HourlyRate);

                if (unitOfWork.Employee.GetByName(row.Name) == null)
                {

                    unitOfWork.Employee.Add(employee);
                    unitOfWork.Complete();
                }
            }

        }

        public void getWeeks(ExcelDTO excel, IUnitOfWork unitOfWork)
        {
            Week week = new();

            week.StartDate = excel.ExcelRow[0].DaysOfWork[0].Date;
            week.EndDate = excel.ExcelRow[0].DaysOfWork[6].Date;
            week.TotalGrossAmount = Decimal.Parse(excel.TotalGrossAmount);
            week.TotalNetAmount = Decimal.Parse(excel.TotalNetAmount);
            week.IsPaid = false;

            if (unitOfWork.Week.GetWeekByDate(week.StartDate, week.EndDate) == null)
            {
                unitOfWork.Week.Add(week);
            }
        }

        public void getEmployeeTasks(ExcelDTO excel, IUnitOfWork unitOfWork)
        {

            foreach (ExcelRowDTO row in excel.ExcelRow)
            {

                var weekId = unitOfWork.Week.GetWeekByDate(excel.ExcelRow[0].DaysOfWork[0].Date, excel.ExcelRow[0].DaysOfWork[6].Date).WeekId;
                var employeeId = unitOfWork.Employee.GetByName(row.Name).EmployeeId;
                var weeklyCostId = unitOfWork.WeeklyCost.GetByEmployeeIdAndWeekId(employeeId, weekId).WeeklyCostId;

                foreach (var item in row.DaysOfWork)
                {
                    var employee = new EmployeeTask();
                    if (item.Project != String.Empty && item.Project != "Off" && item.Project != null && item.WorkedHours != "Off")
                    {
                        employee.EmployeeId = employeeId;
                        employee.WeeklyCostId = weeklyCostId;
                        employee.TaskId = unitOfWork.Task.GetTaskByName(item.Task).TaskId;
                        if (item.WorkedHours != String.Empty && item.WorkedHours != null)
                        {
                            employee.WorkedHours = item.WorkedHours;
                        }
                        else
                        {
                            employee.WorkedHours = "0";
                        }
                        employee.Date = item.Date;

                        if (unitOfWork.EmployeeTask.GetEmployeeTaskByEmployeeAndWeeklyCostIdAndTaskId(employee.EmployeeId, employee.WeeklyCostId, employee.TaskId) == null)
                        {
                            unitOfWork.EmployeeTask.Add(employee);
                        }
                    }

                }
            }
        }

        public void getWeeklyCost(ExcelDTO excel, IUnitOfWork unitOfWork)
        {
            List<WeeklyCost> weeklyCosts = new List<WeeklyCost>();

            foreach (ExcelRowDTO row in excel.ExcelRow)
            {

                WeeklyCost weeklyCost = new WeeklyCost();
                weeklyCost.TotalHours = Int32.Parse(row.TotalHours);
                weeklyCost.GrossAmmount = Decimal.Parse(row.GrossAmount);
                weeklyCost.EmployeeId = unitOfWork.Employee.GetByName(row.Name).EmployeeId;
                weeklyCost.WeekId = unitOfWork.Week.GetWeekByDate(excel.ExcelRow[0].DaysOfWork[0].Date, excel.ExcelRow[0].DaysOfWork[6].Date).WeekId;

                if (weeklyCosts.Where(x => x.EmployeeId == weeklyCost.EmployeeId && x.WeekId == weeklyCost.WeekId).FirstOrDefault() == null)
                {
                    if (unitOfWork.WeeklyCost.GetByEmployeeIdAndWeekId(weeklyCost.EmployeeId, weeklyCost.WeekId) == null)
                    {
                        weeklyCosts.Add(weeklyCost);
                    }
                }
                else
                {
                    if (weeklyCosts.Count > 0)
                    {
                        var updateData = weeklyCosts.Where(x => x.EmployeeId == weeklyCost.EmployeeId && x.WeekId == weeklyCost.WeekId).FirstOrDefault();
                        updateData.TotalHours += weeklyCost.TotalHours;
                        updateData.GrossAmmount += weeklyCost.GrossAmmount;
                    }
                }
            }

            unitOfWork.WeeklyCost.AddRange(weeklyCosts);
        }

        public void getEntities(ExcelDTO excel, IUnitOfWork unitOfWork)
        {
            getEmployees(excel, unitOfWork);

            getWeeks(excel, unitOfWork);
            unitOfWork.Complete();

            getWeeklyCost(excel, unitOfWork);
            unitOfWork.Complete();

            getEmployeeTasks(excel, unitOfWork);
            unitOfWork.Complete();
        }
    }
}
