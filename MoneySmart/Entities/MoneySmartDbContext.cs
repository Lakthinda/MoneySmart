using Microsoft.EntityFrameworkCore;

namespace MoneySmart.API.Entities
{
    public class MoneySmartDbContext : DbContext
    {
        public MoneySmartDbContext(DbContextOptions<MoneySmartDbContext> options) : base(options)
        {
            Database.Migrate();            
        }

        public DbSet<SavingAccount> SavingAccounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
