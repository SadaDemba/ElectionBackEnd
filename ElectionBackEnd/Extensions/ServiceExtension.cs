using ElectionBackEnd.Interfaces;
using ElectionBackEnd.Services;

namespace ElectionBackEnd.Extensions
{
    public static class ServiceExtension
    {
        public static void RegisterAppServices(this IServiceCollection service)
        {
            service.AddTransient<IElector, ElectorService>();
            service.AddTransient<IDesk, DeskService>();
            service.AddTransient<ICenter, CenterService>();
            service.AddTransient<IAddress, AddressService>();
            service.AddTransient<IAllocation, AllocationService>();
            service.AddTransient<ICandidate, CandidateService>();
        }
    }
}
