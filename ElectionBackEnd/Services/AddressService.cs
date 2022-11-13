using BAMCIS.GIS;
using ElectionBackEnd.Data;
using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ElectionBackEnd.Services
{
    public class AddressService:IAddress
    {
        private readonly ElectionDataContext _context;
        static readonly string AddressNotFound = "Adresse introuvable!";
        public AddressService(ElectionDataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Address>> GetAddresses ()
        {
            return await _context.Addresses.AsNoTracking().ToListAsync();
        }


        public async Task<Address> GetAddress(int id)
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address is null) throw new KeyNotFoundException(AddressNotFound);
            return address;
        }

        public async Task<Address> Post(Address address)
        {
                      
            _context.Addresses.Add(address);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }
            return address;
        }

        public async Task<Address?> Put(int id, Address address)
        {
            if (id != address.Id)
                return null;
            if (!_context.Addresses.Any(d => d.Id == address.Id)) throw new KeyNotFoundException(AddressNotFound);
            _context.Update(address);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (ex.InnerException is SqlException sql)
                    HandleException(sql);
            }

            return address;
        }

        public async Task Delete(int id)
        {
            var address = _context.Addresses.FirstOrDefault(c => c.Id == id);
            if (address is null) throw new KeyNotFoundException(AddressNotFound);
            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Address>> GetAddressesByRegion(string region)
        {
            return await _context.Addresses.AsNoTracking().Where(r=>r.Region==region).ToListAsync();
        }

        public double GetDistance(int idR1, int idR2)
        {
            var address1 =  _context.Addresses.AsNoTracking().FirstOrDefault(a=>a.Id == idR1);
            if (address1 == null) throw new KeyNotFoundException(AddressNotFound);
            var address2 = _context.Addresses.AsNoTracking().FirstOrDefault(a => a.Id == idR2);
            if (address2 == null) throw new KeyNotFoundException(AddressNotFound);
            GeoCoordinate source = new(address1.Latitude, address1.Longitude);
            GeoCoordinate target = new(address2.Latitude, address2.Longitude);
            return source.DistanceTo(target, DistanceType.KILOMETERS);
        }
        private static void HandleException(SqlException sql)
        {
            if (sql.Message.Contains("IX_Addresses_Longitude_Latitude"))
                throw new BadHttpRequestException("Adresse(Coordonnées) déja existante!!!");
        }
    }
   
}
