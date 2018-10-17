using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Galore.Models.User;
using Galore.Repositories.Context;
using Galore.Repositories.Implementations;
using Galore.Repositories.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Galore.Tests.Repositories
{
    [TestClass]
    public class UserRepositoryTest
    {
        private IMockDatabaseContext _context;
        private IUserRepository repository;

        [TestInitialize]
        public void Initialize() {
            // arrange
            _context = new MockDatabaseContext();
            repository = new UserRepository(_context);
        }

        [TestMethod]
        public void GetAllUsersTest_ReturnsIEnumerableOfUser() {
            // act
            var users = repository.GetAllUsers();
            // assert
            Assert.IsInstanceOfType(users, typeof(IEnumerable<User>));
            Assert.AreEqual(2, users.Count());
        }

        [TestMethod]
        public void GetUserByValidIdTest_ReturnsUser() {
            // act
            var user = repository.GetUserById(1);
            // assert
            Assert.IsInstanceOfType(user, typeof(User));
            Assert.IsNotNull(user);
        }

        [TestMethod]
        public void GetUserByInvalidIdTest_ReturnsNull() {
            // act
            var user = repository.GetUserById(100);
            // assert
            Assert.IsNull(user);
        }

        [TestMethod]
        public void DeleteUserByIdTest_ReturnsNothing() {
            // act & assert
            Assert.AreEqual(2, repository.GetAllUsers().Count());
            var user = repository.GetUserById(1);
            repository.DeleteUser(user);
            Assert.AreEqual(1, repository.GetAllUsers().Count());
            Assert.AreEqual(true, user.Deleted);
        }

        [TestMethod]
        public void UpdateUserTest_ReturnsNothinh() {
            // act & assert
            User updatedUser = new User {
                FirstName = "Updated First Name",
                LastName = "Updated Last Name",
                Address = "Updated Address",
                Email = "update@address.com",
                Phone = "321-7654"
            };
            User before = repository.GetUserById(2);
            Assert.AreEqual("Asdis Erna", before.FirstName);
            repository.UpdateUserById(updatedUser, 2);
            User after = repository.GetUserById(2);
            Assert.AreEqual("Updated First Name", after.FirstName);
        }
    }
}