using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Context;
using Galore.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Galore.Repositories.Implementations {
    public class TapeRepository : ITapeRepository {

        private readonly GaloreDbContext _dbContext;

        public TapeRepository (GaloreDbContext dbContext) {
            _dbContext = dbContext;
        }

        //Add a new tape to the database and return the tape id
        public int CreateTape (Tape tape) {
            _dbContext.Tapes.Add (tape);
            _dbContext.SaveChanges ();
            return tape.Id;
        }


        //"Delete" a tape by setting the deleted attribute of the user to true
        public void DeleteTape (Tape tape) {
            var reviews = _dbContext.Reviews.Where(r => r.TapeId == tape.Id);
            _dbContext.Reviews.RemoveRange(reviews);
            tape.Deleted = true;
            _dbContext.SaveChanges ();
        }

        //Return a list of all tapes that have not been deleted
        public IEnumerable<Tape> GetAllTapes () {
            // return _dataContext.getAllTapes.Where(t => t.Deleted == false);
            return _dbContext.Tapes.Where (t => t.Deleted == false).ToList ();
        }

        //Get a specific tape by id(that hasn't been deleted)
        public Tape GetTapeById (int tapeId) {
            return _dbContext.Tapes.Where (t => t.Deleted == false).FirstOrDefault (t => t.Id == tapeId);
        }

        //Update a specific tape by id in the database
        public void UpdateTapeById (Tape tape, int tapeId) {

            var updateTape = _dbContext.Tapes.Where (t => t.Deleted == false).FirstOrDefault (t => t.Id == tapeId);
            updateTape.DateModified = DateTime.Now;
            updateTape.Title = tape.Title;
            updateTape.DirectorFirstName = tape.DirectorFirstName;
            updateTape.DirectorLastName = tape.DirectorLastName;
            updateTape.EIDR = tape.EIDR;
            updateTape.ReleaseDate = tape.ReleaseDate;
            updateTape.Type = tape.Type;
            _dbContext.SaveChanges ();
        }
    }
}