using Galore.Models.Tape;

namespace Galore.Services.Interfaces
{
    //Interface for the Recommendation service class
    public interface IRecommendationService
    {
        TapeDetailDTO GetRecommendation(int userId);
    }
}