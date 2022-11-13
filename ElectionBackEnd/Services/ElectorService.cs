using ElectionBackEnd.Data;
using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.EntityFrameworkCore;

namespace ElectionBackEnd.Services
{
    
    public class ElectorService : IElector
    {
        private readonly ElectionDataContext _context;
        private readonly IAllocation @ia;
        static readonly string ElecterNotFound = "Electeur introuvable!";
        static readonly string DeskNotFound = "Bureau introuvable!";
        static readonly string AddressNotFound = "Adresse introuvable!";
        static readonly string CenterNotFound = "Centre introuvable!";
        static readonly string CandidateNotFound = "Candidate introuvable!";
        static readonly string CandidateHaveVoted = "Candidate a déja voté!";
        public ElectorService(ElectionDataContext context, IAllocation allocationService)
        {
            _context = context;
            ia = allocationService;
        }

        public async Task DeleteElecter(int id)
        {
            var e =  _context.Electers.AsNoTracking().FirstOrDefault(e => e.Id==id);
            if (e is null) throw new KeyNotFoundException(ElecterNotFound);
            _context.Electers.Remove(e);
            ia.IncrementAfterDelElecter(e);
            await _context.SaveChangesAsync();

        }

        public async Task<Elector> GetElecter(int id)
        {
            var e = await _context.Electers.AsNoTracking().FirstOrDefaultAsync(e => e.Id==id);
            if (e is null) throw new KeyNotFoundException(ElecterNotFound);
            return e;
        }

        public async Task<IEnumerable<Elector>> GetElecters()
        {
            return await _context.Electers.AsNoTracking().ToListAsync();
        }

        /*public Task<IEnumerable<Elector>> GetElectersWithSameCenter(int idCenter)
        {
            var desks = _deskService.GetDesksWithSameCenter(idCenter);
            List<Elector> electers= new();
            foreach (Desk desk in desks.Result)
            {
                electers.AddRange(GetElectersWithSameDesk(desk.Id).Result);
            }
            return Task.FromResult< IEnumerable < Elector >>(electers);
        }*/

        public async Task<IEnumerable<Elector>> GetElectersWithSameDesk(int idDesk)
        {
            var desk = _context.Desks.Find(idDesk);
            if (desk is null) throw new KeyNotFoundException(DeskNotFound);
            return await _context.Electers.Where(e=>e.DeskId==idDesk).AsNoTracking().ToListAsync();
        }

        public async Task PostElecter(Elector electer)
        {
            if (!_context.Addresses.Any(a => a.Id == electer.AddressId)) throw new KeyNotFoundException(AddressNotFound);
            electer.DeskId = ia.AssignBestCenter(electer.AddressId).Result.Id;
            electer.CandidateId=1;
            electer.Voted = false;
            _context.Electers.Add(electer);
            await _context.SaveChangesAsync();
        }

        public async Task<Elector?> UpdateElecter(int id, Elector electer)
        {
            if (id != electer.Id) 
                return null;
            if (!_context.Electers.Any(e => e.Id == electer.Id)) throw new KeyNotFoundException(ElecterNotFound);
            if (!_context.Addresses.Any(a => a.Id == electer.AddressId)) throw new KeyNotFoundException(AddressNotFound);
            electer.DeskId = ia.AssignBestCenter(electer.AddressId).Result.Id;
            _context.Update(electer);
            await _context.SaveChangesAsync();
            return electer;
        }

        public async Task Vote(int id, int idC)
        {
            if (!_context.Candidates.Any(e => e.Id == idC)) throw new KeyNotFoundException(CandidateNotFound);
            var e = _context.Electers.AsNoTracking().FirstOrDefault(e => e.Id == id);
            if(e is null) throw new KeyNotFoundException(ElecterNotFound);
            if(e.Voted) throw new KeyNotFoundException(CandidateHaveVoted);
            e.Voted = true;
            e.CandidateId= idC;
            _context.Update(e);
            await _context.SaveChangesAsync();
        }

        //--------------------------------

        public async Task<int> CountElectors()
        {
            return await _context.Electers.AsNoTracking().CountAsync();
        }

        public async Task<int> CountVotedElectors()
        {
            return await _context.Electers.AsNoTracking().Where(e => e.Voted == true).CountAsync();
        }
        public async Task<int>  CountVotedElectorsForACand(int idC)
        {
            if (!_context.Candidates.Any(c => c.Id == idC)) throw new KeyNotFoundException(CandidateNotFound);
            return await _context.Electers.AsNoTracking().
                Where(e => e.Voted == true).
                Where(e=> e.CandidateId==idC).
                CountAsync();
        }
        public async Task<int> CountElectorsByCenter(int id)
        {
            int nbr = 0;
            if (!_context.Centers.AsNoTracking().Any(c => c.Id == id)) throw new KeyNotFoundException(CenterNotFound);
            var desks = _context.Desks.AsNoTracking().Where(d => d.CenterId == id).ToList();
            foreach (Desk desk in desks)
            {
                nbr += await _context.Electers.AsNoTracking().Where(e => e.DeskId == desk.Id).CountAsync();
            }
            return nbr;
        }
        public async Task<int> CountVotedElectorsByCenter(int id)
        {
            int nbr = 0;
            var c = _context.Centers.AsNoTracking().FirstOrDefault(c => c.Id == id);
            if (c is null) throw new KeyNotFoundException(CenterNotFound);
            var desks = _context.Desks.AsNoTracking().Where(d => d.CenterId == id).ToList();
            foreach (Desk desk in desks)
            {
                nbr += await _context.Electers.AsNoTracking().
                    Where(e => e.DeskId == desk.Id).
                    Where(e => e.Voted == true).CountAsync();
            }
            return nbr;
        }
        public async Task<int> CountVotedElectorsByCenterForACand(int idCenter, int idCand)
        {
            int nbr = 0;
            var c = _context.Centers.AsNoTracking().FirstOrDefault(c => c.Id == idCenter);
            if (c is null) throw new KeyNotFoundException(CenterNotFound);
            if (_context.Candidates.AsNoTracking().Any(c=>c.Id == idCand)) throw new KeyNotFoundException(CandidateNotFound);
            var desks = _context.Desks.AsNoTracking().Where(d => d.CenterId == idCenter).ToList();
            foreach (Desk desk in desks)
            {
                nbr += await _context.Electers.AsNoTracking().
                    Where(e => e.DeskId == desk.Id).
                    Where(e => e.Voted == true).
                    Where(e=>e.CandidateId == idCand).CountAsync();
            }
            return nbr;
        }
        public async Task<int> CountElectorsByRegion(string region)
        {
            int nbr = 0;
            var addresses = _context.Addresses.AsNoTracking().Where(a => a.Region.ToLower() == region.ToLower()).ToList();
            if (addresses.Count() == 0) return 0;
            List<Center> centers = new ();
            foreach (Address address in addresses)
            {
                centers.AddRange(_context.Centers.AsNoTracking().Where(c => c.AddressId == address.Id).ToList());
            }
            if (centers.Count() == 0) return 0;
            foreach (Center center in centers)
            {
                nbr += await CountElectorsByCenter(center.Id);
            }

            return nbr;
        }
        public async Task<int> CountVotedElectorsByRegion(string region)
        {
            int nbr = 0;
            var addresses = _context.Addresses.AsNoTracking().Where(a => a.Region.ToLower() == region.ToLower()).ToList();
            if (addresses.Count() == 0) return 0;
            List<Center> centers = new List<Center>();
            foreach (Address address in addresses)
            {
                centers.AddRange(_context.Centers.AsNoTracking().Where(c => c.AddressId == address.Id).ToList());
            }
            if (centers.Count == 0) return 0;
            foreach (Center center in centers)
            {
                nbr += await CountVotedElectorsByCenter(center.Id);
            }

            return nbr;
        }
        public async Task<int> CountVotedElectorsByRegionForACand(string region, int id)
        {
            int nbr = 0;
            if (_context.Candidates.AsNoTracking().Any(c => c.Id == id)) throw new KeyNotFoundException(CandidateNotFound);
            var addresses = _context.Addresses.AsNoTracking().Where(a => a.Region.ToLower() == region.ToLower()).ToList();
            if (addresses.Count() == 0) return 0;
            List<Center> centers = new List<Center>();
            foreach (Address address in addresses)
            {
                centers.AddRange(_context.Centers.AsNoTracking().Where(c => c.AddressId == address.Id).ToList());
            }
            if (centers.Count == 0) return 0;
            foreach (Center center in centers)
            {
                nbr += await CountVotedElectorsByCenterForACand(center.Id, id);
            }

            return nbr;
        }
        public async Task<int> CountElectorsByDesk(int id)
        {
            if (_context.Desks.AsNoTracking().Any(d => d.Id == id)) throw new KeyNotFoundException(DeskNotFound);
            return await _context.Electers.AsNoTracking().Where(e => e.DeskId == id).CountAsync();
        }
        public async Task<int> CountVotedElectorsByDesk(int id)
        {
            if (_context.Desks.AsNoTracking().Any(d => d.Id == id)) throw new KeyNotFoundException(DeskNotFound);
            return await _context.Electers.AsNoTracking()
                .Where(e => e.Voted == true)
                .Where(e => e.DeskId == id)
                .CountAsync();
        }
        public async Task<int> CountVotedElectorsByDeskForACand(int idD, int idC )
        {
            if (_context.Desks.AsNoTracking().Any(d => d.Id == idD)) throw new KeyNotFoundException(DeskNotFound);
            if (_context.Candidates.AsNoTracking().Any(c => c.Id == idC)) throw new KeyNotFoundException(CandidateNotFound);
            return await _context.Electers.AsNoTracking().Where(e => e.Voted == true).
                Where(e => e.DeskId == idD).
                Where(e=>e.CandidateId == idC).
                CountAsync();
        }

    }
}
