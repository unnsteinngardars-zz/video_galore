using Galore.Models.Exceptions;
using Galore.Models.User;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using Galore.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        private Mock<IUserService> _userService;
        private UserController controller;

        [TestInitialize]
        public void Initialize()
        {
            // arrange
            _userService = new Mock<IUserService>();
            controller = new UserController(_userService.Object);
        }

        [TestMethod]
        public void GetAllUsersTest_ReturnsOk()
        {
            // act
            var result = controller.GetAllUsers();
            // assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void CreateUserTestValidModel_ReturnsCreatedAtRoute()
        {
            // act
            controller.ModelState.Clear();
            IActionResult actionResult = controller.CreateUser(new UserInputModel());
            // assert
            Assert.IsInstanceOfType(actionResult, typeof(CreatedAtRouteResult));
        }

        [TestMethod]
        [ExpectedException(typeof(ModelFormatException), "Model not properly formatted")]
        public void CreateUserTestInvalidModel_ThrowsModelFormatException()
        {
            // act
            controller.ModelState.AddModelError("test", "test");
            controller.CreateUser(new UserInputModel());
        }

        [TestMethod]
        public void GetUserById_ReturnsOk()
        {
            // act
            var result = controller.GetUserById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void DeleteUserByIdTest_ReturnsNoContent()
        {
            // act
            var result = controller.DeleteUserById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        public void UpdateUserByIdTestValidModel_ReturnsNoContent()
        {
            // act
            var result = controller.UpdateUserById(new UserInputModel(), 1);
            // assert
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelFormatException), "Model not properly formatted")]
        public void UpdateUserByIdTestInvalidModel_ThrowsModelFormatException()
        {
            // act
            controller.ModelState.AddModelError("test", "test");
            controller.UpdateUserById(new UserInputModel(), 1);
        }

    }
}