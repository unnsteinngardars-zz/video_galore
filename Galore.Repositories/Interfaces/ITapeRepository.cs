using System.Collections.Generic;
using Galore.Models.Review;
using Galore.Models.Tape;

namespace Galore.Repositories.Interfaces
{
    //Interface for the tape repository
    public interface ITapeRepository
    {
        IEnumerable<Tape> GetAllTapes();
        int CreateTape(Tape tape);
        Tape GetTapeById(int tapeId);
        void DeleteTape(Tape tape);
        void UpdateTapeById(Tape tape, int tapeId);

    }
}