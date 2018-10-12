using System.Collections.Generic;
using Galore.Models.Tape;

namespace Galore.Services.Interfaces
{
    public interface ITapeService
    {
        IEnumerable<TapeDTO> GetAllTapes();
        int CreateTape(TapeInputModel tape);
        TapeDetailDTO GetTapeById(int tapeId);
        void DeleteTape(int tapeId);
        void UpdateTape(TapeInputModel tape, int tapeId);
        IEnumerable<Tape> GetTapesByDate(string date);
    }
}