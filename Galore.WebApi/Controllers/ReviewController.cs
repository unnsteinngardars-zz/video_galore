using Galore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Galore.Models.Review;
using Galore.Models.Exceptions;
using System.Collections.Generic;

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

        ///<summary>Get all reviews for user</summary>
        [HttpGet]
        [Route("users/{userId:int}/reviews")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDTO>), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetAllReviewsForUser(int userId)
        {
            return Ok(_reviewService.GetAllReviewsForUser(userId));
        }

        ///<summary>Get a user review for a tape</summary>
        [HttpGet]
        [Route("users/{userId:int}/reviews/{tapeId:int}", Name = "GetUserReviewForTape")]
        [ProducesResponseType(typeof(ReviewDTO), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetUserReviewForTape(int userId, int tapeId)
        {
            return Ok(_reviewService.GetUserReviewForTape(userId, tapeId));
        }

        ///<summary>Create a user review for a tape</summary>
        [HttpPost]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        [ProducesResponseType(typeof(CreatedAtRouteResult), 201)]
        [ProducesResponseType(typeof(ExceptionModel), 412)]
        [ProducesResponseType(typeof(ExceptionModel), 422)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult CreateUserReview([FromBody] ReviewInputModel review, int userId, int tapeId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("Review was not properly formatted"); }
            var newId = _reviewService.CreateUserReview(review, userId, tapeId);
            return CreatedAtRoute("GetUserReviewForTape", new { userId, tapeId }, null);
        }

        ///<summary>Delete a user review</summary>
        [HttpDelete]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult DeleteUserReviewForTape(int userId, int tapeId)
        {
            _reviewService.DeleteUserReviewForTape(userId, tapeId);
            return NoContent();
        }

        ///<summary>Update a user review</summary>
        [HttpPut]
        [Route("users/{userId:int}/reviews/{tapeId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(ExceptionModel), 412)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult UpdateUserReviewForTape([FromBody] ReviewInputModel review, int userId, int tapeId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("Review was not properly formatted"); }
            _reviewService.UpdateUserReviewForTape(review, userId, tapeId);
            return NoContent();
        }

        ///<summary>Get all user reviews for all tapes</summary>
        [HttpGet]
        [Route("tapes/reviews")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDTO>), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetAllReviewsForAllTapes()
        {
            return Ok(_reviewService.GetAllReviewsForAllTapes());
        }

        ///<summary>Get all user reviews for a tape</summary>
        [HttpGet]
        [Route("tapes/{tapeId:int}/reviews")]
        [ProducesResponseType(typeof(IEnumerable<ReviewDTO>), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetAllReviewsForTape(int tapeId)
        {
            return Ok(_reviewService.GetAllReviewsForTape(tapeId));
        }

        ///<summary>Get a user review for a tape</summary>
        [HttpGet]
        [Route("tapes/{tapeId:int}/reviews/{userId:int}")]
        [ProducesResponseType(typeof(ReviewDTO), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetUserReviewForTape2(int tapeId, int userId)
        {
            return Ok(_reviewService.GetUserReviewForTape(userId, tapeId));
        }

        ///<summary>Delete a user review</summary>
        [HttpDelete]
        [Route("tapes/{tapeId:int}/reviews/{userId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult DeleteUserReviewForTape2(int tapeId, int userId)
        {
            _reviewService.DeleteUserReviewForTape(userId, tapeId);
            return NoContent();
        }

        ///<summary>Update a user review</summary>
        [HttpPut]
        [Route("tapes/{tapeId:int}/reviews/{userId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(ExceptionModel), 412)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult UpdateUserReviewForTape2([FromBody] ReviewInputModel review, int tapeId, int userId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("Review was not properly formatted"); }
            _reviewService.UpdateUserReviewForTape(review, userId, tapeId);
            return NoContent();
        }
    }
}