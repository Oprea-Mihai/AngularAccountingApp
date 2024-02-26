using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_3_Accountant.Controllers;
using WEB_3_Accountant.Data.Interfaces;

namespace WEB_3_Accountant.tests
{
    public class ExcelControllerShould
    {
        private ExcelController excelController;

        public ExcelControllerShould()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();
            unitOfWorkMock.Setup(x => x.Employee.checkExistingEmployee("Andrei")).Returns(true);
            unitOfWorkMock.Setup(x => x.Employee.checkExistingEmployee("Ionut")).Returns(true);
            unitOfWorkMock.Setup(x => x.Employee.checkExistingEmployee("Cristi")).Returns(false);

            excelController = new ExcelController(unitOfWorkMock.Object);
        }

        [Fact]
        public async void CheckNewEmp_ShouldReturn200_WhenGivenNewNames()
        {
            List<string> empList = new List<string> { "Andrei", "Ionut", "Cristi" };
            var responseResult = (ObjectResult)excelController.CheckNewEmp(empList);

            List<string> namesResult = (List<string>)((ObjectResult)excelController.CheckNewEmp(empList)).Value;

            Assert.Equal(StatusCodes.Status200OK, responseResult.StatusCode);
            Assert.Equal("Cristi", namesResult[0]);
        }

    }
}
