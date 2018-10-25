using System;
using System.Collections.Generic;
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
        private readonly ITapeService _tapeService;

        public TapeController(ITapeService tapeService)
        {
            _tapeService = tapeService;
        }

        ///<summary>Get all tapes</summary>
        [HttpGet]
        [Route("tapes")]
        [ProducesResponseType(typeof(IEnumerable<TapeDTO>), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)] 
        public IActionResult GetAllTapes([FromQuery] string LoanDate = "")
        {
            return Ok(_tapeService.GetAllTapes(LoanDate));
        }

        ///<summary>Create a tape</summary>
        [HttpPost]
        [Route("tapes")]
        [ProducesResponseType(typeof(CreatedAtRouteResult), 201)]
        [ProducesResponseType(typeof(ExceptionModel), 412)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult CreateTape([FromBody] TapeInputModel tape)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("Tape was not properly formatted"); }
            var newId = _tapeService.CreateTape(tape);
            return CreatedAtRoute("GetTapeById", new { tapeId = newId }, null);
        }

        ///<summary>Get details for tape</summary>
        [HttpGet]
        [Route("tapes/{tapeId:int}", Name = "GetTapeById")]
        [ProducesResponseType(typeof(TapeDetailDTO), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult GetTapeById(int tapeId)
        {
            return Ok(_tapeService.GetTapeById(tapeId));
        }

        ///<summary>Delete a tape</summary>
        [HttpDelete]
        [Route("tapes/{tapeId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult DeleteTapeById(int tapeId)
        {
            _tapeService.DeleteTape(tapeId);
            return NoContent();
        }

        ///<summary>Update a tape</summary>
        [HttpPut]
        [Route("tapes/{tapeId:int}")]
        [ProducesResponseType(typeof(NoContentResult), 204)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult UpdateTapeById([FromBody] TapeInputModel tape, int tapeId)
        {
            if (!ModelState.IsValid) { throw new ModelFormatException("Tape was not properly formatted"); }
            _tapeService.UpdateTape(tape, tapeId);
            return NoContent();
        }



    }
}