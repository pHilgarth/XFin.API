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
                }
                );

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
                    Id = 1,
                    AccountHolderId = 1,
                    BankAccountIban = "DE21654913200071808000",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Konto"
                },
                new BankAccount
                {
                    Id = 2,
                    AccountHolderId = 1,
                    BankAccountIban = "DE21654913200071808019",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Konto"
                },
                new BankAccount
                {
                    Id = 3,
                    AccountHolderId = 1,
                    BankAccountIban = "DE21654913200071808400",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Sparkonto"
                },
                new BankAccount
                {
                    Id = 4,
                    AccountHolderId = 2,
                    BankAccountIban = "DE66654913200027911004",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Girokonto"
                },
                new BankAccount
                {
                    Id = 5,
                    AccountHolderId = 2,
                    BankAccountIban = "DE66654913200027911403",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Sparkonto"
                });

            modelBuilder.Entity<ExternalParty>().HasData(
                new ExternalParty
                {
                    Id = 1,
                    Name = "Arbeitgeber",
                    BankAccountIban = "Arbeitgeber_Iban"
                },
                new ExternalParty
                {
                    Id = 2,
                    Name = "Aldi",
                    BankAccountIban = "Aldi_Iban"
                });

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction
                {
                    Id = 1,
                    Date = new DateTimeOffset(),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountId = 1,
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 2,
                    Date = new DateTimeOffset(),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountId = 2,
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 3,
                    Date = new DateTimeOffset(),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountId = 3,
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 4,
                    Date = new DateTimeOffset(),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountId = 4,
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 5,
                    Date = new DateTimeOffset(),
                    Amount = 500m,
                    Reference = "Kontoinitialisierung",
                    BankAccountId = 5,
                    TransactionCategoryId = 1
                },
                new Transaction
                {
                    Id = 6,
                    Date = new DateTimeOffset(),
                    Amount = 100m,
                    Reference = "Umbuchung 000 - 019",
                    BankAccountId = 2,
                    TransactionCategoryId = 1,
                    CounterPartTransactionToken = "TOKEN: Umbuchung 000 - 019"
                },
                new Transaction
                {
                    Id = 7,
                    Date = new DateTimeOffset(),
                    Amount = -100m,
                    Reference = "Umbuchung 000 - 019",
                    BankAccountId = 1,
                    TransactionCategoryId = 1,
                    CounterPartTransactionToken = "TOKEN: Umbuchung 000 - 019"
                },
                new Transaction
                {
                    Id = 8,
                    Date = new DateTimeOffset(),
                    Amount = 50m,
                    Reference = "Einnahme von extern, z.B. Arbeitgeber",
                    BankAccountId = 1,
                    TransactionCategoryId = 1,
                    ExternalPartyId = 1
                },
                new Transaction
                {
                    Id = 9,
                    Date = new DateTimeOffset(),
                    Amount = -75m,
                    Reference = "Ausgabe nach extern, z.B. Aldi Kartenzahlung",
                    BankAccountId = 1,
                    TransactionCategoryId = 2,
                    ExternalPartyId = 2
                },
                new Transaction
                {
                    Id = 10,
                    Date = new DateTimeOffset(),
                    Amount = 10m,
                    Reference = "Umbuchung von 000 Cat1 - 000 Cat2",
                    BankAccountId = 1,
                    TransactionCategoryId = 2,
                    CounterPartTransactionToken = "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2"
                },
                new Transaction
                {
                    Id = 11,
                    Date = new DateTimeOffset(),
                    Amount = -10m,
                    Reference = "Umbuchung von 000 Cat1 - 000 Cat2",
                    BankAccountId = 1,
                    TransactionCategoryId = 1,
                    CounterPartTransactionToken = "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
