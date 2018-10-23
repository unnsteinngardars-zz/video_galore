using Galore.Repositories.Interfaces;
using Galore.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FizzWare.NBuilder;
using Galore.Models.Tape;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System;
using Galore.Models.Exceptions;
using Galore.Services.Interfaces;
using Galore.Models.Loan;

namespace Galore.Tests.Services
{
    [TestClass]
    public class TapeServiceTest
    {
        // arrange 
        TapeInputModel tape = new TapeInputModel
        {
            Title = "Test Movie 3",
            DirectorFirstName = "Test first name",
            DirectorLastName = "Test last name",
            Type = "vhs",
            EIDR = "10.5240/AAAA-AAAA-AAAA-AAAA-AAAA-C",
            ReleaseDate = "2018-01-01"
        };

        private Mock<ITapeRepository> _tapeRepository;
        private Mock<ILoanRepository> _loanRepository;
        private ITapeService service;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context)
        {
            // arrange            
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize(c =>
            {
                c.CreateMap<Tape, TapeDTO>();
                c.CreateMap<TapeDTO, Tape>();
                c.CreateMap<Tape, TapeDetailDTO>();
                c.CreateMap<TapeInputModel, Tape>()
                    .ForMember(t => t.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(t => t.DateModified, opt => opt.UseValue(DateTime.Now));
            });
        }

        [TestInitialize]
        public void Initialize()
        {
            // arrange
            _tapeRepository = new Mock<ITapeRepository>();
            _loanRepository = new Mock<ILoanRepository>();

            _loanRepository.Setup(m => m.GetAllLoans())
                .Returns(FizzWare.NBuilder.Builder<Loan>
                .CreateListOfSize(2)
                    .IndexOf(0).With(l => l.Id = 1).With(l => l.TapeId = 1).With(l => l.UserId = 1).With(l => l.BorrowDate = new DateTime(2001, 01, 01)).With(l => l.ReturnDate = DateTime.MinValue)
                    .IndexOf(1).With(l => l.Id = 2).With(l => l.TapeId = 2).With(l => l.UserId = 2).With(l => l.BorrowDate = new DateTime(2002, 02, 02)).With(l => l.ReturnDate = new DateTime(2002, 05, 05))
                    .Build());

            _tapeRepository.Setup(m => m.GetAllTapes())
            .Returns(FizzWare.NBuilder.Builder<Tape>
                .CreateListOfSize(2)
                    .IndexOf(0).With(t => t.Id = 1).With(t => t.Title = "Test Movie 1").With(t => t.Type = "vhs")
                    .IndexOf(1).With(t => t.Id = 2).With(t => t.Title = "Test Movie 2").With(t => t.Type = "betamax")
                        .Build());

            _tapeRepository.Setup(m => m.GetTapeById(1))
                .Returns(FizzWare.NBuilder.Builder<Tape>
                    .CreateNew().With(t => t.Id = 1).With(t => t.Title = "Test Movie 1")
                        .Build());
            _tapeRepository.Setup(m => m.CreateTape(It.IsAny<Tape>())).Returns(1);
            service = new TapeService(_tapeRepository.Object, _loanRepository.Object);
        }

        [TestMethod]
        public void GetAllTapesTest_ReturnsListOfTwoTapeDTO()
        {
            // act
            var result = service.GetAllTapes("");
            // assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TapeDTO>));
            Assert.AreEqual(2, result.Count());
            _tapeRepository.Verify((m => m.GetAllTapes()), Times.Once());
        }

        [TestMethod]
        public void GetAllTapesOnLoanDateValid_ReturnsListOfOneTapeDTO()
        {
            // act
            var result = service.GetAllTapes("2005-01-01");
            // assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TapeDTO>));
            Assert.AreEqual(1, result.Count());
            // _loanRepository.Verify((m => m.GetAllLoans()), Times.Once());
            _tapeRepository.Verify((m => m.GetAllTapes()), Times.Once());
        }

        [TestMethod]
        public void GetAllTapesOnLoanDateUnvalid_ReturnsNothing()
        {
            // act
            var result = service.GetAllTapes("2000-01-01");
            //Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TapeDTO>));
            Assert.AreEqual(0, result.Count());
            _tapeRepository.Verify((m => m.GetAllTapes()), Times.Once());
        }
        [TestMethod]
        public void GetTapeById_ReturnsTapeDetailDTO()
        {
            // act
            var result = service.GetTapeById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(TapeDetailDTO));
            Assert.AreEqual("Test Movie 1", result.Title);
            Assert.AreEqual(1, result.BorrowHistory.Count());
            _tapeRepository.Verify((m => m.GetTapeById(1)), Times.Once());
        }

        [TestMethod]
        public void CreateTape_ReturnsId()
        {
            // act
            int result = service.CreateTape(tape);
            // assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(1, result);

            _tapeRepository.Verify((m => m.CreateTape(It.IsAny<Tape>())), Times.Once());
        }

        [TestMethod]
        public void DeleteValidTape_ReturnsNothing()
        {
            // act
            service.DeleteTape(1);
            _tapeRepository.Verify((m => m.GetTapeById(1)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void DeleteInvalidTape_ThrowsResourceNotFoundException()
        {
            // act
            service.DeleteTape(100);
            _tapeRepository.Verify((m => m.GetTapeById(100)), Times.Once());

        }

        [TestMethod]
        public void UpdateValidTape_ReturnsNothing()
        {
            // act
            service.UpdateTape(tape, 1);
            _tapeRepository.Verify((m => m.GetTapeById(1)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void UpdateInvalidTape_ThrowsResourceNotFoundException()
        {
            // act
            service.UpdateTape(tape, 100);
            _tapeRepository.Verify((m => m.GetTapeById(100)), Times.Once());
        }
    }
}