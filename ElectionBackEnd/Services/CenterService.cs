using ElectionBackEnd.Data;
using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.EntityFrameworkCore;

namespace ElectionBackEnd.Services
{
    public class CenterService : ICenter
    {
        private readonly ElectionDataContext _context;
        static readonly string AddressNotFound = "Adresse introuvable!";
        static readonly string CenterNotFound = "Centre introuvable!";
        public CenterService(ElectionDataContext context)
        {
            _context = context;
        }
        public async Task Delete(int id)
        {
            var center = _context.Centers.FirstOrDefault(c => c.Id == id);
            if (center is null) throw new KeyNotFoundException(CenterNotFound);
            _context.Centers.Remove(center);
            await _context.SaveChangesAsync();
        }

        public async Task<Center> Get(int id)
        {
            var center = await _context.Centers.AsNoTracking().FirstOrDefaultAsync(c=> c.Id==id);
            if (center is null) throw new KeyNotFoundException(CenterNotFound);
            return center;
        }

        public async Task<IEnumerable<Center>> GetDesksWithSameAddress(int idAddress)
        {
            if (!_context.Addresses.Any(a => a.Id == idAddress)) throw new KeyNotFoundException(AddressNotFound);
            return await _context.Centers.Where(c => c.AddressId == idAddress).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Center>> GetCenters()
        {
            return await _context.Centers.AsNoTracking().ToListAsync();
        }

        public async Task<Center?> Post(Center center)
        {
            if (!_context.Addresses.Any(a => a.Id == center.AddressId)) throw new KeyNotFoundException(AddressNotFound);
            if (center.Nb_Desk <= 0)
                return null;
            _context.Centers.Add(center);
            await _context.SaveChangesAsync();
            return center;
        }

        public async Task<Center?> Put(int id, Center center)
        {
            if (id != center.Id)
                return null;
            if (!_context.Centers.Any(c => c.Id == center.Id)) throw new KeyNotFoundException(CenterNotFound);
            if (!_context.Addresses.Any(d => d.Id == center.AddressId)) throw new KeyNotFoundException(AddressNotFound);
            _context.Update(center);
            await _context.SaveChangesAsync();
            return center;
        }
     
    }
}
    