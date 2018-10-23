using Galore.Models.Exceptions;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using Galore.WebApi.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Controllers
{
    [TestClass]
    public class TapeControllerTest
    {

        private Mock<ITapeService> _tapeService;
        private TapeController controller;

        [TestInitialize]
        public void Initialize()
        {
            // arrange
            _tapeService = new Mock<ITapeService>();
            controller = new TapeController(_tapeService.Object);
        }

        [TestMethod]
        public void GetAllTapesTest_ReturnsOk()
        {
            // act
            var result = controller.GetAllTapes();
            // assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void CreateTapeTestValidModel_ReturnsCreatedAtRoute()
        {
            // act
            controller.ModelState.Clear();
            IActionResult actionResult = controller.CreateTape(new TapeInputModel());
            // assert
            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ModelFormatException), "Model not formated correctly")]
        public void CreateTapeTestInvalidModel_ThrowsModelFormatException()
        {
            // act
            controller.ModelState.AddModelError("test", "test");
            controller.CreateTape(new TapeInputModel());
        }

        [TestMethod]
        public void GetTapeByIdTest_ReturnsOk()
        {
            // act
            var result = controller.GetTapeById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void DeleteTapeByIdTest_ReturnsNoContent()
        {
            // act
            var result = controller.DeleteTapeById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        public void UpdateTapeByIdTestValidModel_ReturnsNoContent()
        {
            // act
            var result = controller.UpdateTapeById(new TapeInputModel(), 1);
            // assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelFormatException), "Model not formatted correctly")]
        public void UpdateTapeByIdInvalidModel_ThrowsModelFormatException()
        {
            controller.ModelState.AddModelError("test", "test");
            controller.UpdateTapeById(new TapeInputModel(), 1);
        }
    }
}