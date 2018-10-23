using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.Tape;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Repositories
{
    [TestClass]
    public class TapeRepositoryTest
    {
        private ITapeRepository repository;
        private GaloreDbContext _context;


        [TestInitialize]
        public void Initialize()
        {
            // arrange
            var options = new DbContextOptionsBuilder<GaloreDbContext>()
                .UseInMemoryDatabase(databaseName: "Tapes").Options;
            _context = new GaloreDbContext(options);
            repository = new TapeRepository(_context);
        }

        [TestMethod]
        public void CreateTape_ReturnsTapeId()
        {
            // act
            var tape1 = new Tape
            {
                Id = 1,
                Title = "The Shining",
                DirectorFirstName = "Stanley",
                DirectorLastName = "Kubrick",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1980, 10, 5),
                Deleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            var tape2 = new Tape
            {
                Id = 2,
                Title = "The Lion King",
                DirectorFirstName = "Roger",
                DirectorLastName = "Allers",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1994, 12, 2),
                Deleted = false,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
            var tape1Id = repository.CreateTape(tape1);
            var tape2Id = repository.CreateTape(tape2);

            // assert
            Assert.IsInstanceOfType(tape1Id, typeof(int));
            Assert.AreEqual(1, tape1Id);
            Assert.AreEqual(2, _context.Tapes.Count());
        }

        [TestMethod]
        public void GetAllTapes_ReturnsIEnumerableOfTape()
        {
            // act
            var tapes = repository.GetAllTapes();
            // assert
            Assert.IsInstanceOfType(tapes, typeof(IEnumerable<Tape>));
            Assert.AreEqual(2, tapes.Count());
        }

        [TestMethod]
        public void GetTapeByValidId_ReturnsTape()
        {
            // act
            var tape = repository.GetTapeById(1);
            // assert
            Assert.IsInstanceOfType(tape, typeof(Tape));
            Assert.IsNotNull(tape);
        }

        [TestMethod]
        public void GetTapeByIdInvalidId_ReturnsNull()
        {
            // act
            var tape = repository.GetTapeById(100);
            // assert
            Assert.IsNull(tape);
        }

        [TestMethod]
        public void DeleteTapeById_ReturnsNothing()
        {
            // act & assert
            Assert.AreEqual(2, repository.GetAllTapes().Count());
            var tape = repository.GetTapeById(1);
            repository.DeleteTape(tape);
            Assert.AreEqual(1, repository.GetAllTapes().Count());
            Assert.AreEqual(true, tape.Deleted);
        }


        [TestMethod]
        public void UpdateTape_ReturnsNothing()
        {
            // act & assert
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