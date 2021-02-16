using Microsoft.EntityFrameworkCore;
using XFin.API.Core.Entities;

namespace XFin.API.DAL.DbContexts
{
    public class XFinDbContext : DbContext
    {
        /*************************************************************************************************************
         * 
         * Constructors
         * 
        *************************************************************************************************************/
        public XFinDbContext(DbContextOptions<XFinDbContext> options) : base(options) { }

        /*************************************************************************************************************
         * 
         * Public Members
         * 
        *************************************************************************************************************/
        public DbSet<Depositor> Depositors { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }

        /*************************************************************************************************************
         * 
         * Private Members
         * 
        *************************************************************************************************************/
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Depositor>().HasData(
                new Depositor()
                {
                    Id = 1,
                    Name = "Patrick Hilgarth",
                },
                new Depositor()
                {
                    Id = 2,
                    Name = "Ilona Schuhmacher",
                });

            modelBuilder.Entity<BankAccount>().HasData(
                new BankAccount()
                {
                    Id = 1,
                    DepositorId = 1,
                    AccountNumber = "71808000",
                    Iban = "DE66654913200071808000",
                    Bic = "GENODES1VBL",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Konto"
                },
                new BankAccount()
                {
                    Id = 2,
                    DepositorId = 1,
                    AccountNumber = "71808019",
                    Iban = "DE214913200071808019",
                    Bic = "GENODES1VBL",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Konto"
                },
                new BankAccount()
                {
                    Id = 3,
                    DepositorId = 1,
                    AccountNumber = "71808400",
                    Iban = "DE45654913200071808400",
                    Bic = "GENODES1VBL",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Sparkonto"
                },
                new BankAccount()
                {
                    Id = 4,
                    DepositorId = 2,
                    AccountNumber = "27911004",
                    Iban = "DE45654913200027911004",
                    Bic = "GENODES1VBL",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Girokonto"
                },
                new BankAccount()
                {
                    Id = 5,
                    DepositorId = 2,
                    AccountNumber = "27911403",
                    Iban = "DE45654913200027911403",
                    Bic = "GENODES1VBL",
                    Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                    AccountType = "Sparkonto"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}
