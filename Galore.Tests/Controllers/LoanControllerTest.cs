using Galore.Models.Exceptions;
using Galore.Models.Loan;
using Galore.Services.Interfaces;
using Galore.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Controllers
{
    [TestClass]
    public class LoanControllerTest
    {
        private Mock<ILoanService> _loanService;
        private LoanController controller;

        [TestInitialize]
        public void Initialize()
        {
            _loanService = new Mock<ILoanService>();
            controller = new LoanController(_loanService.Object);
        }

        [TestMethod]
        public void GetTapesOnLoanForUser_ReturnsOk()
        {
            var result = controller.GetTapesOnLoanForUser(1);
            Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            var actual = result as OkObjectResult;
            Assert.AreEqual(200, actual.StatusCode);
        }

        [TestMethod]
        public void RegisterTapeOnLoan_ReturnsNoContent()
        {
            var result = controller.RegisterTapeOnLoan(1, 1);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        public void ReturnLoanedTape_ReturnsNoContent()
        {
            var result = controller.RegisterTapeOnLoan(1, 1);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        public void UpdateBorrowingInformationValidModel_ReturnsNoContent()
        {
            var result = controller.UpdateBorrowingInformation(new LoanInputModel(), 1, 1);
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var actual = result as NoContentResult;
            Assert.AreEqual(204, actual.StatusCode);
        }

        [TestMethod]
        [ExpectedException(typeof(ModelFormatException), "Model not properly formatted")]
        public void UpdateBorrowingInformationInvalidModel()
        {
            controller.ModelState.AddModelError("test", "test");
            controller.UpdateBorrowingInformation(new LoanInputModel(), 1, 1);
        }


    }
}