using System.Collections.Generic;
using Galore.Models.User;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using AutoMapper;
using System.Linq;
using Galore.Models.Exceptions;

namespace Galore.Services.Implementations
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
           return Mapper.Map<IEnumerable<UserDTO>>(_userRepository.GetAllUsers());
        }
        public int CreateUser(UserInputModel user)
        {
            var users = _userRepository.GetAllUsers();
            var nextId = users.Max(u => u.Id) + 1;
            var newUser = Mapper.Map<User>(user);
            newUser.Id = nextId;
            return _userRepository.CreateUser(newUser);

        }
        public UserDetailDTO GetUserById(int userId)
        {
            var user = IsValidId(userId);
            return Mapper.Map<UserDetailDTO>(user);
        }
        public void DeleteUser(int userId)
        {
            var user = IsValidId(userId);
            _userRepository.DeleteUser(user);
        }
        public void UpdateUser(UserInputModel user, int userId)
        {
            var updateUser = IsValidId(userId);
            _userRepository.UpdateUserById(Mapper.Map<User>(user), userId);
        }

        // TODO: Report for admin. L8r
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

        private User IsValidId(int id) {
            var user = _userRepository.GetUserById(id);
            if(user == null) {
                throw new ResourceNotFoundException($"User with id {id} was not found");
            }
            return user;
        }
    }
}