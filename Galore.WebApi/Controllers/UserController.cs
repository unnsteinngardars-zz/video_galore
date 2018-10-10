
using Galore.Models.User;
using Microsoft.AspNetCore.Mvc;
using Galore.Models.Loan;
using Galore.Models.Review;
using System;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/users/")]
    public class UserController : Controller
    {

        [HttpGet]
        [Route("")]
        public IActionResult GetAllUsers(){
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetReportByDate([FromQuery] string LoanDate = "2000-01-01"){
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetReportByDuration([FromQuery] int LoanDuration = 30){
            return Ok();
        }

        [HttpGet]
        [Route("")]
        public IActionResult GetReportByDurationAndDate([FromQuery] int LoanDuration = 30, [FromQuery] string LoanDate = "2000-01-01"){
            return Ok();
        }

        [HttpPost]
        [Route("")]
        public IActionResult CreateUser([FromBody] UserInputModel user) {
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}")]
        public IActionResult GetUserById(int userId){
            return Ok();
        }

        [HttpDelete]
        [Route("{userId:int}")]
        public IActionResult DeleteUserById(int userId){
            return Ok();
        }

        [HttpPut]
        [Route("{userId:int}")]
        public IActionResult UpdateUserById([FromBody] UserInputModel user, int userId){
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}/tapes")]
        public IActionResult GetTapesOnLoan(int userId){
            return Ok();
        }

        [HttpPost]
        [Route("{userId:int}/tapes/{tapeId:int}")]
        public IActionResult RegisterTapeLoan([FromBody] LoanInputModel loan, int userId, int tapeId){
            return Ok();
        }

        [HttpDelete]
        [Route("{userId:int}/tapes/{tapeId:int}")]
        public IActionResult ReturnTapeLoan(int userId, int tapeId){
            return Ok();
        }

        [HttpPut]
        [Route("{userId:int}/tapes/{tapeId:int}")]
        public IActionResult UpdateTapeLoan([FromBody] LoanInputModel loan, int userId, int tapeId){
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}/reviews")]
        public IActionResult GetAllReviews(int userId){
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}/reviews/{tapeId:int}")]
        public IActionResult GetUserReviewForTape(int userId, int tapeId){
            return Ok();
        }

        [HttpPost]
        [Route("{userId:int}/reviews/{tapeId:int}")]
        public IActionResult CreateUserReviewForTape([FromBody] ReviewInputModel review, int userId, int tapeId){
            return Ok();
        }

        [HttpDelete]
        [Route("{userId:int}/reviews/{tapeId:int}")]
        public IActionResult DeleteUserReviewForTape(int userId, int tapeId){
            return Ok();
        }

        [HttpGet]
        [Route("{userId:int}/recommendation")]
        public IActionResult GetRecommendation(int userId){
            return Ok();
        }
    }
}