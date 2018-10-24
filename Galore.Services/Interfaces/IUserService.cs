using System.Collections.Generic;
using Galore.Models.User;

namespace Galore.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetAllUsers(int LoanDuration, string LoanDate);

        int CreateUser(UserInputModel user);

        UserDetailDTO GetUserById(int userId);

        void DeleteUser(int userId);

        void UpdateUser(UserInputModel user, int userId);

        User IsValidId(int id);
    }
}