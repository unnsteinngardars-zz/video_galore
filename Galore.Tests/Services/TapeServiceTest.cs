using Galore.Repositories.Interfaces;
using Galore.Services.implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
// using FizzWare.NBuilder;
using Galore.Models.Tape;

namespace Galore.Tests.Services
{   
    [TestClass]
    public class TapeServiceTest
    {

        private Mock<ITapeRepository> _tapeRepository;
        private TapeService service;

        [TestInitialize]
        public void initialize()
        {
        // _dbContextMock.Setup(method => method.Posts).Returns(FizzWare.NBuilder.Builder<Post>.CreateListOfSize(3).TheFirst(2).With(x => x.Title = "A Grayskull title").TheFirst(1).With(x => x.Author = "Stallman").Build());

            _tapeRepository = new Mock<ITapeRepository>();
            // _tapeRepository.Setup(m => m.GetAllTapes()).Returns(FizzWare.NBuilder.Builder<TapeDTO>.CreateListOfSize(2).Build());

            service = new TapeService(_tapeRepository.Object);
        }

        [TestMethod]
        public void GetAllTapesTest_ReturnsIEnumerableTapeDTO()
        {
        }
    }
}