using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using WEB_3_Accountant.Controllers;
using WEB_3_Accountant.Data.Entities;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.tests
{
    public class PaymentControllerShould
    {
        private PaymentController _paymentController;
        Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();

        public PaymentControllerShould()
        {


            unitOfWorkMock.Setup(repo => repo.Payment.GetUnpaidWeek()).Returns(new Week()
            {
                StartDate = new DateTime(1999, 3, 16),
                EndDate = new DateTime(1999, 3, 23),
                IsPaid = false,
                TotalGrossAmount = 100,
                TotalNetAmount = 50,
                WeekId = 1,
            });

            unitOfWorkMock.Setup(repo => repo.Employee.GetAll())
                .Returns(new List<Employee>() { new Employee() { EmployeeId = 1, HourlyRate = 21, Name = "Andrei", TaxPercentage = 30 },
                    new Employee() { EmployeeId = 2, HourlyRate = 22, Name = "Mike", TaxPercentage = 30 },
                    new Employee() { EmployeeId = 3, HourlyRate = 29, Name = "Ionut", TaxPercentage = 40 },
                    new Employee() { EmployeeId = 4, HourlyRate = 18, Name = "Patri", TaxPercentage = 15 },
                    new Employee() { EmployeeId = 5, HourlyRate = 27, Name = "Adi", TaxPercentage = 35 } });

            unitOfWorkMock.Setup(repo => repo.WeeklyCost.GetAllById(1))
                .Returns(new List<WeeklyCost>() { new WeeklyCost() { EmployeeId = 1,GrossAmmount = 10, TotalHours = 20, WeekId = 1,WeeklyCostId = 1},
                new WeeklyCost() { EmployeeId = 2,GrossAmmount = 20, TotalHours = 30, WeekId = 1,WeeklyCostId = 2},
                new WeeklyCost() { EmployeeId = 3,GrossAmmount = 30, TotalHours = 18, WeekId = 1,WeeklyCostId = 3},
                new WeeklyCost() { EmployeeId = 4,GrossAmmount = 40, TotalHours = 10, WeekId = 1,WeeklyCostId = 4},
                new WeeklyCost() { EmployeeId = 5,GrossAmmount = 50, TotalHours = 14, WeekId = 1,WeeklyCostId = 5}});

            _paymentController = new PaymentController(unitOfWorkMock.Object);
        }

        [Fact]
        public void GetPayment_ShouldReturn200()
        {
            var response = (ObjectResult)_paymentController.GetPayment();

            var values = JsonConvert.SerializeObject(response.Value);

            var expectedValue = JsonConvert.SerializeObject(new WeekModel()
            {
                Employees = new List<EmployeeModel>() {new EmployeeModel() { Name = "Andrei",TotalHours = 20, GrossAmount = 10, NetAmount = 7,TaxPercentage = 30},
                    new EmployeeModel() { Name = "Mike",TotalHours = 30, GrossAmount = 20, NetAmount = 14, TaxPercentage = 30 },
                    new EmployeeModel() { Name = "Ionut",TotalHours = 18, GrossAmount = 30, NetAmount = 18,  TaxPercentage = 40},
                    new EmployeeModel() { Name = "Patri",TotalHours = 10, GrossAmount = 40, NetAmount = 34,  TaxPercentage = 15},
                    new EmployeeModel() { Name = "Adi",TotalHours = 14, GrossAmount = 50, NetAmount = (decimal)32.5,TaxPercentage = 35 }  },
                StartDate = new DateTime(1999, 3, 16),
                EndDate = new DateTime(1999, 3, 23),
                IsPaid = false,
                TotalNetAmount = 105.5M,
                TotalGrossAmount = 150,
            });

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(expectedValue, values);
        }
        [Fact]
        public void GetPayment_ShouldReturn404()
        {
            unitOfWorkMock.Setup(repo => repo.Employee.GetAll())
                .Returns((List<Employee>)null);
            unitOfWorkMock.Setup(repo => repo.Payment.GetUnpaidWeek()).Returns((Week)null);

            var response = (ObjectResult)_paymentController.GetPayment();

            Assert.Equal(StatusCodes.Status404NotFound, response.StatusCode);
        }
        [Fact]
        public void UpdatePaymentStatus_ShouldReturn204()
        {
            unitOfWorkMock.Setup(repo => repo.Payment.GetByDate(new DateTime(2000, 3, 16))).Returns(new Week()
            {
                StartDate = new DateTime(2000, 3, 16),
                EndDate = new DateTime(2000, 3, 23),
                IsPaid = false,
                TotalGrossAmount = 100,
                TotalNetAmount = 50,
                WeekId = 1,
            });

            var response = (NoContentResult)_paymentController.UpdatePaymentStatus(new DateTime(2000, 3, 16));

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
        [Fact]
        public void UpdatePaymentStatus_ShouldReturn400()
        {
            unitOfWorkMock.Setup(repo => repo.Payment.GetByDate(new DateTime(2000, 3, 16))).Returns(null as Week);

            var response = (ObjectResult)_paymentController.UpdatePaymentStatus(new DateTime(2000, 3, 16));

            Assert.Equal(StatusCodes.Status400BadRequest, response.StatusCode);
        }
        [Fact]
        public void GetLastPayment_ShouldReturn200()
        {
            unitOfWorkMock.Setup(repo => repo.Payment.GetUnpaidWeek()).Returns(null as Week);
            unitOfWorkMock.Setup(repo => repo.Payment.GetAll()).Returns(new List<Week>() {new Week()
            {
                StartDate = new DateTime(2000, 3, 16),
                EndDate = new DateTime(2000, 3, 23),
                IsPaid = false,
                TotalGrossAmount = 100,
                TotalNetAmount = 50,
                WeekId = 1,
            } });

            var response = (ObjectResult)_paymentController.GetLastPayment();
            var value = JsonConvert.SerializeObject(response.Value);

            var exprectedResult = JsonConvert.SerializeObject(new Week()
            {
                StartDate = new DateTime(2000, 3, 16),
                EndDate = new DateTime(2000, 3, 23),
                IsPaid = false,
                TotalGrossAmount = 100,
                TotalNetAmount = 50,
                WeekId = 1,
            });

            Assert.Equal(StatusCodes.Status200OK, response.StatusCode);
            Assert.Equal(exprectedResult, value);
        }
        [Fact]
        public void GetLastPayment_ShouldReturn204()
        {
            unitOfWorkMock.Setup(repo => repo.Payment.GetUnpaidWeek()).Returns(
                new Week()
                {
                    StartDate = new DateTime(2000, 3, 16),
                    EndDate = new DateTime(2000, 3, 23),
                    IsPaid = false,
                    TotalGrossAmount = 100,
                    TotalNetAmount = 50,
                    WeekId = 1,
                });

            unitOfWorkMock.Setup(repo => repo.Payment.GetAll()).Returns(null as List<Week>);

            var response = (NoContentResult)_paymentController.GetLastPayment();

            Assert.Equal(StatusCodes.Status204NoContent, response.StatusCode);
        }
    }
}
