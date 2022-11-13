using ElectionBackEnd.Model;
using Microsoft.EntityFrameworkCore;
namespace ElectionBackEnd.Data
{
    public class ElectionDataContext: DbContext
    {
        public ElectionDataContext(DbContextOptions<ElectionDataContext> options) : base(options)
        {
        }
        public DbSet<Elector> Electers { get; set; }
        public DbSet<Desk> Desks { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Center> Centers { get; set; }
        public DbSet<Candidate> Candidates { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Elector>().
                HasOne(e => e.Address).
                WithMany(a => a.Electers);

            modelBuilder.Entity<Desk>().
                HasMany(d => d.Electers).
                WithOne(e => e.Desk);

            modelBuilder.Entity<Center>().
                HasMany(c => c.Desks).
                WithOne(d => d.Center);

            modelBuilder.Entity<Elector>().
                HasOne(e => e.Candidate).
                WithMany(c => c.Electors);

        }
    }
}
