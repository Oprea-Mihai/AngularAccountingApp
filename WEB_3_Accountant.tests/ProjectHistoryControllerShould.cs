using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WEB_3_Accountant.Controllers;
using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;
using Task = WEB_3_Accountant.Data.Entities.Task;

namespace WEB_3_Accountant.tests
{
    public class ProjectHistoryControllerShould
    {
        private ProjectHistoryController _projectHistoryController;
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        public ProjectHistoryControllerShould()
        {
            unitOfWorkMock.Setup(repo => repo.Project.GetAll()).Returns(new List<Project>()
            {
               new Project()
               {
                   ProjectId = 1,
                   Name = "Project",
                   InitialBudget = 1000,
                   IsFinished = false,
                   CurrentBudget = 1000,
               },
               new Project()
               {
                   ProjectId = 2,
                   Name = "Project2",
                   InitialBudget = 2000,
                   IsFinished = true,
                   CurrentBudget = 2000,
               },
               new Project()
               {
                   ProjectId = 3,
                   Name = "Project3",
                   InitialBudget = 3000,
                   IsFinished = true,
                   CurrentBudget = 3000,
               }
            });

            unitOfWorkMock.Setup(repo => repo.Employee.GetAll())
                .Returns(new List<Employee>() { new Employee() { EmployeeId = 1, HourlyRate = 21, Name = "Andrei", TaxPercentage = 30 },
                    new Employee() { EmployeeId = 2, HourlyRate = 22, Name = "Mike", TaxPercentage = 30 },
                    new Employee() { EmployeeId = 3, HourlyRate = 29, Name = "Ionut", TaxPercentage = 40 },
                    new Employee() { EmployeeId = 4, HourlyRate = 18, Name = "Patri", TaxPercentage = 15 },
                    new Employee() { EmployeeId = 5, HourlyRate = 27, Name = "Adi", TaxPercentage = 35 } });

            unitOfWorkMock.Setup(repo => repo.EmployeeTask.GetAll())
                .Returns(new List<EmployeeTask>() { new EmployeeTask() { EmployeeTaskId = 1, EmployeeId = 1, Date = new DateTime(2022, 05, 09), WorkedHours = "10", WeeklyCostId = 1, TaskId = 1 },
                    new EmployeeTask() { EmployeeTaskId = 1, EmployeeId = 2, Date = new DateTime(2022, 05, 09), WorkedHours = "10", WeeklyCostId = 2, TaskId = 2 },
                    new EmployeeTask() { EmployeeTaskId = 1, EmployeeId = 3, Date = new DateTime(2022, 05, 09), WorkedHours = "10", WeeklyCostId = 3, TaskId = 3 },
                    new EmployeeTask() { EmployeeTaskId = 1, EmployeeId = 4, Date = new DateTime(2022, 05, 09), WorkedHours = "10", WeeklyCostId = 4, TaskId = 4 },
                    new EmployeeTask() { EmployeeTaskId = 1, EmployeeId = 5, Date = new DateTime(2022, 05, 09), WorkedHours = "10", WeeklyCostId = 5, TaskId = 5} });

            unitOfWorkMock.Setup(repo => repo.Task.GetTaskByProjectID(1))
                .Returns(new List<Task>() { new Task() { TaskId = 1, Name = "task1", Income = 1000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 2, Name = "task2", Income = 2000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 3, Name = "task3", Income = 3000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 4, Name = "task4", Income = 4000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 5, Name = "task5", Income = 5000, ProjectId = 1, IsSpecial = false} });

            unitOfWorkMock.Setup(repo => repo.Project.GetByName("Project"))
                .Returns(new Project()
                {
                    ProjectId = 1,
                    Name = "Project",
                    InitialBudget = 1000,
                    CurrentBudget = 1000,
                    IsFinished = false,
                });

            _projectHistoryController = new ProjectHistoryController(unitOfWorkMock.Object);
        }

        [Fact]
        public void GetProjectHistory_ShouldReturn200()
        {
            var response = (ObjectResult)_projectHistoryController.GetProjectHistory("Project");

            var values = JsonConvert.SerializeObject(response.Value);

            var expectedValue = JsonConvert.SerializeObject(new List<ProjectEmployeesDTO>()
            {
                new ProjectEmployeesDTO()
                {
                    Name = "Andrei",
                    WorkedHours = 10
                },
                new ProjectEmployeesDTO()
                {
                   Name = "Mike",
                   WorkedHours = 10
                },
                new ProjectEmployeesDTO()
                {
                   Name = "Ionut",
                   WorkedHours = 10
                },
                new ProjectEmployeesDTO()
                {
                   Name = "Patri",
                   WorkedHours = 10
                },
                new ProjectEmployeesDTO()
                {
                   Name = "Adi",
                   WorkedHours = 10
                },
            });

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(expectedValue, values);
        }

        [Fact]
        public void GetProjectHistory_ShouldReturn404()
        {
            unitOfWorkMock.Setup(repo => repo.Project.GetByName("Project")).Returns(null as Project);

            var response = (ObjectResult)_projectHistoryController.GetProjectHistory("Project");

            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }

        [Fact]
        public void GetUnfinishedProjects_ShouldReturn200()
        {
            var response = (ObjectResult)_projectHistoryController.GetProjectsToSelect();

            var values = JsonConvert.SerializeObject(response.Value);

            var expectedValue = JsonConvert.SerializeObject(new List<string>() { "Project", "Project2", "Project3" });

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(expectedValue, values);
        }
    }
}
