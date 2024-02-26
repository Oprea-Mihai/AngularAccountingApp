using WEB_3_Accountant.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Controllers
{
    [Route("api/paymenthistory")]
    public class PaymentHistoryController : Controller
    {
        private readonly IUnitOfWork _repository;

        public PaymentHistoryController(IUnitOfWork repo)
        {
            this._repository = repo;
        }

        [HttpGet]
        public IActionResult GetPaymentHistoryByWeek([FromQuery] DateTime startDate)
        {
            var currentWeek = _repository.Payment.GetByDateAndPaid(startDate.Date);

            var employees = _repository.WeeklyCost.GetAllById(currentWeek.WeekId).Join(_repository.Employee.GetAll(),
               weeklycost => weeklycost.EmployeeId,
               employee => employee.EmployeeId,
               (weeklycost, employee) => new EmployeeModel
               {
                   Name = employee.Name,
                   TotalHours = weeklycost.TotalHours,
                   GrossAmount = weeklycost.GrossAmmount,
                   NetAmount = weeklycost.GrossAmmount - (weeklycost.GrossAmmount * employee.TaxPercentage) / 100,
                   TaxPercentage = employee.TaxPercentage,
               }
               );

            if (employees.Count() == 0)
                return NotFound("This week has no employees!");


            var projects = _repository.WeeklyCost.GetAllById(currentWeek.WeekId).Join(_repository.EmployeeTask.GetEmployeeTaskWithoutSpecialDuplicated(),
                weeklycost => weeklycost.WeeklyCostId,
                employeeTask => employeeTask.WeeklyCostId,
                (weeklycost, employeeTask) => new
                {
                    TaskId = employeeTask.TaskId,
                    WorkedHours = employeeTask.WorkedHours,
                    EmployeeTaskId = employeeTask.EmployeeTaskId,
                    EmployeeId = employeeTask.EmployeeId
                }
                ).Join(_repository.Task.GetAll(),
                task => task.TaskId,
                employeetask => employeetask.TaskId,
                (employeetask, task) => new
                {
                    TaskId = task.TaskId,
                    ProjectId = task.ProjectId,
                    EmployeeId = employeetask.EmployeeId,
                    EmployeeTaskId = employeetask.EmployeeTaskId,
                    IsSpecial = task.IsSpecial,
                    Income = task.Income,
                    WorkedHours = employeetask.WorkedHours,
                }
                ).Join(_repository.Employee.GetAll(),
                employeetasks => employeetasks.EmployeeId,
                employee => employee.EmployeeId,
                (employeetasks, employee) => new
                {
                    TaskId = employeetasks.TaskId,
                    ProjectId = employeetasks.ProjectId,
                    EmployeeTaskId = employeetasks.EmployeeTaskId,
                    IsSpecial = employeetasks.IsSpecial,
                    Income = employeetasks.Income,
                    WorkedHours = employeetasks.WorkedHours,
                    HourlyRate = employee.HourlyRate
                }
                ).Join(_repository.Project.GetAll(),
                tasks => tasks.ProjectId,
                project => project.ProjectId,
                (tasks, project) => new PaymentProjectDTO
                {
                    Name = project.Name,
                    TotalSum = ((tasks.IsSpecial ? tasks.Income : Decimal.Parse(tasks.WorkedHours) * tasks.HourlyRate))
                }
                ).GroupBy(d => d.Name)
                 .Select(
                    g => new PaymentProjectDTO
                    {
                        Name = g.First().Name,
                        TotalSum = g.Sum(s => s.TotalSum)
                    }
                );

            var paymentsToDisplay = new WeekHistoryDTO()
            {
                Employees = employees,
                Projects = projects,
                StartDate = currentWeek.StartDate,
                EndDate = currentWeek.EndDate,
                TotalGrossAmount = currentWeek.TotalGrossAmount,
                TotalNetAmount = currentWeek.TotalNetAmount
            };

            try
            {
                if (paymentsToDisplay == null)
                    return NotFound("There aren't unpaid weeks!");
                return Ok(paymentsToDisplay);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("toselect")]
        public IActionResult GetPaymentHistoryWeekToSelect()
        {
            var weeksPaid = _repository.Payment.GetPaidWeeks().OrderBy(o => o.StartDate);
            List<String> weeksToSelect = new List<String>();

            foreach(var week in weeksPaid)
            {
                var weekToSelect = week.StartDate.ToString("MM.dd.yy") + " - " + week.EndDate.ToString("MM.dd.yy");
                weeksToSelect.Add(weekToSelect);
            }

            return Ok(weeksToSelect);
        }
    }
}
