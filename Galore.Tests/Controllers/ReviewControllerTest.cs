using Galore.Models.Exceptions;
using Galore.Models.Review;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using Galore.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTest
    {
        private Mock<IReviewService> _reviewService;
        private ReviewController controller;

        [TestInitialize]
        public void Initialize()
        {
            _reviewService = new Mock<IReviewService>();
            controller = new ReviewController(_reviewService.Object);
        }

        [TestMethod]
        public void GetAllReviewsForUser_ReturnsOk()
        {
            var result = controller.GetAllReviewsForUser(1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void GetUserReviewForTape_ReturnsOk()
        {
            var result = controller.GetUserReviewForTape(1, 1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void CreateUserReviewTestValidModel_ReturnsCreatedAtRoute()
        {
            controller.ModelState.Clear();
            IActionResult actionResult = controller.CreateUserReview(new ReviewInputModel(), 1, 2);
            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ModelFormatException), "Model was not properly formattet")]
        public void CreateUserReviewTestInvalidModel_ThrowsModelFormatException()
        {
            controller.ModelState.AddModelError("test", "test");
            controller.CreateUserReview(new ReviewInputModel(), 1, 100);
        }

        [TestMethod]
        public void DeleteUserReviewForTape_ReturnsNoContent()
        {
            var result = controller.DeleteUserReviewForTape(1, 2);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }
        [TestMethod]
        public void GetAllReviewsForAllTapes_ReturnsOk()
        {
            var result = controller.GetAllReviewsForAllTapes();
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }
        [TestMethod]
        public void GetAllReviewsForTape_ReturnsOk()
        {
            var result = controller.GetAllReviewsForTape(1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }
    }
}