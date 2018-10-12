using System.Collections.Generic;
using Galore.Models.User;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;

namespace Galore.Services.implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<UserDTO> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }
        public int CreateUser(UserInputModel user)
        {
            throw new System.NotImplementedException();
        }
        public UserDetailDTO GetUserById(int userId)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteUser(int userId)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateUser(UserInputModel user, int userId)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserDTO> GetReportByDate(string date)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserDTO> GetReportByDuration(int duration)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<UserDTO> GetReportByDurationAndDate(int duration, string date)
        {
            throw new System.NotImplementedException();
        }
    }
}