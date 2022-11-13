using ElectionBackEnd.Data;
using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ElectionBackEnd.Services
{
    public class CandidateService : ICandidate
    {
        private readonly ElectionDataContext _context;
        static readonly string CandidateNotFound = "Candidat introuvable!";
        public CandidateService(ElectionDataContext context)
        {
            _context = context;
        }
        public async Task DeleteElecter(int id)
        {
            var c = _context.Candidates.FirstOrDefault(c => c.Id == id);
            if (c is null) throw new KeyNotFoundException(CandidateNotFound);
            _context.Candidates.Remove(c);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidate> GetCandidate(int id)
        {
            var c = await _context.Candidates.FirstOrDefaultAsync(e => e.Id == id);
            if (c is null) throw new KeyNotFoundException(CandidateNotFound);
            return c;
        }

        public async Task<Candidate> GetCandidateByParti(string parti)
        {
            var c = await _context.Candidates.FirstOrDefaultAsync(e => e.Parti == parti);
            if (c is null) throw new KeyNotFoundException(CandidateNotFound);
            return c;
        }

        public async Task<IEnumerable<Candidate>> GetCandidates()
        {
            return await _context.Candidates.AsNoTracking().ToListAsync();
        }

        public async Task PostCandidate(Candidate candidate)
        {
            _context.Candidates.Add(candidate);
            await _context.SaveChangesAsync();
        }

        public async Task<Candidate?> UpdateCandidate(int id, Candidate candidate)
        {
            if (id != candidate.Id)
                return null;
            if (!_context.Candidates.Any(d => d.Id == candidate.Id)) throw new KeyNotFoundException(CandidateNotFound);
            _context.Update(candidate);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }

            return candidate;
        }
        private static void HandleException(SqlException sql)
        {
            if (sql.Message.Contains("IX_Addresses_Longitude_Latitude"))
                throw new BadHttpRequestException("Adresse(Coordonnées) déja existante!!!");
        }
    }
}
