using Microsoft.EntityFrameworkCore;

namespace Assignment.Models.CurrencyRate
{
    public class CurrencyRateDbContext : DbContext
    {
        public CurrencyRateDbContext(DbContextOptions<CurrencyRateDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Replace with your actual connection string
                optionsBuilder.UseNpgsql("Host=postgres-server;Database=currencydb;Username=admin;Password=adminpassword");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities here if needed
        }

        public DbSet<Tables.CurrencyRate> CurrencyRates { get; set; }
        public DbSet<Tables.CalculatedHistory> CalculatedHistories { get; set; }
    }
}