using Microsoft.AspNetCore.Mvc;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;
using WEB_3_Accountant.Models.ExcelModels;

namespace WEB_3_Accountant.Controllers
{
    [Route("~/api/[controller]")]
    [ApiController]
    public class ExcelController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;
        public ExcelController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost("post")]
        public IActionResult Post(ExcelDTO excel)
        {
            try
            {
                ExcelToEnitities adapter = new ExcelToEnitities();
                adapter.getEntities(excel, _unitOfWork);
                return NoContent();
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost("checkNewEmp")]
        public IActionResult CheckNewEmp(List<string> empNames)
        {
            List<string> newNames = new List<string>(empNames);
            foreach (string name in empNames)
            {
                if (_unitOfWork.Employee.checkExistingEmployee(name) == true)
                    newNames.Remove(name);
            }
            return Ok(newNames);
        }

        [HttpPost("validateExcel")]
        public IActionResult ValidateExcel(ExcelDTO excel)
        {
            ExcelChecker checker = new ExcelChecker(_unitOfWork);

            checker.ValidateExcelTasksAndProjects(excel);

            checker.ValidateExcelCalc(excel);

            if (checker.errorData.Count != 0)
                return Ok(checker.errorData);

            return Ok(true);
        }
    }
}
