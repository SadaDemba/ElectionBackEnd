using ElectionBackEnd.Model;

namespace ElectionBackEnd.Interfaces
{
    public interface ICandidate
    {
        Task<Candidate> GetCandidate(int id);
        Task<IEnumerable<Candidate>> GetCandidates();
        Task<Candidate> GetCandidateByParti(string parti);
        /* Task<IEnumerable<Elector>> GetElectersWithSameCenter(int IdCenter);*/
        Task<Candidate?> UpdateCandidate(int id, Candidate candidate);
        Task DeleteElecter(int id);
        Task PostCandidate(Candidate candidate);
    }
}
