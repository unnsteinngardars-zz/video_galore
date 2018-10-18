using Galore.Models.Tape;

namespace Galore.Models.Review {
    public class ReviewDTO {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TapeId { get; set; }
        public int Score { get; set; }
    }
}