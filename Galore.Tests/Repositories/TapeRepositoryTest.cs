using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.Tape;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Repositories
{   
    [TestClass]
    public class TapeRepositoryTest
    {
        private IMockDatabaseContext _context;
        private ITapeRepository repository;


        [TestInitialize]
        public void initialize()
        {   
            _context = new MockDatabaseContext();
            repository = new TapeRepository(_context);
        }

        [TestMethod]
        public void GetAllTapes_ReturnsIEnumerableOfTape()
        {   
            // Act
            IEnumerable<Tape> tapes = repository.GetAllTapes();
            // Assert
            Assert.IsInstanceOfType(tapes, typeof(IEnumerable<Tape>));
            Assert.AreEqual(5, tapes.Count());
        }

        [TestMethod]
        public void GetTapeByValidId_ReturnsTape()
        {
            Tape tape = repository.GetTapeById(1);
            Assert.IsInstanceOfType(tape, typeof(Tape));
            Assert.IsNotNull(tape);
        }

        [TestMethod]
        public void GetTapeByIdInvalidId_ReturnsNull()
        {
            var tape = repository.GetTapeById(100);
            Assert.IsNull(tape);
        }

        [TestMethod]
        public void DeleteTapeById_ReturnsNothing()
        {
            Assert.AreEqual(5, repository.GetAllTapes().Count());
            var tape = repository.GetTapeById(1);
            repository.DeleteTape(tape);
            Assert.AreEqual(4, repository.GetAllTapes().Count());
        }

        [TestMethod]
        public void UpdateTape_ReturnsNothing()
        {
            Tape updatedTape = new Tape
            {
                Title = "Updated Tape",
                DirectorFirstName = "First name",
                DirectorLastName = "Last name",
                Type = "betamax",
                EIDR = "10.5240/AAAA-AAAA-AAAA-AAAA-AAAA-C",
                ReleaseDate = DateTime.Now
            };
            Tape before = repository.GetTapeById(2);
            Assert.AreEqual("The Lion King", before.Title);
            repository.UpdateTapeById(updatedTape, 2);
            Tape after = repository.GetTapeById(2);
            Assert.AreEqual("Updated Tape", after.Title);

        }

    }
}