using Galore.Models.Tape;

namespace Galore.Services.Interfaces
{
    public interface IRecommendationService
    {
         /**
            users/userId/recommendation : Get recommendation for userId
          */
        TapeDetailDTO getRecommendation(int userId);
    }
}