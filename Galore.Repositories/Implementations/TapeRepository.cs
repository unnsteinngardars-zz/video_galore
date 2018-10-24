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

        public int CreateTape (Tape tape) {
            _dbContext.Tapes.Add (tape);
            _dbContext.SaveChanges ();
            return tape.Id;
        }

        public void DeleteTape (Tape tape) {
            var reviews = _dbContext.Reviews.Where(r => r.TapeId == tape.Id);
            _dbContext.Reviews.RemoveRange(reviews);
            tape.Deleted = true;
            _dbContext.SaveChanges ();
        }

        public IEnumerable<Tape> GetAllTapes () {
            // return _dataContext.getAllTapes.Where(t => t.Deleted == false);
            return _dbContext.Tapes.Where (t => t.Deleted == false).ToList ();
        }

        public Tape GetTapeById (int tapeId) {
            return _dbContext.Tapes.Where (t => t.Deleted == false).FirstOrDefault (t => t.Id == tapeId);
        }

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