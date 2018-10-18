using System.Collections.Generic;
using Galore.Models.User;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using AutoMapper;
using System.Linq;
using Galore.Models.Exceptions;
using System;

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
        public IEnumerable<UserDTO> GetAllUsers(int LoanDuration, string LoanDate)
        {
            List<User> loanUsers;
            var users = _userRepository.GetAllUsers();
            if(LoanDate.Length == 0 && LoanDuration != 0 ) 
            {
                // return list with loan duration query parameters
                loanUsers = new List<User>();
                DateTime now = DateTime.Now.AddDays(LoanDuration*(-1));
                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((l.BorrowDate < now) && l.ReturnDate.Equals(DateTime.MinValue)) || ((int)(l.ReturnDate.Date - l.BorrowDate.Date).TotalDays > LoanDuration ));
               
                foreach(var l in loans) {
                    var user = users.FirstOrDefault(u => u.Id == l.UserId);
                    if(!loanUsers.Contains(user)) {
                        loanUsers.Add(user);
                    }
                }

                return Mapper.Map<IEnumerable<UserDTO>>(loanUsers);

            } 
            else if(LoanDate.Length > 0 && LoanDuration == 0) 
            {
                // return list with only loan date query parameters
                loanUsers = new List<User>();
                DateTime date = DateTime.Parse(LoanDate);
                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((date >= l.BorrowDate && date < l.ReturnDate) || (date >= l.BorrowDate && l.ReturnDate.Equals(DateTime.MinValue))));

                foreach(var l in loans) {
                    var user = users.FirstOrDefault(u => u.Id == l.UserId);
                    if(!loanUsers.Contains(user)) {
                        loanUsers.Add(user);
                    }
                }

                return Mapper.Map<IEnumerable<UserDTO>>(loanUsers);
            } 
            else if(LoanDate.Length > 0 && LoanDuration != 0) 
            {
                // return list with both loan duration and loan date
                loanUsers = new List<User>();
                DateTime date = DateTime.Parse(LoanDate);
                DateTime now = DateTime.Now.AddDays(LoanDuration*(-1));

                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((date >= l.BorrowDate && date < l.ReturnDate) || (date >= l.BorrowDate && l.ReturnDate.Equals(DateTime.MinValue))))
                    .Where(l => ((l.BorrowDate < now) && l.ReturnDate.Equals(DateTime.MinValue)) || ((int)(l.ReturnDate.Date - l.BorrowDate.Date).TotalDays > LoanDuration ));
                
                foreach(var l in loans) {
                    var user = users.FirstOrDefault(u => u.Id == l.UserId);
                    if(!loanUsers.Contains(user)) {
                        loanUsers.Add(user);
                    }
                }

                return Mapper.Map<IEnumerable<UserDTO>>(loanUsers);
            } 

            return Mapper.Map<IEnumerable<UserDTO>>(users);
            
        }

        public int CreateUser(UserInputModel user)
        {
            var users = _userRepository.GetAllUsers();
            var newUser = Mapper.Map<User>(user);
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

        public User IsValidId(int id) {
            var user = _userRepository.GetUserById(id);
            if(user == null) {
                throw new ResourceNotFoundException($"User with id {id} was not found");
            }
            return user;
        }

        
    }
}