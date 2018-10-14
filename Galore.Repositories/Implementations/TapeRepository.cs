using System;
using System.Collections.Generic;
using System.Linq;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class TapeRepository : ITapeRepository
    {
        private readonly IMockDatabaseContext _dataContext;

        public TapeRepository(IMockDatabaseContext dataContext) {
            _dataContext = dataContext;
        }
        public int CreateTape(Tape tape)
        {
            _dataContext.getAllTapes.Add(tape);
            return tape.Id;
        }

        public void DeleteTape(Tape tape)
        {
            _dataContext.getAllTapes.Remove(tape);
        }

        public IEnumerable<Tape> GetAllTapes()
        {
            return _dataContext.getAllTapes;
        }

        public Tape GetTapeById(int tapeId)
        {
            return _dataContext.getAllTapes.FirstOrDefault(t => t.Id == tapeId);        
         }

        public void UpdateTapeById(Tape tape, int tapeId)
        {

            var updateTape = _dataContext.getAllTapes.FirstOrDefault(t => t.Id == tapeId);
            updateTape.DateModified = DateTime.Now;
            updateTape.Title = tape.Title;
            updateTape.DirectorFirstName = tape.DirectorFirstName;
            updateTape.DirectorLastName = tape.DirectorLastName;
            updateTape.EIDR = tape.EIDR;
            updateTape.ReleaseDate = tape.ReleaseDate;
            updateTape.Type = tape.Type;
        }
    }
}