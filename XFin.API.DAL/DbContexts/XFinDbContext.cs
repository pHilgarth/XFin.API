using Microsoft.EntityFrameworkCore;
using XFin.API.Core.Entities;

namespace XFin.API.DAL.DbContexts
{
    public class XFinDbContext : DbContext
    {
        public XFinDbContext(DbContextOptions<XFinDbContext> options) : base(options) { }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<BlockedBudget> BlockedBudget { get; set; }
        public DbSet<InternalBankAccount> InternalBankAccounts { get; set; }
        public DbSet<ExternalBankAccount> ExternalBankAccounts { get; set; }
        public DbSet<ExternalParty> ExternalParties { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<InternalTransaction> InternalTransactions { get; set; }
        public DbSet<ExternalTransaction> ExternalTransactions { get; set; }
        public DbSet<InternalBankAccountSettings> InternalBankAccountSettings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TransactionCategory>().HasData(
                new TransactionCategory
                {
                    Id = 1,
                    Name = "Nicht zugewiesen"
                },
                new TransactionCategory
                {
                    Id = 2,
                    Name = "Essen, Trinken"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
