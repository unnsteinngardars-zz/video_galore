using Galore.Repositories.Interfaces;
using Galore.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FizzWare.NBuilder;
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
            Score: 8
        };

        private Mock<IReviewRepository> _reviewRepository;
        private IReviewService service;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context) {
            // arrange
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize( c => {
                c.CreateMap<Review, ReviewDTO>();
                c.CreateMap<ReviewDTO, Review>();
                c.CreateMap<Review, UserDetailDTO>();
                c.CreateMap<ReviewInputModel, Review>()
                    .ForMember(u => u.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(u => u.DateModified, opt => opt.UseValue(DateTime.Now))
            });
        }

        [TestInitialize]
        public void Initialize() {
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
                .CreateListOfSize(2)
                    .IndexOf(0).With(r => r.Id = 1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.Score = 7)
                        .Build()); 

            _reviewRepository.Setup(m => m.GetUserReviewForTape(2))
            .Returns(FizzWare.NBuilder.Builder<Review>
                .CreateListOfSize(2)
                    .IndexOf(0).With(r => r.Id = 1).With(r => r.UserId = 1).With(r => r.TapeId = 2).With(r => r.Score = 7)
                    .IndexOf(1).With(r => r.Id = 2).With(r => r.UserId = 2).With(r => r.TapeId = 2).With(r => r.Score = 5)
                        .Build()); 
            
            service = new ReviewService(_reviewRepository.Object);
        }

        [TestMethod]
        public void GetAllReviewsForUser_ReturnsIEnumerableOfReviewDTO() {
            var result = service.GetAllReviewsForUser(1);
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            Assert.AreEqual(1, result.Count());
            _reviewRepository.Verify((m => m.GetAllReviewsForUser()), Times.Once());
        }

        [TestMethod]
        public void GetUserReviewForTape_ReturnsReviewDTO() {
            var result = service.GetUserReviewForTape(1, 2);
            Assert.IsInstanceOfType(result, typeof(ReviewDTO));
            Assert.AreEqual(7, result.Score);
        }

        [TestMethod]
        public void CreateUserReview_ReturnsReviewId() {
            var result = service.CreateUserReview(review);
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(1, result);
            _reviewRepository.Verify((m => m.CreateUserReview(It.IsAny<Review>())), Times.Once());
        }
        [TestMethod]
        public void DeleteValidUserReview_ReturnsNothing() {
            service.DeleteUserReview(1, 2);
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }

        [TestMethod]
        public void DeleteInvalidUserReview_ReturnsNothing() {
            service.DeleteUserReview(1, 19);
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }

        [TestMethod]
        public void UpdateValidUserReview_ReturnsNothing() {
            service.UpdateUserReview(review, 2, 2);
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }

        [TestMethod]
        public void UpdateInvalidUserReview_ReturnsNothing() {
            service.UpdateUserReview(review, 2, 100);
            _reviewRepository.Verify((m => m.GetAllReviewsForUser(1)), Times.Once());
        }

        [TestMethod]
        public void GetAllReviewForAllTapes_ReturnsIEnumerableOfReviewDTO() {
            var result = service.GetAllReviewsForAllTapes();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            Assert.AreEqual(2, result.Count());
            _reviewRepository.Verify((m => m.GetAllReviewsForAllTapes()), Times.Once());
        }

        [TestMethod]
        public void GetAllReviewForTape_ReturnsIEnumerableOfReviewDTO() {
            var result = service.GetAllReviewsForTape();
            Assert.IsInstanceOfType(result, typeof(IEnumerable<ReviewDTO>));
            Assert.AreEqual(2, result.Count());
            _reviewRepository.Verify((m => m.GetAllReviewsForTape()), Times.Once());
        }


    }
}