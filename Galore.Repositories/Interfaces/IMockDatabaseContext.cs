using System.Collections.Generic;
using Galore.Models.User;

namespace Galore.Repositories.Interfaces
{
    public interface IMockDatabaseContext
    {
        List<User> getAllUsers { get; set; }
    }
}