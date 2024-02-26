using WEB_3_Accountant.Models;
using Microsoft.AspNetCore.Mvc;
using WEB_3_Accountant.Data.Interfaces;

namespace WEB_3_Accountant.Controllers
{
    [Route("api/payment")]
    [ApiController]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork _repository;

        public PaymentController(IUnitOfWork repo)
        {
            this._repository = repo;
        }

        [HttpGet("unpaid")]
        public IActionResult GetPayment()
        {
            try
            {
                //first unpaid week.
                var currentUnpaidWeek = _repository.Payment.GetUnpaidWeek();

            if (currentUnpaidWeek == null)
                return NotFound("There aren't unpaid weeks!");
            //join weeklycost with employee and get the right employees.
            var employees = _repository.WeeklyCost.GetAllById(currentUnpaidWeek.WeekId).Join(_repository.Employee.GetAll(),
                weeklycost => weeklycost.EmployeeId,
                employee => employee.EmployeeId,
                (weeklycost, employee) => new EmployeeModel()
                {
                    Name = employee.Name,
                    TotalHours = weeklycost.TotalHours,
                    GrossAmount = weeklycost.GrossAmmount,
                    NetAmount = weeklycost.GrossAmmount - (weeklycost.GrossAmmount * employee.TaxPercentage) / 100,
                    TaxPercentage = employee.TaxPercentage
                }
                ).ToList();
            if (employees.Count() == 0)
                return NotFound("This week has not employees!");
            //create weekDTO which is sent through API.
            var paymentsToDisplay = new WeekModel() { Employees = employees,
                StartDate = currentUnpaidWeek.StartDate,
                EndDate = currentUnpaidWeek.EndDate,
                TotalGrossAmount = employees.Sum(x => x.GrossAmount),
                TotalNetAmount = employees.Sum(x => x.NetAmount)
            };

            
                if (paymentsToDisplay == null)
                    return NotFound("There aren't unpaid weeks!");
                return Ok(paymentsToDisplay);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpGet("lastpayment")]
        public IActionResult GetLastPayment()
        {
            try
            {
                var unpaidWeek = _repository.Payment.GetUnpaidWeek();
                
                var weeks = _repository.Payment.GetAll();
                
                if (unpaidWeek == null)
                    return Ok(weeks.Last());
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPut("updatepayment")]
        public IActionResult UpdatePaymentStatus([FromQuery] DateTime startDate)
        {
            var paymentToUpdate = _repository.Payment.GetByDate(startDate);
            
            try
            {
                if (paymentToUpdate is null)
                    return BadRequest($"The item from {startDate} doesn't exist!");
                paymentToUpdate.IsPaid = true;
                _repository.Payment.Update(paymentToUpdate);
                _repository.Complete();

                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

    }
}
