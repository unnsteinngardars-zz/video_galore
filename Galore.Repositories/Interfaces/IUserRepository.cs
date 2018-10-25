using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Recommendation;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;

namespace Galore.Repositories.Interfaces
{
    //Interface for the user repository
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        int CreateUser(User user);
        User GetUserById(int userId);
        void DeleteUser(User user);
        void UpdateUserById(User user, int userId);
    }
}