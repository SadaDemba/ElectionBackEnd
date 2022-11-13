using BAMCIS.GIS;
using ElectionBackEnd.Data;
using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Model;
using Microsoft.EntityFrameworkCore;

namespace ElectionBackEnd.Services
{
    public class AllocationService : IAllocation
    {
        private readonly ElectionDataContext _context;
        private readonly IDesk @ifd;
        private readonly ICenter @ic;
        static readonly string AddressNotFound = "Adresse introuvable!";
        static readonly string NoCenterFound = "Pas de centre disponible!";
        public AllocationService(ElectionDataContext context, IDesk deskService, ICenter centerService)
        {
            _context = context;
            @ifd = deskService;
            @ic = centerService;
        }

        public async Task<Desk> AssignBestCenter(int idAddress)
        {
            double tmp, distance = 0;
            Center cTmp, center;

            var centers = await _context.Centers.Where(c => c.IsFull == false).AsNoTracking().ToListAsync();
            if (!centers.Any()) throw new KeyNotFoundException(NoCenterFound);
            tmp = this.Distance(idAddress, centers[0].AddressId).Result;
            cTmp = centers[0];
            for (int i = 1; i < centers.Count(); i++)
            {
                distance = this.Distance(idAddress, centers[i].AddressId).Result;
                center = centers[i];
                if (distance < tmp)
                {
                    tmp = distance;
                    cTmp = centers[i];
                }
            }
            Console.WriteLine(cTmp.ToString());
            Desk desk = _context.Desks.Where(d => d.CenterId == cTmp.Id).
                                       Where(d => d.Nb_ElectersAssignable != 0).
                                       First();
            this.DecrementAFterAdd(desk, cTmp);
            Console.WriteLine(desk.ToString());
            return desk;
        }
        public async Task<double> Distance(int id1, int id2)
        {
            var address1 = await _context.Addresses.FindAsync(id1);
            if (address1 == null) throw new KeyNotFoundException(AddressNotFound);
            var address2 = await _context.Addresses.FindAsync(id2);
            if (address2 == null) throw new KeyNotFoundException(AddressNotFound);
            GeoCoordinate source = new(address1.Latitude, address1.Longitude);
            GeoCoordinate target = new(address2.Latitude, address2.Longitude);
            return source.DistanceTo(target, DistanceType.KILOMETERS);
        }
        public async void DecrementAFterAdd(Desk desk, Center c)
        {
            desk.Nb_ElectersAssignable--;
            await @ifd.PutDesk(desk.Id, desk);
            if (desk.Nb_ElectersAssignable > 0) return;
            if (!_context.Desks.AsNoTracking().Where(d => d.CenterId == desk.CenterId).Where(d => d.Nb_ElectersAssignable != 0).Any())
            {
                c.IsFull = true;
                await @ic.Put(c.Id, c);
            }

        }

        public async void IncrementAfterDelElecter(Elector electer)
        {
            var desk = await @ifd.GetDesk(electer.DeskId);
            desk.Nb_ElectersAssignable++;
            var center = await @ic.Get(desk.CenterId);
            //await @ifd.PutDesk(desk.Id, desk);
            if (center.IsFull)
            {
                center.IsFull = false;
                await @ic.Put(center.Id, center);
            }

        }
    }
}
