using System.Collections.Generic;
using AutoMapper;
using Galore.Models.Review;
using Galore.Services.Interfaces;
using Galore.Repositories.Interfaces;

namespace Galore.Services.Implementations
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _repository;
        private readonly IUserService _userService;
        private readonly ITapeService _tapeservice;

        public IEnumerable<ReviewDTO> GetAllReviewsForUser(int userId)
        {
            var user = _userService.IsValidId(userId);
            return Mapper.Map<IEnumerable<ReviewDTO>>(_repository.GetAllReviewsForUser(userId));
        }

        public ReviewDTO GetUserReviewForTape(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeservice.IsValidId(tapeId);
            return Mapper.Map<ReviewDTO>(_repository.GetUserReviewForTape(userId, tapeId);
        }

        public int CreateUserReview(ReviewInputModel review, int userId, int tapeId)
        {
            return 
        }
        
        public void DeleteUserReviewForTape(int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateUserReviewForTape(ReviewInputModel review, int userId, int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForAllTapes()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<ReviewDTO> GetAllReviewsForTape(int tapeId)
        {
            throw new System.NotImplementedException();
        }
    }
}