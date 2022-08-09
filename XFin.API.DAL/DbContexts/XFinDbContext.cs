﻿using Microsoft.EntityFrameworkCore;
using XFin.API.Core.Entities;

namespace XFin.API.DAL.DbContexts
{
    public class XFinDbContext : DbContext
    {
        public XFinDbContext(DbContextOptions<XFinDbContext> options) : base(options) { }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<InternalBankAccount> InternalBankAccounts { get; set; }
        public DbSet<ExternalBankAccount> ExternalBankAccounts { get; set; }
        public DbSet<ExternalParty> ExternalParties { get; set; }
        public DbSet<CostCenter> CostCenters { get; set; }
        public DbSet<InternalTransaction> InternalTransactions { get; set; }
        public DbSet<ExternalTransaction> ExternalTransactions { get; set; }
        public DbSet<Reserve> Reserves { get; set; }
        public DbSet<RecurringExpense> RecurringExpenses { get; set; }
        public DbSet<RecurringRevenue> RecurringRevenues { get; set; }
        public DbSet<RecurringTransfer> RecurringTransfer { get; set; }
        public DbSet<Loan> Loans { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
