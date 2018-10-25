using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Galore.Models.Loan;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Models.User;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Galore.Repositories.Context
{
    public class GaloreDbContext : DbContext
    {
        public GaloreDbContext(DbContextOptions<GaloreDbContext> options) : base(options) { }
        public DbSet<User> Users { get; set; }
        public DbSet<Tape> Tapes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}