using Galore.Repositories.Interfaces;
using Galore.Services.implementations;
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

namespace Galore.Tests.Services
{   
    [TestClass]
    public class TapeServiceTest
    {
        // arrange 
        TapeInputModel tape = new TapeInputModel {
            Title = "Test Movie 3",
            DirectorFirstName = "Test first name",
            DirectorLastName = "Test last name",
            Type = "vhs",
            EIDR = "10.5240/AAAA-AAAA-AAAA-AAAA-AAAA-C",
            ReleaseDate = "2018-01-01"
        };

        private Mock<ITapeRepository> _tapeRepository;
        private ITapeService service;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context) {
            // arrange            
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize( c => {
                c.CreateMap<Tape, TapeDTO>();
                c.CreateMap<TapeDTO, Tape>();
                c.CreateMap<Tape, TapeDetailDTO>();
                c.CreateMap<TapeInputModel, Tape>()
                    .ForMember(t => t.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(t => t.DateModified, opt => opt.UseValue(DateTime.Now));
            });
        }      

        [TestInitialize]
        public void initialize() {
            // arrange
            _tapeRepository = new Mock<ITapeRepository>();
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
            service = new TapeService(_tapeRepository.Object);
        }

        [TestMethod]
        public void GetAllTapesTest_ReturnsListOfTwoTapeDTO() {
            // act
            var result = service.GetAllTapes();
            // assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TapeDTO>));
            Assert.AreEqual(2, result.Count());
            _tapeRepository.Verify((m => m.GetAllTapes()), Times.Once());
        }

        [TestMethod]
        public void GetTapeById_ReturnsTapeDetailDTO() {
            // act
            var result = service.GetTapeById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(TapeDetailDTO));
            Assert.AreEqual("Test Movie 1", result.Title);
            _tapeRepository.Verify((m => m.GetTapeById(1)), Times.Once());
        }

        [TestMethod]
        public void CreateTape_ReturnsId() {
            // act
            int result = service.CreateTape(tape);
            // assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(1, result);
            
            _tapeRepository.Verify((m => m.CreateTape(It.IsAny<Tape>())), Times.Once());
        }

        [TestMethod]
        public void DeleteValidTape_ReturnsNothing() {
            // act
            service.DeleteTape(1);
            _tapeRepository.Verify((m => m.GetTapeById(1)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void DeleteInvalidTape_ThrowsResourceNotFoundException() {
            // act
            service.DeleteTape(100);
            _tapeRepository.Verify((m => m.GetTapeById(100)), Times.Once());

        }

        [TestMethod]
        public void UpdateValidTape_ReturnsNothing() {
            // act
            service.UpdateTape(tape, 1);
            _tapeRepository.Verify((m => m.GetTapeById(1)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void UpdateInvalidTape_ThrowsResourceNotFoundException() {
            // act
            service.UpdateTape(tape, 100);
            _tapeRepository.Verify((m => m.GetTapeById(100)), Times.Once());
        }
    }
}