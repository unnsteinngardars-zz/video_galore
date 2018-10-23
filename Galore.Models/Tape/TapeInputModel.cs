using System.ComponentModel.DataAnnotations;
using TecRad.Models;
using System;

namespace Galore.Models.Tape
{
    public class TapeInputModel : HyperMediaModel
    {
        private string type;
        [Required]
        public string Title { get; set; }

        [Required]
        public string DirectorFirstName { get; set; }

        [Required]
        public string DirectorLastName { get; set; }

        [Required]
        [RegularExpression("vhs|betamax")]
        public string Type
        {
            get
            {
                return this.type;
            }
            set
            {
                this.type = value.ToLower();
            }
        }

        [Required]
        [RegularExpression("10.5240/(([0-9]|[A-Z]|[a-z]){4}-){5}C")]
        public string EIDR { get; set; }

        [Required]
        [RegularExpression("[12][0-9]{3}-([0][123456789]|[1][012])-([012][123456789]|10|20|3[01])")]
        public string ReleaseDate { get; set; }
    }
}