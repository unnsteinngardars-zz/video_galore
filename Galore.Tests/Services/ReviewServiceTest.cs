using Galore.Repositories.Interfaces;
using Galore.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FizzWare.NBuilder;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System;
using Galore.Models.Exceptions;
using Galore.Services.Interfaces;


namespace Galore.Tests.Services
{
    [TestClass]
    public class ReviewServiceTest
    {
        // arrange
        ReviewInputModel review = new ReviewInputModel {
            Score = 8,
        };

        private Mock<IReviewRepository> _reviewRepository;
        private IReviewService service;
        private Mock<IUserRepository> _userRepository;
        private IUserService uService;
        private Mock<ITapeRepository> _tapeRepository;
        private ITapeService tService;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context) {
            // arrange
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize( c => {
                c.CreateMap<Review, ReviewDTO>();
                c.CreateMap<ReviewDTO, Review>();
                c.CreateMap<ReviewInputModel, Review>()
                    .ForMember(u => u.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(u => u.DateModified, opt => opt.UseValue(DateTime.Now));
            });
        }

        [TestInitialize]
        public void Initialize() {
            //Set User service
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(m => m.GetAllUsers())
            .Returns(FizzWare.NBuilder.Builder<User>
                .CreateListOfSize(2)
                    .IndexOf(0).With(u => u.Id = 1).With(u => u.FirstName = "First Name 1").With(u => u.LastName = "Last Name 1")
                    .IndexOf(1).With(u => u.Id = 2).With(u => u.FirstName = "First Name 2").With(u => u.LastName = "Last Name 2")
                        .Build());
                
            _userRepository.Setup(m => m.GetUserById(1))
                .Returns(FizzWare.NBuilder.Builder<User>
                    .CreateNew().With(u => u.Id = 1).With(u => u.FirstName = "First Name 1").With(u => u.LastName = "Last Name 1")
                        .Build());

            _userRepository.Setup(m => m.GetUserById(2))
                .Returns(FizzWare.NBuilder.Builder<User>
                    .CreateNew().With(u => u.Id = 2).With(u => u.FirstName = "First Name 2").With(u => u.LastName = "Last Name 2")
                        .Build());
            _userRepository.Setup(m => m.CreateUser(It.IsAny<User>())).Returns(1);
            uService = new UserService(_userRepository.Object);

            //Set the tape service
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

            _tapeRepository.Setup(m => m.GetTapeById(2))
                .Returns(FizzWare.NBuilder.Builder<Tape>
                    .CreateNew().With(t => t.Id = 2).With(t => t.Title = "Test Movie 2")
                        .Build());
            _tapeRepository.Setup(m => m.CreateTape(It.IsAny<Tape>())).Returns(1);
            tService = new TapeService(_tapeRepository.Object);

            //Set the ReviewService
            _reviewRepository = new Mock<IReviewRepository>();
            _reviewRepository.Setup(m => m.GetAllReviewsForAllTapes())
            .Returns(FizzWare.NBuilder.Builder<Review>
                .CreateListOfSize(2)
                    .IndexOf(0).With(r => r.Id = 1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.Score = 7)
                    .IndexOf(1).With(r => r.Id = 2).With(r => r.UserId = 2).With(r => r.TapeId = 2).With(r => r.Score = 5)
                        .Build());
            
            _reviewRepository.Setup(m => m.GetAllReviewsForTape(2))
            .Returns(FizzWare.NBuilder.Builder<Review>
                .CreateListOfSize(2)
                    .IndexOf(0).With(r => r.Id = 1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.Score = 7)
                    .IndexOf(1).With(r => r.Id = 2).With(r => r.UserId = 2).With(r => r.TapeId = 2).With(r => r.Score = 5)
                        .Build());
            
            _reviewRepository.Setup(m => m.GetAllReviewsForUser(1))
            .Returns(FizzWare.NBuilder.Builder<Review>
                .CreateListOfSize(1)
                    .IndexOf(0).With(r => r.Id = 1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.Score = 7)
                        .Build()); 

            _reviewRepository.Setup(m => m.GetUserReviewForTape(1, 2))
            .Returns(FizzWare.NBuilder.Builder<Review>
                    .CreateNew().With(r => r.Id = 1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.Score = 7)
                        .Build()); 
            
            service = new ReviewService(_reviewRepository.Object, uService, tService);
        }

        [TestMethod]
        public void GetAllReviewsForUser_ReturnsIEnumerableOfReviewDTO() {
            var result = service.GetAllReviewsForUser(1);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            Assert.AreEqual(1, result.Count());
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }

        [TestMethod]
        public void GetUserReviewForTape_ReturnsReviewDTO() {
            var result = service.GetUserReviewForTape(1, 2);
            Assert.IsInstanceOfType(result, typeof(ReviewDTO));
            Assert.AreEqual(7, result.Score);
        }
        /* 
        [TestMethod]
        public void CreateUserReview_ReturnsReviewId() {
            var result = service.CreateUserReview(review, 1, 1);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(1, result);
            _reviewRepository.Verify((m => m.CreateUserReview(Mapper.Map<Review>(review), 1, 1)), Times.Once());
        }

        [TestMethod]
        public void DeleteUserReview_ReturnsNothing() {
            service.DeleteUserReviewForTape(1, 2);
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }

        [TestMethod]
        public void UpdateUserReview_ReturnsNothing() {
            service.UpdateUserReviewForTape(review, 2, 2);
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }
*/
        [TestMethod]
        public void GetAllReviewForAllTapes_ReturnsIEnumerableOfReviewDTO() {
            var result = service.GetAllReviewsForAllTapes();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            Assert.AreEqual(2, result.Count());
            _reviewRepository.Verify((m => m.GetAllReviewsForAllTapes()), Times.Once());
        }

        [TestMethod]
        public void GetAllReviewForTape_ReturnsIEnumerableOfReviewDTO() {
            var result = service.GetAllReviewsForTape(2);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            Assert.AreEqual(2, result.Count());
            _reviewRepository.Verify((m => m.GetAllReviewsForTape(2)), Times.Once());
        }


    }
}