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
        public DbSet<Loan> Loans { get; set; }
        public DbSet<RecurringTransaction> RecurringTransactions { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CostCenter>().HasData(
                new CostCenter
                {
                    Id = 1,
                    Name = "Nicht zugewiesen"
                },
                new CostCenter
                {
                    Id = 2,
                    Name = "Essen, Trinken"
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

            modelBuilder.Entity<RecurringTransaction>()
                .HasOne(r => r.SourceCostCenter)
                .WithMany(c => c.RecurringExpenses)
                .HasForeignKey(r => r.SourceCostCenterId);

            modelBuilder.Entity<RecurringTransaction>()
                .HasOne(r => r.TargetCostCenter)
                .WithMany(b => b.RecurringRevenues)
                .HasForeignKey(r => r.TargetCostCenterId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceBankAccount)
                .WithMany(b => b.Expenses)
                .HasForeignKey(t => t.SourceBankAccountId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TargetBankAccount)
                .WithMany(b => b.Revenues)
                .HasForeignKey(t => t.TargetBankAccountId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.SourceCostCenter)
                .WithMany(c => c.Expenses)
                .HasForeignKey(t => t.SourceCostCenterId);

            modelBuilder.Entity<Transaction>()
                .HasOne(t => t.TargetCostCenter)
                .WithMany(c => c.Revenues)
                .HasForeignKey(t => t.TargetCostCenterId);

            modelBuilder.Entity<Loan>()
                .HasOne(e => e.RecurringTransaction)
                .WithOne(r => r.Loan)
                .HasForeignKey<RecurringTransaction>(r => r.LoanId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
