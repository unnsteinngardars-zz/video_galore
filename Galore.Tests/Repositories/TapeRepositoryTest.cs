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
        public void Initialize() {   
            // arrange
            _context = new MockDatabaseContext();
            repository = new TapeRepository(_context);
        }

        [TestMethod]
        public void GetAllTapes_ReturnsIEnumerableOfTape() {   
            // act
            var tapes = repository.GetAllTapes();
            // assert
            Assert.IsInstanceOfType(tapes, typeof(IEnumerable<Tape>));
            Assert.AreEqual(5, tapes.Count());
        }

        [TestMethod]
        public void GetTapeByValidId_ReturnsTape() {
            // act
            var tape = repository.GetTapeById(1);
            // assert
            Assert.IsInstanceOfType(tape, typeof(Tape));
            Assert.IsNotNull(tape);
        }

        [TestMethod]
        public void GetTapeByIdInvalidId_ReturnsNull() {
            // act
            var tape = repository.GetTapeById(100);
            // assert
            Assert.IsNull(tape);
        }

        [TestMethod]
        public void DeleteTapeById_ReturnsNothing() {
            // act & assert
            Assert.AreEqual(5, repository.GetAllTapes().Count());
            var tape = repository.GetTapeById(1);
            repository.DeleteTape(tape);
            Assert.AreEqual(4, repository.GetAllTapes().Count());
            Assert.AreEqual(true, tape.Deleted);
        }

        [TestMethod]
        public void CreateTape_ReturnsTapeId()
        {
            Tape tape = new Tape{
                Id = 6,
            };
            var tapeId = repository.CreateTape(tape);
            Assert.IsInstanceOfType(tapeId, typeof(int));
            Assert.AreEqual(6, tapeId);
        }

        [TestMethod]
        public void UpdateTape_ReturnsNothing() {
            // act & assert
            Tape updatedTape = new Tape {
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