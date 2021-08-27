﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using XFin.API.DAL.DbContexts;

namespace XFin.API.DAL.Migrations
{
    [DbContext(typeof(XFinDbContext))]
    partial class XFinDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalBankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Bic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ExternalPartyId")
                        .HasColumnType("int");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalPartyId");

                    b.ToTable("ExternalBankAccounts");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalParty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("ExternalParties");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CounterPartTransactionToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("ExternalBankAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TransactionToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ExternalBankAccountId");

                    b.ToTable("ExternalTransactions");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.InternalBankAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountHolderId")
                        .HasColumnType("int");

                    b.Property<string>("Bank")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Bic")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Iban")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AccountHolderId");

                    b.ToTable("InternalBankAccounts");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.InternalTransaction", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("CounterPartTransactionToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("InternalBankAccountId")
                        .HasColumnType("int");

                    b.Property<string>("Reference")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TransactionCategoryId")
                        .HasColumnType("int");

                    b.Property<string>("TransactionToken")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InternalBankAccountId");

                    b.HasIndex("TransactionCategoryId");

                    b.ToTable("InternalTransactions");
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

                    b.ToTable("TransactionCategories");

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

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalBankAccount", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.ExternalParty", "ExternalParty")
                        .WithMany()
                        .HasForeignKey("ExternalPartyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalParty");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalTransaction", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.ExternalBankAccount", "ExternalBankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("ExternalBankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ExternalBankAccount");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.InternalBankAccount", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.AccountHolder", "AccountHolder")
                        .WithMany()
                        .HasForeignKey("AccountHolderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AccountHolder");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.InternalTransaction", b =>
                {
                    b.HasOne("XFin.API.Core.Entities.InternalBankAccount", "InternalBankAccount")
                        .WithMany("Transactions")
                        .HasForeignKey("InternalBankAccountId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("XFin.API.Core.Entities.TransactionCategory", "TransactionCategory")
                        .WithMany("Transactions")
                        .HasForeignKey("TransactionCategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InternalBankAccount");

                    b.Navigation("TransactionCategory");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.ExternalBankAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.InternalBankAccount", b =>
                {
                    b.Navigation("Transactions");
                });

            modelBuilder.Entity("XFin.API.Core.Entities.TransactionCategory", b =>
                {
                    b.Navigation("Transactions");
                });
#pragma warning restore 612, 618
        }
    }
}
