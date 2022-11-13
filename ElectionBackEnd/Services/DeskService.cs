using ElectionBackEnd.Data;
using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.EntityFrameworkCore;

namespace ElectionBackEnd.Services
{
    public class DeskService : IDesk
    {
        private readonly ElectionDataContext _context;
        static readonly string DeskNotFound = "Bureau introuvable!";
        static readonly string CenterNotFound = "Centre introuvable!";
        static readonly string NbDeskReached = "Nombre de bureau max atteint!";

        public DeskService(ElectionDataContext context)
        {
            _context = context;
        }

        public async Task DeleteDesk(int id)
        {
            var desktop = _context.Desks.FirstOrDefault(d => d.Id == id);
            if (desktop is null) throw new KeyNotFoundException(DeskNotFound);
            _context.Desks.Remove(desktop);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Desk>> GetDesks()
        {
            return await _context.Desks.AsNoTracking().ToListAsync();
        }

        public async Task<Desk> GetDesk(int id)
        {
            var desk = await _context.Desks.FirstOrDefaultAsync(d => d.Id == id);
            if (desk is null) throw new KeyNotFoundException(DeskNotFound);
            return desk;
        }

        public async Task<IEnumerable<Desk>> GetDesksWithSameCenter(int idCenter)
        {
            if (!_context.Centers.Any(c => c.Id == idCenter)) throw new KeyNotFoundException(CenterNotFound);
            return await _context.Desks.Where(d => d.CenterId == idCenter).AsNoTracking().ToListAsync();
        }

        public async Task<Desk?> PostDesk(Desk desk)
        {
            var c = _context.Centers.AsNoTracking().FirstOrDefault(c => c.Id == desk.CenterId);
            if (c is null) throw new KeyNotFoundException(CenterNotFound);
            if(c.Nb_Desk <= _context.Desks.Where(b=>b.CenterId == c.Id).ToList().Count()) throw new KeyNotFoundException(NbDeskReached);
            if (desk.Capacity <= 0)
                return null;
            desk.Nb_ElectersAssignable = desk.Capacity;
            _context.Desks.Add(desk);
            await _context.SaveChangesAsync();
            return desk;
        }

        public async Task<Desk?> PutDesk(int id, Desk desk)
        {
            if (id != desk.Id)
                return null;
            if (!_context.Desks.AsNoTracking().Any(d => d.Id == desk.Id)) throw new KeyNotFoundException(DeskNotFound);
            if (!_context.Centers.AsNoTracking().Any(c => c.Id == desk.CenterId)) throw new KeyNotFoundException(DeskNotFound);
            _context.Update(desk);
            await _context.SaveChangesAsync();
            return desk;
        }
    }
}
