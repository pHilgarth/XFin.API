﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Migrations
{
    [DbContext(typeof(XFinDbContext))]
    [Migration("20210309204126_DateTimeStringFix")]
    partial class DateTimeStringFix
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.3")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("XFin.API.Core.Entities.AccountHolder", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.HasKey("Id");

                    b.ToTable("AccountHolders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Patrick Hilgarth"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ilona Schuhmacher"
                        });
                });

            modelBuilder.Entity("XFin.API.Core.Entities.BankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountHolderId")
                        .HasColumnType("int");

                    b.Property<string>("AccountType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankAccountIdentifierIban")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("AccountHolderId");

                    b.HasIndex("BankAccountIdentifierIban");

                    b.ToTable("BankAccounts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AccountHolderId = 1,
                            AccountType = "Konto",
                            Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                            BankAccountIdentifierIban = "DE21654913200071808000"
                        },
                        new
                        {
                            Id = 2,
                            AccountHolderId = 1,
                            AccountType = "Konto",
                            Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                            BankAccountIdentifierIban = "DE21654913200071808019"
                        },
                        new
                        {
                            Id = 3,
                            AccountHolderId = 1,
                            AccountType = "Sparkonto",
                            Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                            BankAccountIdentifierIban = "DE21654913200071808400"
                        },
                        new
                        {
                            Id = 4,
                            AccountHolderId = 2,
                            AccountType = "Girokonto",
                            Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                            BankAccountIdentifierIban = "DE66654913200027911004"
                        },
                        new
                        {
                            Id = 5,
                            AccountHolderId = 2,
                            AccountType = "Sparkonto",
                            Bank = "Volksbank-Raiffeisenbank Laupheim-Illertal eG",
                            BankAccountIdentifierIban = "DE66654913200027911403"
                        });
                });

            modelBuilder.Entity("XFin.API.Core.Entities.BankAccountIdentifier", b =>
                {
                    b.Property<string>("Iban")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Bic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Iban");

                    b.ToTable("BankAccountIdentifier");

                    b.HasData(
                        new
                        {
                            Iban = "DE21654913200071808000",
                            Bic = "GENODES1VBL"
                        },
                        new
                        {
                            Iban = "DE21654913200071808019",
                            Bic = "GENODES1VBL"
                        },
                        new
                        {
                            Iban = "DE21654913200071808400",
                            Bic = "GENODES1VBL"
                        },
                        new
                        {
                            Iban = "DE66654913200027911004",
                            Bic = "GENODES1VBL"
                        },
                        new
                        {
                            Iban = "DE66654913200027911403",
                            Bic = "GENODES1VBL"
                        },
                        new
                        {
                            Iban = "Arbeitgeber_Iban",
                            Bic = "Arbeitgeber_Bic"
                        },
                        new
                        {
                            Iban = "Aldi_Iban",
                            Bic = "Aldi_Bic"
                        });
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalParty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BankAccountIban")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("BankAccountIdentifierIban")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountIdentifierIban");

                    b.ToTable("ExternalParty");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BankAccountIban = "Arbeitgeber_Iban",
                            Name = "Arbeitgeber"
                        },
                        new
                        {
                            Id = 2,
                            BankAccountIban = "Aldi_Iban",
                            Name = "Aldi"
                        });
                });

            modelBuilder.Entity("XFin.API.Core.Entities.Transaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("BankAccountId")
                        .HasColumnType("int");

                    b.Property<string>("CounterPartTransactionToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int?>("ExternalPartyId")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionCategoryId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BankAccountId");

                    b.HasIndex("ExternalPartyId");

                    b.HasIndex("TransactionCategoryId");

                    b.ToTable("Transactions");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 500m,
                            BankAccountId = 1,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Kontoinitialisierung",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 2,
                            Amount = 500m,
                            BankAccountId = 2,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Kontoinitialisierung",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 3,
                            Amount = 500m,
                            BankAccountId = 3,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Kontoinitialisierung",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 4,
                            Amount = 500m,
                            BankAccountId = 4,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Kontoinitialisierung",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 5,
                            Amount = 500m,
                            BankAccountId = 5,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Kontoinitialisierung",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 6,
                            Amount = 100m,
                            BankAccountId = 2,
                            CounterPartTransactionToken = "TOKEN: Umbuchung 000 - 019",
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Umbuchung 000 - 019",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 7,
                            Amount = -100m,
                            BankAccountId = 1,
                            CounterPartTransactionToken = "TOKEN: Umbuchung 000 - 019",
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Umbuchung 000 - 019",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 8,
                            Amount = 50m,
                            BankAccountId = 1,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalPartyId = 1,
                            Reference = "Einnahme von extern, z.B. Arbeitgeber",
                            TransactionCategoryId = 1
                        },
                        new
                        {
                            Id = 9,
                            Amount = -75m,
                            BankAccountId = 1,
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ExternalPartyId = 2,
                            Reference = "Ausgabe nach extern, z.B. Aldi Kartenzahlung",
                            TransactionCategoryId = 2
                        },
                        new
                        {
                            Id = 10,
                            Amount = 10m,
                            BankAccountId = 1,
                            CounterPartTransactionToken = "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2",
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Umbuchung von 000 Cat1 - 000 Cat2",
                            TransactionCategoryId = 2
                        },
                        new
                        {
                            Id = 11,
                            Amount = -10m,
                            BankAccountId = 1,
                            CounterPartTransactionToken = "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2",
                            Date = new DateTime(2021, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Reference = "Umbuchung von 000 Cat1 - 000 Cat2",
                            TransactionCategoryId = 1
                        });
                });

            modelBuilder.Entity("XFin.API.Core.Entities.TransactionCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("TransactionCategory");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Nicht zugewiesen"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Essen, Trinken"
                        });
                });

            modelBuilder.Entity("XFin.API.Core.Entities.BankAccount", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.AccountHolder", "AccountHolder")
                        .WithMany()
                        .HasForeignKey("AccountHolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XFin.API.Core.Entities.BankAccountIdentifier", "BankAccountIdentifier")
                        .WithMany()
                        .HasForeignKey("BankAccountIdentifierIban");

                    b.Navigation("AccountHolder");

                    b.Navigation("BankAccountIdentifier");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalParty", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.BankAccountIdentifier", "BankAccountIdentifier")
                        .WithMany()
                        .HasForeignKey("BankAccountIdentifierIban");

                    b.Navigation("BankAccountIdentifier");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.Transaction", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.BankAccount", "BankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("BankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XFin.API.Core.Entities.ExternalParty", "ExternalParty")
                        .WithMany()
                        .HasForeignKey("ExternalPartyId");

                    b.HasOne("XFin.API.Core.Entities.TransactionCategory", "TransactionCategory")
                        .WithMany()
                        .HasForeignKey("TransactionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BankAccount");

                    b.Navigation("ExternalParty");

                    b.Navigation("TransactionCategory");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.BankAccount", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
