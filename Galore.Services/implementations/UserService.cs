using System.Collections.Generic;
using Galore.Models.User;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using AutoMapper;
using System.Linq;
using Galore.Models.Exceptions;
using System;
using Galore.Models.Loan;

namespace Galore.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILoanRepository _loanRepository;

        public UserService(IUserRepository userRepository, ILoanRepository loanRepository)
        {
            _userRepository = userRepository;
            _loanRepository = loanRepository;
        }
        //Return a list of all the users in the system
        //Optional query parameters LoanDuration and LoanDate can be added to narrow down the results
        public IEnumerable<UserDTO> GetAllUsers(int LoanDuration, string LoanDate)
        {
            var users = _userRepository.GetAllUsers();
            if (LoanDate.Length == 0 && LoanDuration != 0)
            {
                // return list with loan duration query parameters
                DateTime now = DateTime.Now.AddDays(LoanDuration * (-1));
                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((l.BorrowDate < now) && l.ReturnDate.Equals(DateTime.MinValue)) || ((int)(l.ReturnDate.Date - l.BorrowDate.Date).TotalDays > LoanDuration));

                return FindUserInLoansList(loans, users);

            }
            else if (LoanDate.Length > 0 && LoanDuration == 0)
            {
                // return list with only loan date query parameters
                DateTime date = DateTime.Parse(LoanDate);
                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((date >= l.BorrowDate && date < l.ReturnDate) || (date >= l.BorrowDate && l.ReturnDate.Equals(DateTime.MinValue))));

                return FindUserInLoansList(loans, users);
            }
            else if (LoanDate.Length > 0 && LoanDuration != 0)
            {
                // return list with both loan duration and loan date
                DateTime date = DateTime.Parse(LoanDate);
                DateTime now = DateTime.Now.AddDays(LoanDuration * (-1));

                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((date >= l.BorrowDate && date < l.ReturnDate) || (date >= l.BorrowDate && l.ReturnDate.Equals(DateTime.MinValue))))
                    .Where(l => ((l.BorrowDate < now) && l.ReturnDate.Equals(DateTime.MinValue)) || ((int)(l.ReturnDate.Date - l.BorrowDate.Date).TotalDays > LoanDuration));

                return FindUserInLoansList(loans, users);
            }

            return Mapper.Map<IEnumerable<UserDTO>>(users);

        }

        //Helper function to get a list of users from the loan list
        private IEnumerable<UserDTO> FindUserInLoansList(IEnumerable<Loan> loans, IEnumerable<User> users)
        {
            List<User> loanUsers = new List<User>();
            foreach (var l in loans)
            {
                var user = users.FirstOrDefault(u => u.Id == l.UserId);
                if (!loanUsers.Contains(user))
                {
                    loanUsers.Add(user);
                }
            }

            return Mapper.Map<IEnumerable<UserDTO>>(loanUsers);
        }

        //Create a new user
        public int CreateUser(UserInputModel user)
        {
            var users = _userRepository.GetAllUsers();
            var newUser = Mapper.Map<User>(user);
            return _userRepository.CreateUser(newUser);

        }

        //Get details about a specific user by id
        //Throws exception if user id is invalid
        public UserDetailDTO GetUserById(int userId)
        {
            var user = IsValidId(userId);
            var loans = _loanRepository.GetAllLoans().Where(l => l.UserId == userId);
            var detailedLoans = Mapper.Map<IEnumerable<LoanDTO>>(loans);
            var detailedUser = Mapper.Map<UserDetailDTO>(user);
            detailedUser.BorrowHistory = detailedLoans.ToList();
            return detailedUser;
        }

        //Delete a valid user, call the delete function from the repository
        //Throws exception if user id is invalid
        public void DeleteUser(int userId)
        {
            var user = IsValidId(userId);
            _userRepository.DeleteUser(user);
        }

        //Update a valid user in the database
        //Throws exception if user id is invalid
        public void UpdateUser(UserInputModel user, int userId)
        {
            var updateUser = IsValidId(userId);
            _userRepository.UpdateUserById(Mapper.Map<User>(user), userId);
        }
        
        //Check if the user id is in the database
        //Throw an exception if it isn't
        public User IsValidId(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                throw new ResourceNotFoundException($"User with id {id} was not found");
            }
            return user;
        }


    }
}