using Galore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult getAllReviewsByUser(int userId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult getUserReviewForSingleTape(int userId, int tapeId)
        {
            return Ok();
        }


        [HttpPost]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult addUserReviewForTape(int userId, int tapeId)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult removeUserReviewForTape(int userId, int tapeId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        public IActionResult updateUserReviewForTape(int userId, int tapeId)
        {
            return Ok();
        }

        [HttpGet]
        [Route("tapes/reviews")]
        public IActionResult getReviewsForAllTapes()
        {
            return Ok();
        }

        [HttpGet]
        [Route("tapes/{tapeId:int}/reviews")]
        public IActionResult getAllReviewsForTape(int tapeId)
        {
            return Ok();
        }

    }
}