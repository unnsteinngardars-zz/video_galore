using Galore.Models.Exceptions;
using Galore.Models.Loan;
using Galore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/")]
    public class LoanController : Controller
    {
        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }

        ///<summary>Get all tapes on loan for user</summary>
        [HttpGet]
        [Route("users/{userId:int}/tapes")]
        public IActionResult GetTapesOnLoanForUser(int userId)
        {
            return Ok(_loanService.GetTapesOnLoanForUser(userId));
        }

        ///<summary>Register a tape on loan for the user</summary>
        [HttpPost]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult RegisterTapeOnLoan(int userId, int tapeId)
        {
            _loanService.RegisterTapeOnLoan(userId, tapeId);
            return NoContent();
        }

        ///<summary>Return a borrowed tape</summary>
        [HttpDelete]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult ReturnLoanedTape(int userId, int tapeId)
        {
            _loanService.ReturnTapeOnLoan(userId, tapeId);
            return NoContent();
        }

        ///<summary>Update the borrowing information</summary>
        [HttpPut]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult UpdateBorrowingInformation([FromBody] LoanInputModel loan, int userId, int tapeId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("Loan was not properly formatted"); }
            _loanService.UpdateTapeOnLoan(loan, userId, tapeId);
            return NoContent();
        }

    }
}