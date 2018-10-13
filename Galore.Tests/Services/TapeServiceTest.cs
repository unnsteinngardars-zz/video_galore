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

namespace Galore.Tests.Services
{   
    [TestClass]
    public class TapeServiceTest
    {
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
        private TapeService service;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context){
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
        public void initialize()
        {
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
        public void GetAllTapesTest_ReturnsListOfTwoTapeDTO()
        {
            // Act
            IEnumerable<TapeDTO> result = service.GetAllTapes();
            // Assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<TapeDTO>));
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetTapeById_ReturnsTapeDetailDTO(){
            // Act
            TapeDetailDTO result = service.GetTapeById(1);
            // Assert
            Assert.IsInstanceOfType(result, typeof(TapeDetailDTO));
            Assert.AreEqual("Test Movie 1", result.Title);
        }

        [TestMethod]
        public void CreateTape_ReturnsId(){
            // Act
            int result = service.CreateTape(tape);
            // Assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void DeleteValidTape_ReturnsNothing()
        {
            // Act
            service.DeleteTape(1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void DeleteInvalidTape_ThrowsResourceNotFoundException()
        {
            // Act
            service.DeleteTape(100);
        }

        [TestMethod]
        public void UpdateValidTape_ReturnsNothing()
        {
            // Act
            service.UpdateTape(tape, 1);
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void UpdateInvalidTape_ThrowsResourceNotFoundException()
        {
            // Act
            service.UpdateTape(tape, 100);
        }
    }
}