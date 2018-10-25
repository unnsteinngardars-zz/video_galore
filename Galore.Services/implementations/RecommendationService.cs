using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Context;
using Galore.Models.Review;
using Galore.Services.Interfaces;
using AutoMapper;

namespace Galore.Services.Implementations
{
    public class RecommendationService : IRecommendationService
    {
        private readonly IUserService _userService;
        private readonly ITapeService _tapeService;
        private readonly IReviewService _reviewService;

        public RecommendationService(IUserService userService, ITapeService tapeService, IReviewService reviewService)
        {
            _userService = userService;
            _tapeService = tapeService;
            _reviewService = reviewService;
        }
        //Returns random movie the user hasn't seen
        public TapeDetailDTO GetRecommendation(int userId)
        {
            var user = _userService.GetUserById(userId);
            var tapes = _tapeService.GetAllTapes("");
            var reviews = _reviewService.GetAllReviewsForAllTapes();

            var userBorrowedTapeIds = user.BorrowHistory.Select(u => u.TapeId).ToArray();
            var unseenTapes = tapes.Where(t => !userBorrowedTapeIds.Contains(t.Id)).ToList();

            // var reviewedTapeIds = reviews.OrderByDescending(r => r.Score).Select(r => r.TapeId).ToArray();
            // var reviewedTapes = unseenTapes.Where(t => reviewedTapeIds.Contains(t.Id)).ToList();

            // Getting random movie that user hasn't seen
            Random rnd = new Random();
            int randomIndex = rnd.Next(0, unseenTapes.Count() - 1);
            var tapeId = unseenTapes.Select(t => t.Id).ElementAt(randomIndex);
            return _tapeService.GetTapeById(tapeId);
        }
    }
}