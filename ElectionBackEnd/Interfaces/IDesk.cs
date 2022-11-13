using ElectionBackEnd.Model;

namespace ElectionBackEnd.Interfaces
{
    public interface IDesk
    {
        Task<IEnumerable <Desk>> GetDesks();
        Task<IEnumerable <Desk>> GetDesksWithSameCenter(int idCenter);
        Task<Desk> GetDesk(int id);
        Task<Desk?> PostDesk(Desk desk);
        Task<Desk?> PutDesk(int id, Desk desk);
        Task DeleteDesk(int id);

    }
}
