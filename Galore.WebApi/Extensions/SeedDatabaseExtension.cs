using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Galore.Models.Loan;
using Galore.Models.Tape;
using Galore.Models.User;
using Galore.Repositories.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Galore.WebApi.Extensions
{
    /**
        SeedDatabaseExtension.cs
        Seeds the database with data from json files
     */
    public static class SeedDatabaseExtension
    {
        public static void SeedDatabase(this GaloreDbContext _dbContext)
        {
            _dbContext.Database.EnsureCreated();

            /* ONLY COMMENT OUT WHEN DELETING ALL DATA FROM DB */
            // _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Reviews");
            // _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Loans");
            // _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Users");    
            // _dbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE Tapes");    
            
            
            /* TAPES */
            _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Tapes ON");
            if (!_dbContext.Tapes.Any())
            {
                var tapes = new List<Tape>();
                using (StreamReader r = new StreamReader("./JsonData/Tapes_NoId.json"))
                {
                    string json = r.ReadToEnd();
                    tapes = JsonConvert.DeserializeObject<List<Tape>>(json);
                }
                _dbContext.AddRange(tapes);
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Tapes OFF");
            }


            /* USERS */
            _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users ON");
            if (!_dbContext.Users.Any())
            {
                var users = new List<User>();
                using (StreamReader r = new StreamReader("./JsonData/Users_NoId.json"))
                {
                    string json = r.ReadToEnd();
                    users = JsonConvert.DeserializeObject<List<User>>(json);
                }
                _dbContext.AddRange(users);
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Users OFF");
            }

            _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Loans ON");
            if (!_dbContext.Loans.Any())
            {
                var loans = new List<Loan>();
                using (StreamReader r = new StreamReader("./JsonData/Loans.json"))
                {
                    string json = r.ReadToEnd();
                    loans = JsonConvert.DeserializeObject<List<Loan>>(json);
                }


                _dbContext.AddRange(loans);
                _dbContext.SaveChanges();
                _dbContext.Database.ExecuteSqlCommand("SET IDENTITY_INSERT Loans OFF");
            }


    
        }
    }
}