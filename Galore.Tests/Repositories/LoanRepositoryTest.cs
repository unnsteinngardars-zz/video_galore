using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Repositories
{
    [TestClass]
    public class LoanRepositoryTest
    {
        private IMockDatabaseContext _context;
        private ILoanRepository repository;

        [TestInitialize]
        public void Initialize()
        {
            _context = new MockDatabaseContext();
            repository = new LoanRepository(_context);
        }

        [TestMethod]
        public void GetTapesOnLoanForUser_ReturnsIEnumerableOfTape()
        {
            var tapes = repository.GetTapesOnLoanForUser(1);
            Assert.IsInstanceOfType(tapes, typeof(IEnumerable<Tape>));
            Assert.AreEqual(1, tapes.Count());
        }

        [TestMethod]
        public void RegisterTapeOnLoan_ReturnsNothing()
        {
            Loan loan = new Loan{
                UserId = 1,
                TapeId = 3,
                BorrowDate = new DateTime(2018,1, 1),
                ReturnDate = DateTime.MinValue
            };
            Assert.AreEqual(1, repository.GetTapesOnLoanForUser(1).Count());
            repository.RegisterTapeOnLoan(1,3);
            Assert.AreEqual(2, repository.GetTapesOnLoanForUser(1).Count());
        }

        [TestMethod]
        public void ReturnTapeOnLoan_ReturnsNothing()
        {
            Assert.AreEqual(2, repository.GetTapesOnLoanForUser(1).Count());
            repository.ReturnTapeOnLoan(1,3);
            Assert.AreEqual(1, repository.GetTapesOnLoanForUser(1).Count());
        }

        // [TestMethod]
        // public void UpdateTapeOnLoan_ReturnsNothing()
        // {
        //     LoanInputModel loan = new LoanInputModel
        //     {
        //         BorrowDate = "2019-01-01"
        //     };

        //     var before = repository.GetTapesOnLoanForUser(1);
        //     repository.UpdateTapeOnLoan(Mapper.Map<Loan>(loan), 1, 1);
        //     var after = repository.GetTapesOnLoanForUser(1);
        //     Assert.AreEqual(before, after);
        // }
    }
}