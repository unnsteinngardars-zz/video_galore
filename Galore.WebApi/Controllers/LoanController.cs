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
        public IActionResult getAllLoansForUser(int userId)
        {
            return Ok();
        }

        [HttpPost]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult registerTapeOnLoan(int userId, int tapeId)
        {
            return Ok();
        }

        [HttpDelete]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult returnLoanedTape(int userId, int tapeId)
        {
            return Ok();
        }

        [HttpPut]
        [Route("users/{userId:int}/tapes/{tapeId:int}")]
        public IActionResult updateBorrowingInformation(int userId, int tapeId)
        {
            return Ok();
        }

    }
}