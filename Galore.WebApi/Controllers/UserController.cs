
using Galore.Models.User;
using Microsoft.AspNetCore.Mvc;
using Galore.Models.Loan;
using Galore.Models.Review;
using System;
using Galore.Services.Interfaces;
using Galore.Models.Exceptions;
using System.Collections.Generic;

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

        ///<summary>Get all users</summary>
        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(IEnumerable<UserDTO>), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetAllUsers([FromQuery] int LoanDuration = 0, [FromQuery] string LoanDate = "")
        {

            return Ok(_userService.GetAllUsers(LoanDuration, LoanDate));
        }

        ///<summary>Create a user</summary>
        [HttpPost]
        [Route("users")]
        [ProducesResponseType(typeof(CreatedAtRouteResult), 201)]
        [ProducesResponseType(typeof(ExceptionModel), 412)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult CreateUser([FromBody] UserInputModel user)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("User was not properly formatted"); }
            var newId = _userService.CreateUser(user);
            return CreatedAtRoute("GetUserById", new { userId = newId }, null);
        }

        ///<summary>Get details for user</summary>
        [HttpGet]
        [Route("users/{userId:int}", Name = "GetUserById")]
        [ProducesResponseType(typeof(UserDetailDTO), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetUserById(int userId)
        {
            return Ok(_userService.GetUserById(userId));
        }

        ///<summary>Delete a user</summary>
        [HttpDelete]
        [Route("users/{userId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult DeleteUserById(int userId)
        {
            _userService.DeleteUser(userId);
            return NoContent();
        }

        ///<summary>Update a user</summary>
        [HttpPut]
        [Route("users/{userId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]
        [ProducesResponseType(typeof(ExceptionModel), 412)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult UpdateUserById([FromBody] UserInputModel user, int userId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("User was not properly formatted"); }
            _userService.UpdateUser(user, userId);
            return NoContent();
        }
    }
}