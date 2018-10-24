using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.Exceptions;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;

namespace Galore.Services.Implementations
{
    public class LoanService : ILoanService
    {
        private readonly ILoanRepository _repository;
        private readonly IUserService _userService;
        private readonly ITapeService _tapeService;

        public LoanService(ILoanRepository repository, IUserService userService, ITapeService tapeService)
        {
            _repository = repository;
            _userService = userService;
            _tapeService = tapeService;
        }

        //Gets a list of tapes that the user has borrowed
        public IEnumerable<TapeDTO> GetTapesOnLoanForUser(int userId)
        {
            var user = _userService.IsValidId(userId);
            return Mapper.Map<IEnumerable<TapeDTO>>(_repository.GetTapesOnLoanForUser(userId));
        }

        // user/userId/tapes/tapeId: Register new loan
        public void RegisterTapeOnLoan(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var checkLoan = CheckIfTapeIsBorrowed(tapeId);
            if (checkLoan != null) { throw new LoanException($"Tape with id {tapeId} is currently loaned"); }
            // if (CheckIfTapeIsBorrowed(tapeId) != null) { throw new LoanException($"Tape with id {tapeId} is currently loaned"); }
            var allLoans = _repository.GetAllLoans();
            _repository.RegisterTapeOnLoan(userId, tapeId);
        }

        // user/userId/tapes/tapeId: Return tape on loan
        public void ReturnTapeOnLoan(int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var checkLoan = CheckIfUserHasTape(userId, tapeId);
            if (checkLoan == null) { throw new LoanException($"Tape with id {tapeId} is not registered as loaned for user with id {userId}"); }
            _repository.ReturnTapeOnLoan(checkLoan);
        }

        // user/userId/tapes/tapeId: Update loan information
        public void UpdateTapeOnLoan(LoanInputModel loan, int userId, int tapeId)
        {
            var user = _userService.IsValidId(userId);
            var tape = _tapeService.IsValidId(tapeId);
            var checkLoan = CheckIfUserHasTape(userId, tapeId);
            if (checkLoan == null) { throw new LoanException($"Tape with id {tapeId} is not registered as loaned for user with id {userId}"); }
            var loanFromInput = Mapper.Map<Loan>(loan);
            checkLoan.BorrowDate = loanFromInput.BorrowDate;
            _repository.UpdateTapeOnLoan(checkLoan);
        }
        //Gets a list of all loans
        public IEnumerable<Loan> GetLoans()
        {
            return _repository.GetAllLoans();
        }
        //See if a user has already borrowed the tape
        public Loan CheckIfTapeIsBorrowed(int tapeId)
        {
            return _repository.GetAllLoans().FirstOrDefault(l => l.TapeId == tapeId && l.ReturnDate == DateTime.MinValue);
        }
        //Check if a certain user has a tape currently on loan
        public Loan CheckIfUserHasTape(int userId, int tapeId)
        {
            return _repository.GetAllLoans().FirstOrDefault(l => l.UserId == userId && l.TapeId == tapeId && l.ReturnDate == DateTime.MinValue);
        }

    }
}