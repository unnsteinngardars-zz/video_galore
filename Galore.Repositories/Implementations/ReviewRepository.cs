using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Context;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly GaloreDbContext _context;

        public ReviewRepository(GaloreDbContext context)
        {
            _context = context;
        }

        // users/userId/reviews : Get all reviews a user has made
        public IEnumerable<Review> GetAllReviewsForUser(int userId)
        {
            var reviews = _context.Reviews.Where(r => r.UserId == userId);
            return reviews.ToList();
        }

        // tapes/tapeId/reviews/userId : Get user review for tape
        // users/userId/reviews/tapeId : Get user review for given tape
        public Review GetUserReviewForTape(int userId, int tapeId)
        {
            var review = _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.TapeId == tapeId);
            return review;
        }

        // users/userId/reviews/tapeId : Add new user review for tape
        public int CreateUserReview(Review review, int userId, int tapeId)
        {
            review.UserId = userId;
            review.TapeId = tapeId;
            review.DateCreated = DateTime.Now;
            review.DateModified = DateTime.Now;
            _context.Reviews.Add(review);
            _context.SaveChanges();
            return review.Id;
        }

        // users/userId/reviews/tapeId : Delete a user review
        // tapes/tapeId/reviews/userId : Delete a user review
        public void DeleteUserReviewForTape(Review review)
        {
            _context.Reviews.Remove(review);
            _context.SaveChanges();
        }

        // users/userId/reviews/tapeId : Update user review
        // tapes/tapeId/reviews/userId : Update user review
        public void UpdateUserReviewForTape(Review review, int userId, int tapeId)
        {
            var UpdatedReview = _context.Reviews.FirstOrDefault(r => r.UserId == userId && r.TapeId == tapeId);
            UpdatedReview.Score = review.Score;
            UpdatedReview.DateModified = DateTime.Now;
            _context.SaveChanges();
        }

        // tapes/reviews : Get reviews for ALL tapes
        public IEnumerable<Review> GetAllReviewsForAllTapes()
        {
            return _context.Reviews.ToList();
        }

        //tapes/tapeId/reviews : Get all reviews for tapeId
        public IEnumerable<Review> GetAllReviewsForTape(int tapeId)
        {
            var reviews = _context.Reviews.Where(r => r.TapeId == tapeId);
            return reviews.ToList();
        }
    }
}