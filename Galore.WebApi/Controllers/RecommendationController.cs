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

        [HttpGet]
        [Route("users/{userId:int}/recommendation")]
        public IActionResult GetRecommendation(int userId)
        {
            return Ok(_recommendationService.GetRecommendation(userId));
        }
    }
}