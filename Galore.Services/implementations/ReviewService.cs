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

        //Gets all reviews that the user has given
        //Throws exception if user id is invalid
        public IEnumerable<ReviewDTO> GetAllReviewsForUser(int userId)
        {
            var user = _userService.IsValidId(userId);
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForUser(userId));
        }

        //Gets a review from a specific user on a tape
        //Throws exception if either user id or tape id are invalid
        public ReviewDTO GetUserReviewForTape(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var review = _repository.GetUserReviewForTape(userId, tapeId);
            if(review == null) { throw new ResourceNotFoundException($"Review for user {userId} and tape {tapeId} does not exist!"); }
            return Mapper.Map<ReviewDTO>(review);
        }

        //Creates a user review for a tape
        //Throws exception if either user id or tape id are invalid
        public int CreateUserReview(ReviewInputModel review, int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);

            var reviews = _repository.GetUserReviewForTape(userId, tapeId);
            if (reviews != null) { throw new AlreadyExistException($"Review for tape {tapeId} by user {userId} already exist!"); }
            var newReview = Mapper.Map<Review>(review);
            return _repository.CreateUserReview(newReview, userId, tapeId);
        }

        //Deletes a user review for a tape
        //Throws exception if either user id or tape id are invalid
        public void DeleteUserReviewForTape(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var review = _repository.GetUserReviewForTape(userId, tapeId);
            if(review == null) { throw new ResourceNotFoundException($"Review for user {userId} and tape {tapeId} does not exist!"); }
            _repository.DeleteUserReviewForTape(review);
        }

        //Updates a user review for a tape
        //Throws exception if either user id or tape id are invalid
        public void UpdateUserReviewForTape(ReviewInputModel review, int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var oldReview = _repository.GetUserReviewForTape(userId, tapeId);
            if(oldReview == null) { throw new ResourceNotFoundException($"Review for user {userId} and tape {tapeId} does not exist!"); }
            _repository.UpdateUserReviewForTape(Mapper.Map<Review>(review), userId, tapeId);
        }

        //Gets a list of all reviews given by all users for all tapes
        public IEnumerable<ReviewDTO> GetAllReviewsForAllTapes()
        {
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForAllTapes());
        }

        //Gets all reviews given for a specific tape
        //Throws exception if tape id is invalid
        public IEnumerable<ReviewDTO> GetAllReviewsForTape(int tapeId)
        {
            var tape = _tapeService.IsValidId(tapeId);
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForTape(tapeId));
        }
    }
}