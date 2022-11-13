using ElectionBackEnd.Model;

namespace ElectionBackEnd.Interfaces
{
    public interface IElector
    {
        Task<Elector> GetElecter(int id);
        Task<IEnumerable<Elector>> GetElecters();
        Task<IEnumerable<Elector>> GetElectersWithSameDesk(int idDesk);
       /* Task<IEnumerable<Elector>> GetElectersWithSameCenter(int IdCenter);*/
        Task<Elector?> UpdateElecter(int id, Elector electer);
        Task Vote(int id, int idC);
        Task DeleteElecter(int id);
        Task PostElecter(Elector electer);
        Task<int> CountElectors();
        Task<int> CountVotedElectors();
        Task<int> CountVotedElectorsForACand(int idC);
        Task<int> CountElectorsByCenter(int id);
        Task<int> CountVotedElectorsByCenter(int id);
        Task<int> CountVotedElectorsByCenterForACand(int idCenter, int idCand);
        Task<int> CountElectorsByDesk(int id);
        Task<int> CountVotedElectorsByDesk(int id);
        Task<int> CountVotedElectorsByDeskForACand(int idD, int idC);
        Task<int> CountElectorsByRegion(string region);
        Task<int> CountVotedElectorsByRegion(string region);
        Task<int> CountVotedElectorsByRegionForACand(string region, int idC);

    }
}
