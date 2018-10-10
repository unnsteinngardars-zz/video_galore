using Galore.Models.Tape;
using Microsoft.EntityFrameworkCore;

namespace Galore.Repositories.Context
{
    public class TapeContext : DbContext
    {
        public TapeContext(DbContextOptions<TapeContext> options) : base(options) {

        }

        public DbSet<Tape> TapeItems { get; set; }
    }
}