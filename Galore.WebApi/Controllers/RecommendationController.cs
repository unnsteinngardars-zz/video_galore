using Galore.Models.Tape;
using Galore.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Galore.WebApi.Controllers
{
    [Route("/v1/api/")]
    public class RecommendationController : Controller
    {
        private readonly IRecommendationService _recommendationService;

        public RecommendationController(IRecommendationService recommendationService)
        {
            _recommendationService = recommendationService;
        }
        ///<summary>Get Recommendation for user</summary>
        [HttpGet]
        [Route("users/{userId:int}/recommendation")]
        [ProducesResponseType(typeof(TapeDetailDTO), 200)]        
        [ProducesResponseType(typeof(NotFoundResult), 404)] 
        public IActionResult GetRecommendation(int userId)
        {
            return Ok(_recommendationService.GetRecommendation(userId));
        }
    }
}