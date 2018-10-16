using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Loan;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class LoanRepository : ILoanRepository
    {
        private readonly IMockDatabaseContext _context;

        public LoanRepository(IMockDatabaseContext context)
        {
            _context = context;
        }

        public IEnumerable<Tape> GetTapesOnLoanForUser(int userId) 
        {
            var loans = _context.getAllLoans.Where(l => l.UserId == userId && l.ReturnDate == DateTime.MinValue);
            var tapes = _context.getAllTapes;   
            var result = from l in loans
                        join t in tapes on l.TapeId equals t.Id 
                        select t;
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
            _context.getAllLoans.Add(loan);

        }
        public void ReturnTapeOnLoan(Loan loan) 
        {
            // var loan = _context.getAllLoans.FirstOrDefault(l => l.UserId == userId && l.TapeId == tapeId && l.ReturnDate == DateTime.MinValue);
            loan.ReturnDate = DateTime.Now;
            loan.DateModified = DateTime.Now;

        }
        public void UpdateTapeOnLoan(Loan loan) 
        {   
            // var updateLoan = _context.getAllLoans.FirstOrDefault(l => l.UserId == userId && l.TapeId == tapeId && l.ReturnDate == DateTime.MinValue);
            // updateLoan.BorrowDate = loan.BorrowDate;
            loan.DateModified = DateTime.Now;
        }

        public IEnumerable<Loan> GetAllLoans(){
            return _context.getAllLoans;
        }
    }
}