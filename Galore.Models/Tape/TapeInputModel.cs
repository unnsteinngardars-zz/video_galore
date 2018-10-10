using TecRad.Models;

namespace Galore.Models.Tape
{
    public class TapeInputModel : HyperMediaModel
    {
        public string Title { get; set; }
        public string DirectorFirstName { get; set; }
        public string DirectorLastName { get; set; }
        public string Type { get; set; }
        public string EIDR { get; set; }
        public string ReleaseDate { get; set; }
    }
}