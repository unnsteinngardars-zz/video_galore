
using Galore.Models.User;
using Microsoft.AspNetCore.Mvc;
using Galore.Models.Loan;
using Galore.Models.Review;
using System;
using Galore.Services.Interfaces;
using Galore.Models.Exceptions;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/")]
    public class UserController : Controller
    {

        // TODO: Reports with query parameters

        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        public IActionResult GetAllUsers([FromQuery] int LoanDuration = 0, [FromQuery] string LoanDate = "")
        {

            return Ok(_userService.GetAllUsers(LoanDuration, LoanDate));
        }

        [HttpPost]
        [Route("users")]
        public IActionResult CreateUser([FromBody] UserInputModel user)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("User was not properly formatted"); }
            var newId = _userService.CreateUser(user);
            return CreatedAtRoute("GetUserById", new { userId = newId }, null);
        }

        [HttpGet]
        [Route("users/{userId:int}", Name = "GetUserById")]
        public IActionResult GetUserById(int userId)
        {
            return Ok(_userService.GetUserById(userId));
        }

        [HttpDelete]
        [Route("users/{userId:int}")]
        public IActionResult DeleteUserById(int userId)
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpPut]
        [Route("users/{userId:int}")]
        public IActionResult UpdateUserById([FromBody] UserInputModel user, int userId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("User was not properly formatted"); }
            _userService.UpdateUser(user, userId);
            return NoContent();
        }
    }
}