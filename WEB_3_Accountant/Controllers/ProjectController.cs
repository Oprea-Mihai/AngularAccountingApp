using Microsoft.AspNetCore.Mvc;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Controllers
{
    [Route("~/api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IUnitOfWork _repos;

        public ProjectController(IUnitOfWork unitOfWork)
        {
            this._repos = unitOfWork;
        }

        [HttpGet("budget")]
        public IActionResult GetProject()
        {
            List<ProjectDTO> projectToDisplay = new List<ProjectDTO>();
            try
            {
                _repos.Project.GetAll().ToList().ForEach(item =>
            {
                projectToDisplay.Add(new ProjectDTO() { Name = item.Name, InitialBudget = item.InitialBudget, CurrentBudget = item.CurrentBudget, SpentMoney = item.InitialBudget - item.CurrentBudget, IsFinished = item.IsFinished });
            });
            if (projectToDisplay == null)
                return BadRequest("Projects aren't registered yet!");
            return Ok(projectToDisplay);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("updateprojectbudget")]
        public IActionResult UpdateProjectsBudget([FromQuery] DateTime startDate)
        {
            try
            {
                var currentUnpaidWeek = _repos.Payment.GetByDate(startDate.Date);

                if (currentUnpaidWeek == null)
                    return BadRequest("There aren't unpaid weeks!");

                var employees = _repos.WeeklyCost.GetAllById(currentUnpaidWeek.WeekId).Join(_repos.Employee.GetAll(),
                weeklycost => weeklycost.EmployeeId,
                employee => employee.EmployeeId,
                (weeklycost, employee) => new
                {
                    EmployeeId = employee.EmployeeId,
                    WeeklyCostId = weeklycost.WeeklyCostId,
                    Name = employee.Name,
                    TaxPercentage = employee.TaxPercentage,
                    HourlyRate = employee.HourlyRate,
                }
                );

                if (employees.Count() == 0)
                    return StatusCode(500);

                var employeesTasks = employees.Join(_repos.EmployeeTask.GetAll(),
                    employee => new Tuple<int, int>(employee.EmployeeId, employee.WeeklyCostId),
                    employeeTask => new Tuple<int, int>(employeeTask.EmployeeId, employeeTask.WeeklyCostId),
                    (employee, employeeTask) => new
                    {
                        HourlyRate = employee.HourlyRate,
                        TaskId = employeeTask.TaskId,
                        WorkedHours = employeeTask.WorkedHours,
                        EmployeeId = employeeTask.EmployeeId,
                        WeeklyCostId = employeeTask.WeeklyCostId,

                    });

                var projectToSubtract = employeesTasks.Join(_repos.Task.GetAll(),
                    eTask => eTask.TaskId,
                    task => task.TaskId,
                    (etask, task) => new ProjectSubtractDTO
                    {
                        ProjectId = task.ProjectId,
                        WorkedHours = int.Parse(etask.WorkedHours),
                        HourlyRate = etask.HourlyRate,
                        TaskPrice = (decimal)task.Income,
                        isSpecial = task.IsSpecial,
                        TaskId = task.TaskId,
                        EmployeeId = etask.EmployeeId,
                        WeeklyCostId = etask.WeeklyCostId,
                    });

                var specialTasks = new List<ProjectSubtractDTO>();
                
                projectToSubtract.ToList().ForEach(item =>
                {
                    var project = _repos.Project.GetById(item.ProjectId);
                    if (item.isSpecial == false)
                    {
                        project.CurrentBudget -= item.WorkedHours * item.HourlyRate;
                    }
                    else
                    {
                        if (specialTasks.Where(stask => stask.TaskId == item.TaskId && stask.EmployeeId == item.EmployeeId && stask.WeeklyCostId == item.WeeklyCostId).FirstOrDefault() == null)
                        {
                            specialTasks.Add(item);
                            project.CurrentBudget -= item.TaskPrice;
                        }
                    }

                    _repos.Project.Update(project);
                    _repos.Complete();
                });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
    }

        [HttpGet("unfinishedProjects")]
        public IActionResult GetUnfinishedProject()
        {
            List<string> unfinishedProjects = new List<string>();

            foreach (var project in _repos.Project.GetUnfinishedProjects())
                unfinishedProjects.Add(project.Name);

            return Ok(unfinishedProjects);
        }

        [HttpPost("markFinishedProject")]
        public IActionResult MarkProjectFinished([FromQuery]string prjName)
        {
            _repos.Project.SetProjectFinished(prjName);
            _repos.Complete();

            return Ok();
        }
    }
}
