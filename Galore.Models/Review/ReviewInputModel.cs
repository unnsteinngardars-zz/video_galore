using System.ComponentModel.DataAnnotations;
using TecRad.Models;

namespace Galore.Models.Review {
    public class ReviewInputModel : HyperMediaModel
    {
        [Required]
        public int Score { get; set; }
    }
}