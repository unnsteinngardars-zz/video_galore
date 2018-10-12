using System.Collections.Generic;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using AutoMapper;
using System.Linq;

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
            return Mapper.Map<IEnumerable<TapeDTO>>(_tapeRepository.GetAllTapes());
        }
        public int CreateTape(TapeInputModel tape)
        {
            var tapes = _tapeRepository.GetAllTapes();
            var nextId = tapes.Max(t => t.Id) +1;
            var newTape = Mapper.Map<Tape>(tape);
            newTape.Id = nextId;
            return _tapeRepository.CreateTape(newTape);
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