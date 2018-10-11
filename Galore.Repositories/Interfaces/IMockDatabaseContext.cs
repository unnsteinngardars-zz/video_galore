using System.Collections.Generic;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Models.User;

namespace Galore.Repositories.Interfaces
{
    public interface IMockDatabaseContext
    {
        List<User> getAllUsers { get; set; }
        List<Tape> getAllTapes { get; set; }
        List<Loan> getAllLoans { get; set; }
    }
}