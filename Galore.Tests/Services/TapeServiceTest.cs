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

namespace Galore.Tests.Services
{   
    [TestClass]
    public class TapeServiceTest
    {

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
        // _dbContextMock.Setup(method => method.Posts)
        // .Returns(FizzWare.NBuilder.Builder<Post>
        // .CreateListOfSize(3)
        // .TheFirst(2).With(x => x.Title = "A Grayskull title")
        // .TheFirst(1).With(x => x.Author = "Stallman").Build());


            _tapeRepository = new Mock<ITapeRepository>();
            
            _tapeRepository.Setup(m => m.GetAllTapes())
            .Returns(FizzWare.NBuilder.Builder<Tape>
            .CreateListOfSize(2)
            .IndexOf(0).With(t => t.Id = 1).With(t => t.Title = "Test Movie 1").With(t => t.Type = "vhs")
            .IndexOf(1).With(t => t.Id = 2).With(t => t.Title = "Test Movie 2").With(t => t.Type = "betamax")
            .Build());

            _tapeRepository.Setup(m => m.GetTapeById(1))
            .Returns(FizzWare.NBuilder.Builder<Tape>
            .CreateNew().With(t => t.Id = 1).With(t => t.Title = "Test Movie 1").Build());

            service = new TapeService(_tapeRepository.Object);
        }

        [TestMethod]
        public void GetAllTapesTest_ReturnsListOfTwoTapeDTO()
        {
            var result = service.GetAllTapes();
            Console.WriteLine(result);
            Assert.AreEqual(2, result.Count());
        }

        [TestMethod]
        public void GetTapeById_ReturnsTapeDetailDTOWithTitleTestMovie1(){
            var result = service.GetTapeById(1);
            Assert.AreEqual("Test Movie 1", result.Title);
        }

        [TestMethod]
        public void CreateTape_ReturnsId(){
            
        }
    }
}