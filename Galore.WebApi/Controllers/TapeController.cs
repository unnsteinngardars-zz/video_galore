using Galore.Models.Review;
using Galore.Models.Tape;
using Microsoft.AspNetCore.Mvc;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/tapes/")]
    public class TapeController : Controller
    {

        // TODO: Reports with query parameters

        [HttpGet]
        [Route("")]
        public IActionResult GetAllTapes(){
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetTapesByDate([FromQuery] string LoanDate = "2000-01-01"){
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateTape([FromBody] TapeInputModel tape){
            return Ok();
        }

        [HttpGet]
        [Route("{tapeId:int}")]
        public IActionResult GetTapeById(int tapeId){
            return Ok();
        }

        [HttpDelete]
        [Route("{tapeId:int}")]
        public IActionResult DeleteTapeById(int tapeId){
            return NoContent();
        }

        [HttpPut]
        [Route("{tapeId:int}")]
        public IActionResult UpdateTapeById([FromBody] TapeInputModel tape, int tapeId){
            return NoContent();
        }

        [HttpGet]
        [Route("reviews")]
        public IActionResult GetAllTapeReviews(){
            return Ok();
        }

        [HttpGet]
        [Route("{tapeId:int}/reviews")]
        public IActionResult GetTapeReviewById(int tapeId){
            return Ok();
        }

        [HttpGet]
        [Route("{tapeId:int}/reviews/{userId:int}")]
        public IActionResult GetUserReviewForTape(int tapeId, int userId){
            return Ok();
        }

        [HttpPut]
        [Route("{tapeId:int}/reviews/{userId:int}")]
        public IActionResult UpdateUserReviewForTape([FromBody] ReviewInputModel review, int tapeId, int userId){
            return NoContent();
        }

        [HttpDelete]
        [Route("{tapeId:int}/reviews/{userId:int}")]
        public IActionResult DeleteUserReviewForTape(int tapeId, int userId){
            return NoContent();
        }




    }
}