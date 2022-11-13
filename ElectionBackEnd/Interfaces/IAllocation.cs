using ElectionBackEnd.Model;

namespace ElectionBackEnd.Interfaces
{
    public interface IAllocation
    {
        Task<Desk> AssignBestCenter(int idAddress);
        void IncrementAfterDelElecter(Elector electer);

    }
}
