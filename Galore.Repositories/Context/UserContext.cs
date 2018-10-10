using Galore.Models.User;
using Microsoft.EntityFrameworkCore;

namespace Galore.Repositories.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options) {

        }

        public DbSet<User> UserItems { get; set; }
    }
}