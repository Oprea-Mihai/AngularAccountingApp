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
    public class ProjectControllerShould
    {
        private ProjectController _projectController;
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        public ProjectControllerShould()
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

            unitOfWorkMock.Setup(repo => repo.Payment.GetByDate(new DateTime(2022, 04, 09)))
                .Returns(new Week()
                {
                    WeekId = 1,
                    StartDate = new DateTime(2022, 04, 09),
                    EndDate = new DateTime(2022, 11, 09),
                    TotalGrossAmount = 10000,
                    TotalNetAmount = 8000,
                    IsPaid = false,
                });

            unitOfWorkMock.Setup(repo => repo.WeeklyCost.GetAllById(1))
               .Returns(new List<WeeklyCost>() { new WeeklyCost() { EmployeeId = 1,GrossAmmount = 10, TotalHours = 20, WeekId = 1,WeeklyCostId = 1},
                new WeeklyCost() { EmployeeId = 2,GrossAmmount = 20, TotalHours = 30, WeekId = 1,WeeklyCostId = 2},
                new WeeklyCost() { EmployeeId = 3,GrossAmmount = 30, TotalHours = 18, WeekId = 1,WeeklyCostId = 3},
                new WeeklyCost() { EmployeeId = 4,GrossAmmount = 40, TotalHours = 10, WeekId = 1,WeeklyCostId = 4},
                new WeeklyCost() { EmployeeId = 5,GrossAmmount = 50, TotalHours = 14, WeekId = 1,WeeklyCostId = 5}});

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

            unitOfWorkMock.Setup(repo => repo.Task.GetAll())
                .Returns(new List<Task>() { new Task() { TaskId = 1, Name = "task1", Income = 1000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 2, Name = "task2", Income = 2000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 3, Name = "task3", Income = 3000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 4, Name = "task4", Income = 4000, ProjectId = 1, IsSpecial = false },
                    new Task() { TaskId = 5, Name = "task5", Income = 5000, ProjectId = 1, IsSpecial = false} });

            unitOfWorkMock.Setup(repo => repo.Project.GetById(1))
                .Returns(new Project()
                {
                    ProjectId = 1,
                    Name = "Project",
                    InitialBudget = 1000,
                    CurrentBudget = 1000,
                    IsFinished = false,
                });

            unitOfWorkMock.Setup(repo => repo.Project.GetUnfinishedProjects()).Returns(new List<Project>()
            {
               new Project()
               {
                   ProjectId = 1,
                   Name = "Project",
                   InitialBudget = 1000,
                   IsFinished = false,
                   CurrentBudget = 1000,
               },
            });

            unitOfWorkMock.Setup(repo => repo.Project.SetProjectFinished("Project"));

            _projectController = new ProjectController(unitOfWorkMock.Object);
        }

        [Fact]
        public void GetProject_ShouldReturn200()
        {
            var response = (ObjectResult)_projectController.GetProject();

            var values = JsonConvert.SerializeObject(response.Value);

            var expectedValue = JsonConvert.SerializeObject(new List<ProjectDTO>()
            {
                new ProjectDTO()
               {
                   Name = "Project",
                   InitialBudget = 1000,
                   IsFinished = false,
                   CurrentBudget = 1000,
                   SpentMoney = 0,
               },
               new ProjectDTO()
               {
                   SpentMoney = 0,
                   Name = "Project2",
                   InitialBudget = 2000,
                   IsFinished = true,
                   CurrentBudget = 2000,
               },
               new ProjectDTO()
               {
                   SpentMoney = 0,
                   Name = "Project3",
                   InitialBudget = 3000,
                   IsFinished = true,
                   CurrentBudget = 3000,
               }
            });

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(expectedValue, values);
        }

        [Fact]
        public void GetProject_ShouldReturn500()
        {
            unitOfWorkMock.Setup(repo => repo.Project.GetAll())
                .Returns((List<Project>)null);

            var response = (ObjectResult)_projectController.GetProject();

            Assert.Equal(StatusCodes.Status500InternalServerError, response.StatusCode);
        }

        [Fact]
        public void UpdateProjectsBudget_ShouldReturn204()
        {
            var response = (NoContentResult)_projectController.UpdateProjectsBudget(new DateTime(2022, 04, 09));

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }

        [Fact]
        public void UpdateProjectsBudget_ShouldReturn400()
        {
            unitOfWorkMock.Setup(repo => repo.Payment.GetByDate(new DateTime(2021, 11, 12))).Returns(null as Week);

            var response = (ObjectResult)_projectController.UpdateProjectsBudget(new DateTime(2021, 11, 12));

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }

        [Fact]
        public void GetUnfinishedProjects_ShouldReturn200()
        {
            var response = (ObjectResult)_projectController.GetUnfinishedProject();

            var values = JsonConvert.SerializeObject(response.Value);

            var expectedValue = JsonConvert.SerializeObject(new List<string>() { "Project" });

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(expectedValue, values);
        }

        [Fact]
        public void MarkProjectFinished_ShouldReturn200()
        {
            var response = (OkResult)_projectController.MarkProjectFinished("Project");

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
        }
    }
}
