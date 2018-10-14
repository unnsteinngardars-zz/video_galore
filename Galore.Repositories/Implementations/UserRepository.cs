using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Loan;
using Galore.Models.Recommendation;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        public readonly IMockDatabaseContext _dataContext;

        public UserRepository(IMockDatabaseContext dataContext) {
            _dataContext = dataContext;
        }

        public int CreateUser(User user)
        {
            _dataContext.getAllUsers.Add(user);
            return user.Id;
        }

        public void DeleteUser(User user)
        {
            _dataContext.getAllUsers.Remove(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _dataContext.getAllUsers;
        }

        public User GetUserById(int userId)
        {
            return _dataContext.getAllUsers.FirstOrDefault(u => u.Id == userId);
        }

        public void UpdateUserById(User user, int userId)
        {
           var updateUser = _dataContext.getAllUsers.FirstOrDefault(u => u.Id == userId);
           updateUser.FirstName = user.FirstName;
           updateUser.LastName = user.LastName;
           updateUser.Address = user.Address;
           updateUser.Email = user.Email;
           updateUser.Phone = user.Phone;
           updateUser.DateModified = DateTime.Now;
        }
    }
}