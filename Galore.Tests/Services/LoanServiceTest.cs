using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using Galore.Models.Loan;
using Galore.Models.Tape;
using FizzWare.NBuilder;
using System.Collections.Generic;
using System.Linq;
using Galore.Services.Implementations;
using Galore.Models.User;
using Galore.Models.Exceptions;
using System;

namespace Galore.Tests.Services
{
    [TestClass]
    public class LoanServiceTest
    {
        private Mock<ILoanRepository> _loanRepository;
        private Mock<IUserService> _userService;
        private Mock<ITapeService> _tapeService;
        private ILoanService service;
        private int userOneId = 1;
        private int userTwoId = 2;
        private int tapeOneId = 1;
        private int tapeTwoId = 2;
        private int tapeThreeId = 3;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context)
        {
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize(c => {
                c.CreateMap<LoanInputModel, Loan>();
            });
        }

        [TestInitialize]
        public void Initialize()
        {
            _loanRepository = new Mock<ILoanRepository>();
            _userService = new Mock<IUserService>();
            _tapeService = new Mock<ITapeService>();
            
            _loanRepository.Setup(m => m.GetTapesOnLoanForUser(It.IsAny<int>()))
            .Returns(FizzWare.NBuilder.Builder<Tape>
                .CreateListOfSize(2)
                    .IndexOf(0).With(t => t.Id = 1).With(t => t.Title = "Test Movie 1").With(t => t.Type = "vhs")
                    .IndexOf(1).With(t => t.Id = 2).With(t => t.Title = "Test Movie 2").With(t => t.Type = "betamax")
                        .Build()).Verifiable();

            _loanRepository.Setup(m => m.GetAllLoans())
            .Returns(FizzWare.NBuilder.Builder<Loan>
                .CreateListOfSize(2)
                    .IndexOf(0).With(l => l.UserId = userOneId).With(l => l.TapeId = tapeOneId).With(l => l.BorrowDate = new DateTime(2018, 01, 01)).With(l => l.ReturnDate = DateTime.MinValue)
                    .IndexOf(1).With(l => l.UserId = userTwoId).With(l => l.TapeId = tapeTwoId).With(l => l.BorrowDate = new DateTime(2018, 02, 02)).With(l => l.ReturnDate = DateTime.MinValue)
                        .Build());
            
            service = new LoanService(_loanRepository.Object, _userService.Object, _tapeService.Object);
        }


        [TestMethod]
        public void GetTapesOnLoanForUser_ReturnsListOfTwoTapeDTO()
        {
            var result = service.GetTapesOnLoanForUser(It.IsAny<int>());
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TapeDTO>));
            Assert.AreEqual(2, result.Count());
            _loanRepository.Verify((m => m.GetTapesOnLoanForUser(It.IsAny<int>())), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource not found")]
        public void GetTApesOnLoanForUserInvalidUserId_ThrowsResourceNotFoundException()
        {
            _userService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());       
            var result = service.GetTapesOnLoanForUser(userOneId);
        }

        [TestMethod]
        public void RegisterTapeOnLoan_ReturnsNothing()
        {
            service.RegisterTapeOnLoan(userOneId, tapeThreeId);
            _loanRepository.Verify((m => m.RegisterTapeOnLoan(It.IsAny<int>(), It.IsAny<int>())), Times.Once());
        }


        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource not found")]
        public void RegisterTapeOnLoanInvalidUserAndTapeId_ThrowsResourceNotFoundException()
        {
            // Arrange
            _userService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());       
            _tapeService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());
            // Act
            service.RegisterTapeOnLoan(userOneId, tapeOneId);
        }

        [TestMethod]
        [ExpectedException(typeof(LoanException), "Loan Exception")]
        public void RegisterTapeOnLoanThatIsAlreadyLoaned_ThrowsLoanException()
        {
            // Act
            service.RegisterTapeOnLoan(userOneId, tapeOneId);
        }

        [TestMethod]
        public void ReturnTapeOnLoan_ReturnsNothing()
        {
            // Act
            service.ReturnTapeOnLoan(userOneId, tapeOneId);
            // Assert
            _loanRepository.Verify((m => m.ReturnTapeOnLoan(It.IsAny<Loan>())), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource not found")]
        public void ReturnTapeOnLoanInvalidUserIdAndTapeId_ThrowsResourceNotFoundException()
        {
            // Arrange
            _userService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());       
            _tapeService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());
            // Act
            service.ReturnTapeOnLoan(userOneId, tapeOneId);
        }


        [TestMethod]
        [ExpectedException(typeof(LoanException), "Loan Exception")]
        public void ReturnTapeOnLoan_ThrowsLoanException()
        {
            service.ReturnTapeOnLoan(userOneId, tapeThreeId);
        }

        [TestMethod]
        public void UpdateTapeOnLoan_ReturnsNothing()
        {

            service.UpdateTapeOnLoan(new LoanInputModel(), userOneId, tapeOneId);
            _loanRepository.Verify((m => m.UpdateTapeOnLoan(It.IsAny<Loan>())), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource not found")]
        public void UpdateTapeOnLoan_ThrowsResourceNotFoundException()
        {
            _userService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());       
            _tapeService.Setup(m => m.IsValidId(It.IsAny<int>())).Throws(new ResourceNotFoundException());
            service.UpdateTapeOnLoan(new LoanInputModel(), It.IsAny<int>(), It.IsAny<int>());
        }

        [TestMethod]
        [ExpectedException(typeof(LoanException))]
        public void UpdateTapeOnLoan_ThrowsLoanException()
        {
            service.UpdateTapeOnLoan(new LoanInputModel(), userOneId, tapeThreeId);
        }

    }
}