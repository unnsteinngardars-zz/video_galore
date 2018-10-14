using Galore.Repositories.Interfaces;
using Galore.Services.Implementations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FizzWare.NBuilder;
using Galore.Models.User;
using System.Linq;
using AutoMapper;
using System.Collections.Generic;
using System;
using Galore.Models.Exceptions;
using Galore.Services.Interfaces;
using Galore.Services.implementations;

namespace Galore.Tests.Services
{
    [TestClass]
    public class UserServiceTest
    {
        // arrange
        UserInputModel user = new UserInputModel {
            FirstName = "Test User First Name",
            LastName = "Test User Last Name",
            Email = "testemail@testing.com",
            Address = "Test Address",
            Phone = "123-4567",
        };

        private Mock<IUserRepository> _userRepository;
        private IUserService service;

        [ClassInitialize]
        public static void MapperInitialize(TestContext context) {
            // arrange
            AutoMapper.Mapper.Reset();
            AutoMapper.Mapper.Initialize( c => {
             c.CreateMap<User, UserDTO>();
                c.CreateMap<UserDTO, User>();
                c.CreateMap<User, UserDetailDTO>();
                c.CreateMap<UserInputModel, User>()
                    .ForMember(u => u.DateCreated, opt => opt.UseValue(DateTime.Now))
                    .ForMember(u => u.DateModified, opt => opt.UseValue(DateTime.Now));
            });
        }      

        [TestInitialize]
        public void Initialize() {
            // arrange
            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(m => m.GetAllUsers())
            .Returns(FizzWare.NBuilder.Builder<User>
                .CreateListOfSize(2)
                    .IndexOf(0).With(u => u.Id = 1).With(u => u.FirstName = "First Name 1").With(u => u.LastName = "Last Name 1")
                    .IndexOf(1).With(u => u.Id = 2).With(u => u.FirstName = "First Name 2").With(u => u.LastName = "Last Name 2")
                        .Build());
                
            _userRepository.Setup(m => m.GetUserById(1))
                .Returns(FizzWare.NBuilder.Builder<User>
                    .CreateNew().With(u => u.Id = 1).With(u => u.FirstName = "First Name 1").With(u => u.LastName = "Last Name 1")
                        .Build());
            _userRepository.Setup(m => m.CreateUser(It.IsAny<User>())).Returns(1);
            service = new UserService(_userRepository.Object);
        }  

        [TestMethod]
        public void GetAllUsersTest_ReturnsListOfTwoUserDTO() {
            // act
            var result = service.GetAllUsers();
            // assert
            Assert.IsInstanceOfType(result, typeof(IEnumerable<UserDTO>));
            Assert.AreEqual(2, result.Count());
            _userRepository.Verify((m => m.GetAllUsers()), Times.Once());
        }

        [TestMethod]
        public void GetUserByIdTest_ReturnsUserDetailDTO() {
            // act
            var result = service.GetUserById(1);
            // assert
            Assert.IsInstanceOfType(result, typeof(UserDetailDTO));
            Assert.AreEqual("First Name 1", result.FirstName);
            _userRepository.Verify((m => m.GetUserById(1)), Times.Once());
        }

        [TestMethod]
        public void CreateUserTest_ReturnsId() {
            // act
            int result = service.CreateUser(user);
            // assert
            Assert.IsInstanceOfType(result, typeof(int));
            Assert.AreEqual(1, result);
            _userRepository.Verify((m => m.CreateUser(It.IsAny<User>())), Times.Once());
        }

        [TestMethod]
        public void DeleteValidUserTest_ReturnsNothing() {
            // act
            service.DeleteUser(1);
            _userRepository.Verify((m => m.GetUserById(1)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void DeleteInvalidUserTest_ThrowsResourceNotFoundException() {
            // act
            service.DeleteUser(100);
            _userRepository.Verify((m => m.GetUserById(100)), Times.Once());
        }

        [TestMethod]
        public void UpdateValidUserTest_ReturnsNothing() {
            // act
            service.UpdateUser(user, 1);
            _userRepository.Verify((m => m.GetUserById(1)), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(ResourceNotFoundException), "Resource was not found")]
        public void UpdateInvalidUserTest_ThrowsResourceNotFoundException() {
            // act
            service.UpdateUser(user, 100);
            _userRepository.Verify((m => m.GetUserById(100)), Times.Once());
        }
    }
}