using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Loan;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Context;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class LoanRepository : ILoanRepository
    {
        private readonly GaloreDbContext _dbContext;

        public LoanRepository(GaloreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Tape> GetTapesOnLoanForUser(int userId)
        {
            var loans = _dbContext.Loans.Where(l => l.UserId == userId && l.ReturnDate == DateTime.MinValue);
            var tapes = _dbContext.Tapes.Where(t => t.Deleted == false);
            var result = from l in loans
                         join t in tapes on l.TapeId equals t.Id
                         select t;
            foreach (var t in loans)
            {
                Console.WriteLine(t.Id + " " + t.TapeId + " " + t.UserId);
            }
            return result;
        }
        public void RegisterTapeOnLoan(int userId, int tapeId)
        {
            Loan loan = new Loan
            {
                UserId = userId,
                TapeId = tapeId,
                BorrowDate = DateTime.Now,
                ReturnDate = DateTime.MinValue,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            _dbContext.Loans.Add(loan);
            _dbContext.SaveChanges();

        }
        public void ReturnTapeOnLoan(Loan loan)
        {
            // var loan = _context.getAllLoans.FirstOrDefault(l => l.UserId == userId && l.TapeId == tapeId && l.ReturnDate == DateTime.MinValue);
            loan.ReturnDate = DateTime.Now;
            loan.DateModified = DateTime.Now;
            _dbContext.SaveChanges();

        }
        public void UpdateTapeOnLoan(Loan loan)
        {
            // var updateLoan = _context.getAllLoans.FirstOrDefault(l => l.UserId == userId && l.TapeId == tapeId && l.ReturnDate == DateTime.MinValue);
            // updateLoan.BorrowDate = loan.BorrowDate;
            loan.DateModified = DateTime.Now;
            _dbContext.SaveChanges();
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            return _dbContext.Loans;
        }
    }
}