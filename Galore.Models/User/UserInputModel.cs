using TecRad.Models;

namespace Galore.Models.User
{
    public class UserInputModel : HyperMediaModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}