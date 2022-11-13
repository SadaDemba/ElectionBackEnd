using ElectionBackEnd.Model;

namespace ElectionBackEnd.Interfaces
{
    public interface ICenter
    {
        Task<IEnumerable<Center>> GetCenters();
        Task<IEnumerable<Center>>  GetDesksWithSameAddress(int id);
        Task<Center> Get(int id);
        Task<Center?> Post(Center center);
        Task<Center?> Put(int id, Center center);
        Task Delete(int id);
    }
}
