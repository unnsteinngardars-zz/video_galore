using System;
using Galore.Models.Tape;

namespace Galore.Models.Recommendation
{
    public class Recommendation
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TapeId { get; set; }
        public DateTime DateCreated { get; set; }
    }
}