using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/")]
    public class TapeController : Controller
    {

        // TODO: Reports with query parameters

        private readonly ITapeService _tapeService;

        public TapeController(ITapeService tapeService)
        {
            _tapeService = tapeService;
        }

        [HttpGet]
        [Route("tapes")]
        public IActionResult GetAllTapes(){
            return Ok();
        }

        [HttpPost]
        [Route("tapes")]
        public IActionResult CreateTape([FromBody] TapeInputModel tape){
            return Ok();
        }

        [HttpGet]
        [Route("tapes/{tapeId:int}")]
        public IActionResult GetTapeById(int tapeId){
            return Ok();
        }

        [HttpDelete]
        [Route("tapes/{tapeId:int}")]
        public IActionResult DeleteTapeById(int tapeId){
            return NoContent();
        }

        [HttpPut]
        [Route("tapes/{tapeId:int}")]
        public IActionResult UpdateTapeById([FromBody] TapeInputModel tape, int tapeId){
            return NoContent();
        }

        [HttpGet]
        [Route("tapes")]
        public IActionResult GetTapesByDate([FromQuery] string LoanDate = "2000-01-01"){
            return Ok();
        }

    }
}