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

        [HttpGet]
        [Route("users/{userId:int}/tapes")]
        public IActionResult GetTapesOnLoanForUser(int userId)
        {
            return Ok(_loanService.GetTapesOnLoanForUser(userId));
        }

        [HttpPost]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult RegisterTapeOnLoan(int userId, int tapeId)
        {   
            _loanService.RegisterTapeOnLoan(userId, tapeId);
            return NoContent();
        }

        [HttpDelete]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult ReturnLoanedTape(int userId, int tapeId)
        {
            _loanService.ReturnTapeOnLoan(userId, tapeId);
            return NoContent();
        }

        [HttpPut]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult UpdateBorrowingInformation([FromBody] LoanInputModel loan, int userId, int tapeId)
        {   
            if(!ModelState.IsValid) { throw new ModelFormatException("Loan was not properly formatted"); }
            _loanService.UpdateTapeOnLoan(loan, userId, tapeId);
            return NoContent();
        }

    }
}