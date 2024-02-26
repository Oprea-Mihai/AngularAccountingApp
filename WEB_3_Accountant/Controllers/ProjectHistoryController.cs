using Microsoft.AspNetCore.Mvc;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.Controllers
{
    [Route("api/projecthistory")]
    [ApiController]

    public class ProjectHistoryController : Controller
    {
        private readonly IUnitOfWork _repository;

        public ProjectHistoryController(IUnitOfWork repo)
        {
            this._repository = repo;
        }

        [HttpGet]
        public IActionResult GetProjectHistory([FromQuery] string projectName)
        {
            var currentProject = _repository.Project.GetByName(projectName);

            if (currentProject == null)
                return NotFound("Project not found.");

            var projectHistory = _repository.Task.GetTaskByProjectID(currentProject.ProjectId).Join(_repository.EmployeeTask.GetAll(),
                task => task.TaskId,
                employeeTask => employeeTask.TaskId,
                (task, employeetask) => new
                {
                    EmployeeId = employeetask.EmployeeId,
                    WorkedHours = employeetask.WorkedHours
                }
                ).Join(_repository.Employee.GetAll(),
                taskEmployee => taskEmployee.EmployeeId,
                employee => employee.EmployeeId,
                (taskEmployee, employee) => new ProjectEmployeesDTO
                {
                    Name = employee.Name,
                    WorkedHours = (taskEmployee.WorkedHours == "0" ? 0 : Convert.ToDecimal(taskEmployee.WorkedHours))
                }
                ).GroupBy(d => d.Name)
                 .Select(
                    g => new ProjectEmployeesDTO
                    {
                        Name = g.First().Name,
                        WorkedHours = g.Sum(s => s.WorkedHours)
                    }
                );
            try
            {
                if (projectHistory == null)
                    return NotFound("There aren't unpaid weeks!");
                return Ok(projectHistory);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("toselect")]
        public IActionResult GetProjectsToSelect()
        {
            var projects = _repository.Project.GetAll();
            List<String> projectsToSelect = new List<String>();

            foreach (var project in projects)
            {
                var projectToSelect = project.Name;
                projectsToSelect.Add(projectToSelect);
            }

            return Ok(projectsToSelect);
        }
    }
}
