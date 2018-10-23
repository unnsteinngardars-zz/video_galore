using System.Collections.Generic;
using Galore.Models.Tape;
using Galore.Repositories.Interfaces;
using Galore.Services.Interfaces;
using AutoMapper;
using System.Linq;
using Galore.Models.Exceptions;
using System;
using Galore.Models.Loan;

namespace Galore.Services.Implementations
{
    public class TapeService : ITapeService
    {
        private readonly ITapeRepository _tapeRepository;
        private readonly ILoanRepository _loanRepository;

        public TapeService(ITapeRepository tapeRepository, ILoanRepository loanRepository)
        {
            _tapeRepository = tapeRepository;
            _loanRepository = loanRepository;
        }

        public IEnumerable<TapeDTO> GetAllTapes(string LoanDate)
        {
            var tapes = _tapeRepository.GetAllTapes();
            if (LoanDate.Length > 0)
            {
                // return list with only loan date query parameters
                DateTime date = DateTime.Parse(LoanDate);
                var loans = _loanRepository.GetAllLoans()
                    .Where(l => ((date >= l.BorrowDate && date < l.ReturnDate) || (date >= l.BorrowDate && l.ReturnDate.Equals(DateTime.MinValue))));
                return FindTapeInLoansList(loans, tapes);
            }
            return Mapper.Map<IEnumerable<TapeDTO>>(tapes);
        }

        private IEnumerable<TapeDTO> FindTapeInLoansList(IEnumerable<Loan> loans, IEnumerable<Tape> tapes)
        {
            List<Tape> loanTapes = new List<Tape>();

            foreach (var l in loans)
            {
                var tape = tapes.FirstOrDefault(u => u.Id == l.UserId);
                if (!loanTapes.Contains(tape))
                {
                    loanTapes.Add(tape);
                }
            }

            return Mapper.Map<IEnumerable<TapeDTO>>(loanTapes);
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

        public Tape IsValidId(int id)
        {
            var tape = _tapeRepository.GetTapeById(id);
            if (tape == null)
            {
                throw new ResourceNotFoundException($"Tape with id {id} was not found");
            }
            return tape;
        }
    }
}