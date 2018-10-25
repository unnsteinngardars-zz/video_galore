using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;

namespace Galore.Repositories.Interfaces
{
    //Interface to mock the database
    public interface IMockDatabaseContext
    {
        List<User> getAllUsers { get; set; }
        List<Tape> getAllTapes { get; set; }
        List<Loan> getAllLoans { get; set; }
        List<Review> getAllReviews { get; set; }
    }
}