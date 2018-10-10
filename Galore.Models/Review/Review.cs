using System;
using Galore.Models.Tape;

namespace Galore.Models.Review {
    public class Review {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TapeId { get; set; }
        public double Score { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}