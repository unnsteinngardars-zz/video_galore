using System.Collections.Generic;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;

namespace Galore.Services.implementations
{
    public class TapeService : ITapeService
    {
        private readonly ITapeRepository _tapeRepository;

        public TapeService(ITapeRepository tapeRepository)
        {
            _tapeRepository = tapeRepository;
        }
        public IEnumerable<TapeDTO> GetAllTapes()
        {
            throw new System.NotImplementedException();
        }
        public int CreateTape(TapeInputModel tape)
        {
            throw new System.NotImplementedException();
        }
        public TapeDetailDTO GetTapeById(int tapeId)
        {
            throw new System.NotImplementedException();
        }
        public void DeleteTape(TapeInputModel tape)
        {
            throw new System.NotImplementedException();
        }
        public void UpdateTape(TapeInputModel tape, int tapeId)
        {
            throw new System.NotImplementedException();
        }
        public IEnumerable<Tape> GetTapesByDate(string date)
        {
            throw new System.NotImplementedException();
        }
    }
}