using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Loan;
using Galore.Models.Recommendation;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Context;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly GaloreDbContext _dbContext;

        public UserRepository(GaloreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //add a user to the database and return the user id
        public int CreateUser(User user)
        {
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return user.Id;
        }

        //"Delete" a user by setting the deleted attribute of the user to true
        public void DeleteUser(User user)
        {
            // var reviews = _dbContext.Reviews.Where(r => r.UserId == user.Id);
            // _dbContext.Reviews.RemoveRange(reviews);
            user.Deleted = true;
            _dbContext.SaveChanges();
        }

        //Return a list of all users that have not been deleted
        public IEnumerable<User> GetAllUsers()
        {
            return _dbContext.Users.Where(u => u.Deleted == false).ToList();
        }

        //Get a specific user by id(that hasn't been deleted)
        public User GetUserById(int userId)
        {
            return _dbContext.Users.Where(u => u.Deleted == false).FirstOrDefault(u => u.Id == userId);
        }

        //Update a specific user by id in the database
        public void UpdateUserById(User user, int userId)
        {
            var updateUser = _dbContext.Users.Where(u => u.Deleted == false).FirstOrDefault(u => u.Id == userId);
            updateUser.FirstName = user.FirstName;
            updateUser.LastName = user.LastName;
            updateUser.Address = user.Address;
            updateUser.Email = user.Email;
            updateUser.Phone = user.Phone;
            updateUser.DateModified = DateTime.Now;
            _dbContext.SaveChanges();
        }
    }
}