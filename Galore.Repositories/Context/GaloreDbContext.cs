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
        public GaloreDbContext(DbContextOptions<GaloreDbContext> options) : base(options)
        {

            // this.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Tapes ON");

            // this.Database.EnsureCreated();

            //     if(!this.Tapes.Any()) {
            //         var tapes = new List<Tape>();
            //         using(StreamReader r = new StreamReader("./JsonData/Tapes.json")) {
            //             string json = r.ReadToEnd();
            //             tapes = JsonConvert.DeserializeObject<List<Tape>>(json);
            //         }

            //         foreach(var t in tapes) {
            //             Console.WriteLine(t.Title);
            //             this.Tapes.Add(t);
            //         }
            //         this.SaveChanges();
            //     }    

            //     this.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Tapes OFF");
            // this.SeedDatabase();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // var tapes = new List<Tape>();
            // using(StreamReader r = new StreamReader("./JsonData/Tapes.json")) {
            //     string json = r.ReadToEnd();
            //     tapes = JsonConvert.DeserializeObject<List<Tape>>(json);
            // }
            // modelBuilder.Entity<Tape>().HasData(
            //     new Tape 
            //     {
            //         Id = 4,
            //         Title = "Toy Story",
            //         DirectorFirstName = "John",
            //         DirectorLastName = "Lasseter",
            //         Type = "vhs",
            //         EIDR = "10.5240/XXXX-XXXX-XXXX-XXXX-XXXX-C",
            //         ReleaseDate = new DateTime(1995, 11, 19),
            //         Deleted = false,
            //         DateCreated = DateTime.Now,
            //         DateModified = DateTime.Now
            //     }
            // );
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Tape> Tapes { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<Review> Reviews { get; set; }

    }
}