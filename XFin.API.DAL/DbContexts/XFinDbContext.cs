using Microsoft.EntityFrameworkCore;
using XFin.API.Core.Entities;

namespace XFin.API.DAL.DbContexts
{
    public class XFinDbContext : DbContext
    {
        public XFinDbContext(DbContextOptions<XFinDbContext> options) : base(options) { }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<CostCenterAsset> CostCenterAssets { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BudgetAllocation>()
                .HasOne(b => b.SourceCostCenter)
                .WithMany(c => c.BudgetDeallocations)
                .HasForeignKey(b => b.SourceCostCenterId);

            modelBuilder.Entity<BudgetAllocation>()
                .HasOne(b => b.TargetCostCenter)
                .WithMany(c => c.BudgetAllocations)
                .HasForeignKey(b => b.TargetCostCenterId);

            modelBuilder.Entity<BudgetAllocation>()
                .HasOne(b => b.SourceCostCenterAsset)
                .WithMany(c => c.BudgetDeallocations)
                .HasForeignKey(b => b.SourceCostCenterAssetId);

            modelBuilder.Entity<BudgetAllocation>()
                .HasOne(b => b.TargetCostCenterAsset)
                .WithMany(c => c.BudgetAllocations)
                .HasForeignKey(b => b.TargetCostCenterAssetId);

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "admin",
                    Password = "admin"
                },
                new User
                {
                    Id = 2,
                    Email = "user",
                    Password = "user",
                });

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.CreditorBankAccount)
                .WithMany(b => b.CreditorLoans)
                .HasForeignKey(l => l.CreditorBankAccountId);

            modelBuilder.Entity<Loan>()
                .HasOne(l => l.DebitorBankAccount)
                .WithMany(b => b.DebitorLoans)
                .HasForeignKey(l => l.DebitorBankAccountId);

            modelBuilder.Entity<RecurringTransaction>()
                .HasOne(r => r.SourceBankAccount)
                .WithMany(b => b.RecurringExpenses)
                .HasForeignKey(r => r.SourceBankAccountId);

            modelBuilder.Entity<RecurringTransaction>()
                .HasOne(r => r.TargetBankAccount)
                .WithMany(b => b.RecurringRevenues)
                .HasForeignKey(r => r.TargetBankAccountId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceBankAccount)
                .WithMany(b => b.Expenses)
                .HasForeignKey(t => t.SourceBankAccountId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TargetBankAccount)
                .WithMany(b => b.Revenues)
                .HasForeignKey(t => t.TargetBankAccountId);

            modelBuilder.Entity<Loan>()
                .HasOne(e => e.RecurringTransaction)
                .WithOne(r => r.Loan)
                .HasForeignKey<RecurringTransaction>(r => r.LoanId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
