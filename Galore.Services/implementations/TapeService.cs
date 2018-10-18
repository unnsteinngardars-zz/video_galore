using System.Collections.Generic;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using AutoMapper;
using System.Linq;
using Galore.Models.Exceptions;
using System;

namespace Galore.Services.Implementations
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
            var newTape = Mapper.Map<Tape>(tape);
            return _tapeRepository.CreateTape(newTape);
        }
        public TapeDetailDTO GetTapeById(int tapeId)
        {
            var tape = IsValidId(tapeId);
            return Mapper.Map<TapeDetailDTO>(tape);
        }

        public void DeleteTape(int tapeId)
        {
            var tape = IsValidId(tapeId);
            _tapeRepository.DeleteTape(tape);
        }

        public void UpdateTape(TapeInputModel tape, int tapeId)
        {
            var updateTape = IsValidId(tapeId);
            _tapeRepository.UpdateTapeById(Mapper.Map<Tape>(tape), tapeId);
        }

        // Report
        // TODO: Report for admin. L8r
        public IEnumerable<Tape> GetTapesByDate(string date)
        {
            throw new System.NotImplementedException();
        }

        public Tape IsValidId(int id){
            var tape = _tapeRepository.GetTapeById(id);
            if(tape == null) {
                throw new ResourceNotFoundException($"Tape with id {id} was not found");
            }
            return tape;
        }
    }
}