using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Repositories
{
    [TestClass]
    public class LoanRepositoryTest
    {
        private GaloreDbContext _context;
        private ILoanRepository repository;
        private IUserRepository userRepository;
        private ITapeRepository tapeRepository;

        [TestInitialize]
        public void Initialize()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GaloreDbContext>()
                .UseInMemoryDatabase(databaseName: "Loans").Options;
            _context = new GaloreDbContext(options);
            repository = new LoanRepository(_context);
            userRepository = new UserRepository(_context);
            tapeRepository = new TapeRepository(_context);

            // add users for testing
            var user2 = new User
                {
                    FirstName = "Asdis Erna",
                    LastName = "Gudmundsdottir",
                    Email = "asdis16@ru.is",
                    Phone = "5885522",
                    Address = "Kopavogsgata 3",
                    Deleted = false,
                };
            var user1 = new User
                {
                    FirstName = "Unnsteinn",
                    LastName = "Gardarsson",
                    Email = "unnsteinn16@ru.is",
                    Phone = "6633819",
                    Address = "Leifsgata 27",
                    Deleted = false,

                };
            var user1Id = userRepository.CreateUser(user1);
            var user2Id = userRepository.CreateUser(user2);
            
            // add tapes for testing
            var tape1 = new Tape
            {
                Title = "The Shining",
                DirectorFirstName = "Stanley",
                DirectorLastName = "Kubrick",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1980, 10, 5),
                Deleted = false,
            };
            var tape2 = new Tape
            {
                Title = "The Lion King",
                DirectorFirstName = "Roger",
                DirectorLastName = "Allers",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1994, 12, 2),
                Deleted = false,

            };
            var tape1Id = tapeRepository.CreateTape(tape1);
            var tape2Id = tapeRepository.CreateTape(tape2);

        }

        [TestMethod]
        public void RegisterTapeOnLoan_ReturnsNothing()
        {
            Assert.AreEqual(0, repository.GetTapesOnLoanForUser(1).Count());
            repository.RegisterTapeOnLoan(1,2);
            Assert.AreEqual(1, repository.GetTapesOnLoanForUser(1).Count());
        }

        [TestMethod]
        public void GetTapesOnLoanForUser_ReturnsIEnumerableOfTape()
        {
            var tapes = repository.GetTapesOnLoanForUser(1);
            Assert.IsInstanceOfType(tapes, typeof(IEnumerable<Tape>));
            Assert.AreEqual(1, tapes.Count());
        }


        // TODO: strengthen this test assertions
        [TestMethod]
        public void ReturnTapeOnLoan_ReturnsNothing()
        {
            Loan loan = new Loan
            {
                UserId = 1,
                TapeId = 3,
                BorrowDate = new DateTime(2018, 4, 15),
                ReturnDate = DateTime.MinValue,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };

            repository.ReturnTapeOnLoan(loan);
        }
        
        // TODO: strengthen this test assertions
        [TestMethod]
        public void UpdateTapeOnLoan_ReturnsNothing()
        {
            Loan loan = new Loan
            {
                BorrowDate = new DateTime(2019, 1, 1)
            };
            repository.UpdateTapeOnLoan(loan);
        }
    }
}