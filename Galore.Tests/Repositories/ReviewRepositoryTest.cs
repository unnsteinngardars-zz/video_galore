using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.User;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FizzWare.NBuilder;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

namespace Galore.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private GaloreDbContext _context;
        private IReviewRepository repository;

        [TestInitialize]
        public void Initialize() {
            // arrange
            var options = new DbContextOptionsBuilder<GaloreDbContext>()
                .UseInMemoryDatabase(databaseName: "Reviews").Options;
            _context = new GaloreDbContext(options);
            repository = new ReviewRepository(_context);
        }

        [TestMethod]
        public void CreateUserReviewTest_ReturnsReviewId() {
            var review1 = new Review {
                Id: 1,
                UserId: 1,
                TapeId: 2,
                Score: 7,
                DateCreated = DateTime.Now;
                DateModified = DateTime.Now;
            };
            var review2 = new Review {
                Id: 2,
                UserId: 2,
                TapeId: 2,
                Score: 6,
                DateCreated = DateTime.Now;
                DateModified = DateTime.Now;
            };
            var reviewId1 = repository.CreateUserReview(review1);
            var reviewId2 = repository.CreateUserReview(review2);

            Assert.IsInstanceOfType(review1, typeof(int));
            Assert.AreEqual(1, reviewId1);
            Assert.AreEqual(2, _context.Reviews.Count());
        }

        [TestMethod]
        public void UpdateUserReviewForTapeTest_ReturnsNothing() {
            // act & assert
            Review updatedReview = new Review {
                Score: 5
            };
            Review before = repository.GetUserReviewForTape(2, 2);
            Assert.AreEqual(6, before.Score);
            repository.UpdateUserReviewForTape(updatedReview, 2, 2);
            Review after = repository.GetUserReviewForTape(2, 2);
            Assert.AreEqual("5", after.Score);
        }

        [TestMethod]
        public void GetAllReviewsForUserTest_ReturnsIEnumerableOfReview() {
            var reviews = repository.GetAllReviewsForUser(1);
            Assert.IsInstanceOfType(reviews, typeof(IEnumerable<Review>));
            Assert.AreEqual(2, reviews.Count());
        }

        [TestMethod]
        public void GetUserReviewForTapeTest_ReturnsReview() {
            var review = repository.GetUserReviewForTape(1, 2);
            Assert.IsInstanceOfType(review, typeof(Review));
            Assert.IsNotNull(review);
        }

        [TestMethod]
        public void GetAllReviewsForAllTapes_ReturnsIEnumerableOfReview()) {
            var reviews = repository.GetAllReviewsForAllTapes();
            Assert.IsInstanceOfType(reviews, typeof(IEnumerable<Review>));
            Assert.AreEqual(2, reviews.Count());
        }

        [TestMethod]
        public void GetAllReviewsForTapeTest_ReturnsIEnumerableOfReview() {
            var reviews = repository.GetAllReviewsForTape(2);
            Assert.IsInstanceOfType(reviews, typeof(IEnumerable<Review>));
            Assert.AreEqual(2, reviews.Count());
        }

        [TestMethod]
        public void DeleteUserReviewTest_ReturnsNothing() {
            Assert.AreEqual(1, repository.GetAllReviewsForUser(1).Count());
            var review = repository.GetUserReviewForTape(1, 2);
            repository.DeleteUserReview(review);
            Assert.AreEqual(0, repository.GetAllReviewsForUser(1).Count());
            Assert.AreEqual(true, review.Deleted);
        }
    }
}