using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WEB_3_Accountant.Controllers;
using WEB_3_Accountant.Data.Interfaces;
using WEB_3_Accountant.Models;

namespace WEB_3_Accountant.tests
{
    public class HomeControllerShould
    {
        private HomeController homeController;
        public HomeControllerShould()
        {
            var unitOfWorkMock = new Mock<IUnitOfWork>();

            List<WeekInfoModel> weeks = new List<WeekInfoModel>();
            unitOfWorkMock.Setup(w => w.Home.GetWeekInfo()).Returns(weeks);

            homeController = new HomeController(unitOfWorkMock.Object);
        }

        [Fact]
        public void GetWeekInfo_ReturnsBadRequest_WhenThereAreNoWeeks()
        {
            var responseResult = (ObjectResult)homeController.GetWeekInfo();

            Assert.Equal(StatusCodes.Status400BadRequest, responseResult.StatusCode);
        }
    }
}
