using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Context;
using Galore.Models.Review;
using Galore.Services.Interfaces;

namespace Galore.Services.Implementations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUserService _userService;
        private readonly ITapeService _tapeService;
        private readonly IReviewService _reviewService;
        private readonly GaloreDbContext _context;

        public RecommendationService(GaloreDbContext context, IUserService userService, ITapeService tapeService, IReviewService reviewService) {
            _context = context;
            _userService = userService;
            _tapeService = tapeService;
            _reviewService = reviewService;
        }

        public TapeDetailDTO GetRecommendation(int userId) {
            var user = _userService.GetUserById(userId);
            var tapes = _tapeService.GetAllTapes("");
            TapeDetailDTO tapeToReturn;
            var highestTapeScore = 0;
            var alreadyBorrowed = false;

            foreach (var tape in tapes) {
                alreadyBorrowed = false;
                foreach (var loan in user.BorrowHistory) {
                    if(user.Id == loan.UserId) {
                        alreadyBorrowed = true;
                        break;
                    }
                }

                if(alreadyBorrowed) {
                    continue;
                }

                else {
                    var average = 0;
                    var tapeReviews = _reviewService.GetAllReviewsForTape(tape.Id);
                    foreach (var review in tapeReviews)
                    {
                        average += review.Score;
                    }
                    average = average / tapeReviews.Count();
                    if(average > highestTapeScore) {
                        tapeToReturn = _tapeService.GetTapeById(tape.Id);
                        highestTapeScore = average;
                    }
                }
            }
            return tapeToReturn;
        }
    }
}