using System.Collections.Generic;
using Galore.Models.Tape;

namespace Galore.Services.Interfaces
{
    //Interface for the tape service class
    public interface ITapeService
    {
        IEnumerable<TapeDTO> GetAllTapes(string LoanDate);
        int CreateTape(TapeInputModel tape);
        TapeDetailDTO GetTapeById(int tapeId);
        void DeleteTape(int tapeId);
        void UpdateTape(TapeInputModel tape, int tapeId);
        Tape IsValidId(int id);
    }
}