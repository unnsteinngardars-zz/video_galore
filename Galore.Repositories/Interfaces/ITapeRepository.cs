using System.Collections.Generic;
using Galore.Models.Review;
using Galore.Models.Tape;

namespace Galore.Repositories.Interfaces
{
    public interface ITapeRepository
    {
        IEnumerable<Tape> GetAllTapes();
        int CreateTape(Tape tape);
        Tape GetTapeById(int tapeId);
        void DeleteTape(Tape tape);
        void UpdateTape(Tape tape, int tapeId);

    }
}