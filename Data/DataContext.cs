using LBank.Data.Mappings;
using LBank.Models;
using Microsoft.EntityFrameworkCore;

namespace LBank.Data;

public class DataContext : DbContext
{
    public DbSet<Account> Accounts { get; set; }
    public DbSet<AccountTransaction> AccountTransactions { get; set; }
    public DbSet<TransactionType> TransactionTypes { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=LBank;User ID=sa;Password=1234");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new AccountMap());
        modelBuilder.ApplyConfiguration(new AccountTransactionMap());
        modelBuilder.ApplyConfiguration(new UserMap());
    }
}