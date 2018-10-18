using Galore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Galore.Models.Review;
using Galore.Models.Exceptions;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/")]
    public class ReviewController : Controller  
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        [Route("users/{userId:int}/reviews")]
        public IActionResult GetAllReviewsForUser(int userId)
        {
            return Ok(_reviewService.GetAllReviewsForUser(userId));
        }

        [HttpGet]
        [Route("users/{userId:int}/reviews/{tapeId:int}", Name="GetUserReviewForTape")]
        public IActionResult GetUserReviewForTape(int userId, int tapeId)
        {
            return Ok(_reviewService.GetUserReviewForTape(userId, tapeId));
        }


        [HttpPost]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult CreateUserReview([FromBody] ReviewInputModel review, int userId, int tapeId)
        {
            if(!ModelState.IsValid) { throw new ModelFormatException("Review was not properly formatted"); }
            var newId = _reviewService.CreateUserReview(review, userId, tapeId);
            return CreatedAtRoute("GetUserReviewForTape", new { userId, tapeId }, null);
        }

        [HttpDelete]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult DeleteUserReviewForTape(int userId, int tapeId)
        {
            _reviewService.DeleteUserReviewForTape(userId, tapeId);
            return NoContent();
        }

        [HttpPut]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult UpdateUserReviewForTape([FromBody] ReviewInputModel review, int userId, int tapeId)
        {
            _reviewService.UpdateUserReviewForTape(review, userId, tapeId);
            return NoContent();
        }

        [HttpGet]
        [Route("tapes/reviews")]
        public IActionResult GetAllReviewsForAllTapes()
        {
            return Ok(_reviewService.GetAllReviewsForAllTapes());
        }

        [HttpGet]
        [Route("tapes/{tapeId:int}/reviews")]
        public IActionResult GetAllReviewsForTape(int tapeId)
        {
            return Ok(_reviewService.GetAllReviewsForTape(tapeId));
        }

    }
}