using Microsoft.AspNetCore.Mvc;
using WEB_3_Accountant.Data.Interfaces;

namespace WEB_3_Accountant.Controllers
{

    [Route("~/api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _repository;

        public HomeController(IUnitOfWork repo)
        {
            this._repository = repo;
        }

        [HttpGet("getWeeks")]
        public IActionResult GetWeekInfo()
        {
            try
            {
                var weeks = _repository.Home.GetWeekInfo();
                if (weeks.Count() == 0)
                    return StatusCode(400,"There are no weeks provided.");

                return Ok(weeks);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}