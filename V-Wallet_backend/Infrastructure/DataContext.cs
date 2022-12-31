using Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class DataContext : IdentityDbContext<User>
{
    public DataContext()
    {
    }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public DbSet<CreditCard> CreditCards { get; set; }
    public DbSet<CryptoCurrency> CryptoCurrencies { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(@"Data Source=TOPSKI\SQLEXPRESS;Initial Catalog=VWalletDB;Integrated Security=True");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CreditCard>()
        .Property(p => p.Deposit)
        .HasPrecision(18, 2);

        modelBuilder.Entity<CryptoCurrency>()
        .Property(p => p.Investment)
        .HasPrecision(18, 2);

        modelBuilder.Entity<CryptoCurrency>()
        .Property(p => p.Value)
        .HasPrecision(18, 2);

        modelBuilder.Entity<Transaction>()
       .Property(p => p.Amount)
       .HasPrecision(18, 2);
    }
}
