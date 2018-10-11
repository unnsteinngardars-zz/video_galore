using System;
using System.Collections.Generic;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Context
{
    public class MockDatabaseContext : IMockDatabaseContext
    {
        private static List<User> _users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Unnsteinn",
                LastName = "Gardarsson",
                Email = "unnsteinn16@ru.is",
                Phone = "6633819",
                Address = "Leifsgata 27",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now,
            },
            new User
            {
                Id = 2,
                FirstName = "Asdis Erna",
                LastName = "Gudmundsdottir",
                Email = "asdis16@ru.is",
                Phone = "5885522",
                Address = "Kopavogsgata 3",
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        };

        // private static List<Tape> _tapes = new List<Tape>
        // {

        // }

        public List<User> getAllUsers { get => _users; set => _users = value;}
    }
}