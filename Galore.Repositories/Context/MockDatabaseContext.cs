using System;
using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Review;
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

        private static List<Tape> _tapes = new List<Tape>
        {
            new Tape
            {
                Id = 1,
                Title = "The Shining",
                DirectorFirstName = "Stanley",
                DirectorLastName = "Kubrick",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1980, 10, 5),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Tape
            {
                Id = 2,
                Title = "The Lion King",
                DirectorFirstName = "Roger",
                DirectorLastName = "Allers",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1994, 12, 2),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Tape
            {
                Id = 3,
                Title = "Psycho",
                DirectorFirstName = "Alfred",
                DirectorLastName = "Hitchcock",
                Type = "betamax",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1960, 6, 16),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Tape 
            {
                Id = 4,
                Title = "Toy Story",
                DirectorFirstName = "John",
                DirectorLastName = "Lasseter",
                Type = "vhs",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1995, 11, 19),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Tape
            {
                Id = 5,
                Title = "The Sound of Music",
                DirectorFirstName = "Robert",
                DirectorLastName = "Wise",
                Type = "betamax",
                EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
                ReleaseDate = new DateTime(1065, 3, 2),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        };

        private static List<Loan> _loans = new List<Loan>
        {
            new Loan
            {
                TapeId = 1,
                UserId = 1,
                BorrowDate = new DateTime(2018, 4, 15),
                ReturnDate = DateTime.MinValue,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Loan
            {
                TapeId = 3,
                UserId = 1,
                BorrowDate = new DateTime(2015, 1, 1),
                ReturnDate = new DateTime(2015, 10, 10),
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Loan
            {
                TapeId = 2,
                UserId = 2,
                BorrowDate = new DateTime(2018, 1, 1),
                ReturnDate = DateTime.MinValue,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }
        };

        private static List<Review> _reviews = new List<Review>
        {
            new Review
            {
                Id = 1,
                UserId = 1,
                TapeId = 1,
                Score = 10,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            },
            new Review
            {
                Id = 2,
                UserId = 2,
                TapeId = 2,
                Score = 5,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            }

        };
        

        public List<User> getAllUsers { get => _users; set => _users = value;}
        public List<Tape> getAllTapes { get => _tapes; set => _tapes = value;}
        public List<Loan> getAllLoans { get => _loans; set => _loans = value;}

        public List<Review> getAllReviews { get => _reviews; set => _reviews = value; }
    }
}