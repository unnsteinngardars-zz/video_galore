using System;
using System.Collections.Generic;
using AutoMapper;
using System.Linq;
using Galore.Models.Review;
using Galore.Services.Interfaces;
using Galore.Repositories.Interfaces;
using Galore.Models.Exceptions;

namespace Galore.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IUserService _userService;
        private readonly ITapeService _tapeService;

        public ReviewService(IReviewRepository repository, IUserService userService, ITapeService tapeService)
        {
            _repository = repository;
            _userService = userService;
            _tapeService = tapeService;
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForUser(int userId)
        {
            var user = _userService.IsValidId(userId);
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForUser(userId));
        }

        public ReviewDTO GetUserReviewForTape(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            return Mapper.Map<ReviewDTO>(_repository.GetUserReviewForTape(userId, tapeId));
        }

        public int CreateUserReview(ReviewInputModel review, int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);

            var reviews = _repository.GetUserReviewForTape(userId, tapeId);
            if (reviews != null) { throw new AlreadyExistException($"Review for tape {tapeId} by user {userId} already exist!"); }
            var newReview = Mapper.Map<Review>(review);
            return _repository.CreateUserReview(newReview, userId, tapeId);
        }

        public void DeleteUserReviewForTape(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var review = _repository.GetAllReviewsForAllTapes().Where(r => r.UserId == userId && r.TapeId == tapeId).FirstOrDefault();
            _repository.DeleteUserReviewForTape(review);
        }

        public void UpdateUserReviewForTape(ReviewInputModel review, int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            _repository.UpdateUserReviewForTape(Mapper.Map<Review>(review), userId, tapeId);
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForAllTapes()
        {
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForAllTapes());
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForTape(int tapeId)
        {
            var tape = _tapeService.IsValidId(tapeId);
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForTape(tapeId));
        }
    }
}