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

        IEnumerable<UserDTO> GetReportByDate(string date);

        IEnumerable<UserDTO> GetReportByDuration(int duration);

        IEnumerable<UserDTO> GetReportByDurationAndDate(int duration, string date);

        User IsValidId(int id);
    }
}