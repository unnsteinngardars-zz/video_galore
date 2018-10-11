using System.Collections.Generic;
using Galore.Models.Review;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;

namespace Galore.Repositories.Implementations
{
    public class TapeRepository : ITapeRepository
    {
        public int CreateTape(TapeInputModel tape)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteTape(Tape tape)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Tape> GetAllTapes()
        {
            throw new System.NotImplementedException();
        }

        public Tape GetTapeById(int tapeId)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateTapeById(Tape tape, int tapeId)
        {
            throw new System.NotImplementedException();
        }
    }
}