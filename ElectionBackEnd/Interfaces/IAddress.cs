using ElectionBackEnd.Model;

namespace ElectionBackEnd.Interfaces
{
    public interface IAddress
    {
        Task<IEnumerable<Address>> GetAddresses();
        Task<Address> GetAddress(int id);
        Task<IEnumerable<Address>> GetAddressesByRegion(string region);
        Task<Address> Post(Address address);
        double GetDistance(int idR1, int idR2);
        Task<Address?> Put(int id, Address address);
        Task Delete(int id);
    }
}
