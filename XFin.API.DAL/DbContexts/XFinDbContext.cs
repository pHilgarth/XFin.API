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
        public DbSet<ExternalParty> ExternalParties { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountHolder>().HasData(
                new AccountHolder
                {
                    Id = 1,
                    Name = "Patrick Hilgarth",
                },
                new AccountHolder
                {
                    Id = 2,
                    Name = "Ilona Schuhmacher",
                });

            modelBuilder.Entity<BankAccountIdentifier>().HasData(
                new BankAccountIdentifier
                {
                    Iban = "DE21654913200071808000",
                    Bic = "GENODES1VBL"
                },
                new BankAccountIdentifier
                {
                    Iban = "DE21654913200071808019",
                    Bic = "GENODES1VBL"
                },
                new BankAccountIdentifier
                {
                    Iban = "DE21654913200071808400",
                    Bic = "GENODES1VBL"
                },
                new BankAccountIdentifier
                {
                    Iban = "DE66654913200027911004",
                    Bic = "GENODES1VBL"
                },
                new BankAccountIdentifier
                {
                    Iban = "DE66654913200027911403",
                    Bic = "GENODES1VBL"
                },
                new BankAccountIdentifier
                {
                    Iban ="Arbeitgeber_Iban",
                    Bic = "Arbeitgeber_Bic"
                },
                new BankAccountIdentifier
                {
                    Iban = "Aldi_Iban",
                    Bic = "Aldi_Bic"
                },
                new BankAccountIdentifier
                {
                    Iban = "Iban-Negative-Sample",
                    Bic = "Sample Bic Negative"
                },
                new BankAccountIdentifier
                {
                    Iban = "Iban-0-Sample",
                    Bic = "Sample Bic 0"
                });

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

            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount
                {
                    AccountNumber = "71808000",
                    AccountHolderId = 1,
                    BankAccountIdentifierIban = "DE21654913200071808000",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Konto"
                },
                new BankAccount
                {
                    AccountNumber = "71808019",
                    AccountHolderId = 1,
                    BankAccountIdentifierIban = "DE21654913200071808019",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Konto"
                },
                new BankAccount
                {
                    AccountNumber = "71808400",
                    AccountHolderId = 1,
                    BankAccountIdentifierIban = "DE21654913200071808400",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Sparkonto"
                },
                new BankAccount
                {
                    AccountNumber = "27911004",
                    AccountHolderId = 2,
                    BankAccountIdentifierIban = "DE66654913200027911004",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Girokonto"
                },
                new BankAccount
                {
                    AccountNumber = "27911403",
                    AccountHolderId = 2,
                    BankAccountIdentifierIban = "DE66654913200027911403",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Sparkonto"
                },
                new BankAccount
                {
                    AccountNumber = "0-Sample",
                    AccountHolderId = 2,
                    BankAccountIdentifierIban = "Iban-0-Sample",
                    Bank = "Sample Bank",
                    AccountType = "Sample 0 Account"
                },
                new BankAccount
                {
                    AccountNumber = "Negative-Sample",
                    AccountHolderId = 2,
                    BankAccountIdentifierIban = "Iban-Negative-Sample",
                    Bank = "Sample Bank",
                    AccountType = "Sample Negative Account"
                });

            modelBuilder.Entity<ExternalParty>().HasData(
                new ExternalParty
                {
                    Id = 1,
                    Name = "Arbeitgeber",
                    BankAccountIdentifierIban = "Arbeitgeber_Iban"
                },
                new ExternalParty
                {
                    Id = 2,
                    Name = "Aldi",
                    BankAccountIdentifierIban = "Aldi_Iban"
                });

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountAccountNumber = "71808000",
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 2,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountAccountNumber = "71808019",
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 3,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountAccountNumber = "71808400",
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 4,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountAccountNumber = "27911004",
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 5,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountAccountNumber = "27911403",
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 6,
                    Date = DateTime.Parse("2021-02-01T14:30:00Z"),
                    Amount = 100m,
                    Reference = "Umbuchung 000 - 019",
                    BankAccountAccountNumber = "71808019",
                    TransactionCategoryId = 1,
                    CounterPartTransactionToken = "TOKEN: Umbuchung 000 - 019"
                },
                new Transaction
                {
                    Id = 7,
                    Date = DateTime.Parse("2021-02-01T14:30:00Z"),
                    Amount = -100m,
                    Reference = "Umbuchung 000 - 019",
                    BankAccountAccountNumber = "71808000",
                    TransactionCategoryId = 1,
                    CounterPartTransactionToken = "TOKEN: Umbuchung 000 - 019"
                },
                new Transaction
                {
                    Id = 8,
                    Date = DateTime.Parse("2021-03-01T14:30:00Z"),
                    Amount = 50m,
                    Reference = "Einnahme von extern, z.B. Arbeitgeber",
                    BankAccountAccountNumber = "71808000",
                    TransactionCategoryId = 1,
                    ExternalPartyId = 1
                },
                new Transaction
                {
                    Id = 9,
                    Date = DateTime.Parse("2021-03-01T14:30:00Z"),
                    Amount = -75m,
                    Reference = "Ausgabe nach extern, z.B. Aldi Kartenzahlung",
                    BankAccountAccountNumber = "71808000",
                    TransactionCategoryId = 2,
                    ExternalPartyId = 2
                },
                new Transaction
                {
                    Id = 10,
                    Date = DateTime.Parse("2021-02-01T14:30:00Z"),
                    Amount = 10m,
                    Reference = "Umbuchung von 000 Cat1 - 000 Cat2",
                    BankAccountAccountNumber = "71808000",
                    TransactionCategoryId = 2,
                    CounterPartTransactionToken = "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2"
                },
                new Transaction
                {
                    Id = 11,
                    Date = DateTime.Parse("2021-02-01T14:30:00Z"),
                    Amount = -10m,
                    Reference = "Umbuchung von 000 Cat1 - 000 Cat2",
                    BankAccountAccountNumber = "71808000",
                    TransactionCategoryId = 1,
                    CounterPartTransactionToken = "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2"
                },
                new Transaction
                {
                    Id = 12,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = 0m,
                    Reference = "[Kontoinitialisierung]",
                    BankAccountAccountNumber = "0-Sample",
                    TransactionCategoryId = 1,
                },
                new Transaction
                {
                    Id = 13,
                    Date = DateTime.Parse("2021-01-01T14:30:00Z"),
                    Amount = -250m,
                    Reference = "[Kontoinitialisierung]",
                    BankAccountAccountNumber = "Negative-Sample",
                    TransactionCategoryId = 1,
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
