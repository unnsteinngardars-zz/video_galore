using Galore.Models.Tape;
using TecRad.Models;

namespace Galore.Models.Recommendation {
    public class RecommendationDTO : HyperMediaModel {
        public int Id { get; set; }
        public int TapeId { get; set; }
    }
}