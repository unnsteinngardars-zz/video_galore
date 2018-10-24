using System.ComponentModel.DataAnnotations;
using TecRad.Models;

namespace Galore.Models.Review
{
    public class ReviewInputModel : HyperMediaModel
    {
        [Required]
        [Range(1,10)]
        public int Score { get; set; }
    }
}