using System;
using Galore.Models.Exceptions;
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
        public IActionResult GetAllTapes([FromQuery] string LoanDate = "2000-01-01"){
            return Ok(_tapeService.GetAllTapes());
        }

        [HttpPost]
        [Route("tapes")]
        public IActionResult CreateTape([FromBody] TapeInputModel tape){
            if(!ModelState.IsValid) { throw new ModelFormatException("Tape was not properly formatted"); }
            var newId = _tapeService.CreateTape(tape);
            return Ok(newId);
        }

        [HttpGet]
        [Route("tapes/{tapeId:int}")]
        public IActionResult GetTapeById(int tapeId){
            return Ok(_tapeService.GetTapeById(tapeId));
        }

        [HttpDelete]
        [Route("tapes/{tapeId:int}")]
        public IActionResult DeleteTapeById(int tapeId){
            _tapeService.DeleteTape(tapeId);
            return NoContent();
        }

        [HttpPut]
        [Route("tapes/{tapeId:int}")]
        public IActionResult UpdateTapeById([FromBody] TapeInputModel tape, int tapeId){
            if(!ModelState.IsValid) { throw new ModelFormatException("Tape was not properly formatted"); }
            _tapeService.UpdateTape(tape, tapeId);
            return NoContent();
        }

   

    }
}