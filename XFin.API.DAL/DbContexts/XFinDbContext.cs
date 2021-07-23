using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using XFin.API.Core.Entities;

namespace XFin.API.DAL.DbContexts
{
    public class XFinDbContext : DbContext
    {
        public XFinDbContext(DbContextOptions<XFinDbContext> options) : base(options) { }

        public DbSet<AccountHolder> AccountHolders { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountIdentifier> BankAccountIdentifiers { get; set; }
        public DbSet<ExternalParty> ExternalParties { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

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
