using System.Diagnostics.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace JG.FinTechTest.Data
{
    [ExcludeFromCodeCoverage]
    public class GiftAidDbContext : DbContext
    {
        private static bool _created = false;
        
        public GiftAidDbContext(DbContextOptions<DbContext> dbContextOptions) : base(dbContextOptions)
        {
            if (!_created)
            {
                _created = true;
                Database.EnsureCreated();
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Donation>()
                .Property(f => f.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Donation> Donations { get; set; }
    }
}