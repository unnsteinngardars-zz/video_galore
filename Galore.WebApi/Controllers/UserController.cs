
using Galore.Models.User;
using Microsoft.AspNetCore.Mvc;
using Galore.Models.Loan;
using Galore.Models.Review;
using System;
using Galore.Services.Interfaces;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/")]
    public class UserController : Controller
    {

        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers(){
            return Ok();
        }

        [HttpPost]
        [Route("users")]
        public IActionResult CreateUser([FromBody] UserInputModel user) {
            return Ok();
        }

        [HttpGet]
        [Route("users/{userId:int}")]
        public IActionResult GetUserById(int userId){
            return Ok();
        }

        [HttpDelete]
        [Route("users/{userId:int}")]
        public IActionResult DeleteUserById(int userId){
            return Ok();
        }

        [HttpPut]
        [Route("users/{userId:int}")]
        public IActionResult UpdateUserById([FromBody] UserInputModel user, int userId){
            return Ok();
        }


        [HttpGet]
        [Route("users")]
        public IActionResult GetReportByDate([FromQuery] string LoanDate = "2000-01-01"){
            return Ok();
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetReportByDuration([FromQuery] int LoanDuration = 30){
            return Ok();
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetReportByDurationAndDate([FromQuery] int LoanDuration = 30, [FromQuery] string LoanDate = "2000-01-01"){
            return Ok();
        }

    }
}