using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace XFin.API.DAL.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountHolders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountHolders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccountIdentifier",
                columns: table => new
                {
                    Iban = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Bic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccountIdentifier", x => x.Iban);
                });

            migrationBuilder.CreateTable(
                name: "TransactionCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    AccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AccountHolderId = table.Column<int>(type: "int", nullable: false),
                    BankAccountIdentifierIban = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.AccountNumber);
                    table.ForeignKey(
                        name: "FK_BankAccounts_AccountHolders_AccountHolderId",
                        column: x => x.AccountHolderId,
                        principalTable: "AccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BankAccounts_BankAccountIdentifier_BankAccountIdentifierIban",
                        column: x => x.BankAccountIdentifierIban,
                        principalTable: "BankAccountIdentifier",
                        principalColumn: "Iban",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ExternalParties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountIdentifierIban = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalParties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalParties_BankAccountIdentifier_BankAccountIdentifierIban",
                        column: x => x.BankAccountIdentifierIban,
                        principalTable: "BankAccountIdentifier",
                        principalColumn: "Iban",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BankAccountAccountNumber = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    TransactionCategoryId = table.Column<int>(type: "int", nullable: false),
                    ExternalPartyId = table.Column<int>(type: "int", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CounterPartTransactionToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_BankAccounts_BankAccountAccountNumber",
                        column: x => x.BankAccountAccountNumber,
                        principalTable: "BankAccounts",
                        principalColumn: "AccountNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_ExternalParties_ExternalPartyId",
                        column: x => x.ExternalPartyId,
                        principalTable: "ExternalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AccountHolders",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Patrick Hilgarth" },
                    { 2, "Ilona Schuhmacher" }
                });

            migrationBuilder.InsertData(
                table: "BankAccountIdentifier",
                columns: new[] { "Iban", "Bic" },
                values: new object[,]
                {
                    { "DE21654913200071808000", "GENODES1VBL" },
                    { "DE21654913200071808019", "GENODES1VBL" },
                    { "DE21654913200071808400", "GENODES1VBL" },
                    { "DE66654913200027911004", "GENODES1VBL" },
                    { "DE66654913200027911403", "GENODES1VBL" },
                    { "Arbeitgeber_Iban", "Arbeitgeber_Bic" },
                    { "Aldi_Iban", "Aldi_Bic" },
                    { "Iban-Negative-Sample", "Sample Bic Negative" },
                    { "Iban-0-Sample", "Sample Bic 0" }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nicht zugewiesen" },
                    { 2, "Essen, Trinken" }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "AccountNumber", "AccountHolderId", "AccountType", "Bank", "BankAccountIdentifierIban" },
                values: new object[,]
                {
                    { "71808000", 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808000" },
                    { "71808019", 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808019" },
                    { "71808400", 1, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808400" },
                    { "27911004", 2, "Girokonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911004" },
                    { "27911403", 2, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911403" },
                    { "Negative-Sample", 2, "Sample Negative Account", "Sample Bank", "Iban-Negative-Sample" },
                    { "0-Sample", 2, "Sample 0 Account", "Sample Bank", "Iban-0-Sample" }
                });

            migrationBuilder.InsertData(
                table: "ExternalParties",
                columns: new[] { "Id", "BankAccountIdentifierIban", "Name" },
                values: new object[,]
                {
                    { 1, "Arbeitgeber_Iban", "Arbeitgeber" },
                    { 2, "Aldi_Iban", "Aldi" }
                });

            migrationBuilder.InsertData(
                table: "Transactions",
                columns: new[] { "Id", "Amount", "BankAccountAccountNumber", "CounterPartTransactionToken", "Date", "ExternalPartyId", "Reference", "TransactionCategoryId" },
                values: new object[,]
                {
                    { 1, 500m, "71808000", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Kontoinitialisierung", 1 },
                    { 7, -100m, "71808000", "TOKEN: Umbuchung 000 - 019", new DateTime(2021, 2, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Umbuchung 000 - 019", 1 },
                    { 10, 10m, "71808000", "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2", new DateTime(2021, 2, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Umbuchung von 000 Cat1 - 000 Cat2", 2 },
                    { 11, -10m, "71808000", "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2", new DateTime(2021, 2, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Umbuchung von 000 Cat1 - 000 Cat2", 1 },
                    { 2, 500m, "71808019", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Kontoinitialisierung", 1 },
                    { 6, 100m, "71808019", "TOKEN: Umbuchung 000 - 019", new DateTime(2021, 2, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Umbuchung 000 - 019", 1 },
                    { 3, 500m, "71808400", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Kontoinitialisierung", 1 },
                    { 4, 500m, "27911004", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Kontoinitialisierung", 1 },
                    { 5, 500m, "27911403", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "Kontoinitialisierung", 1 },
                    { 8, 50m, "71808000", null, new DateTime(2021, 3, 1, 15, 30, 0, 0, DateTimeKind.Local), 1, "Einnahme von extern, z.B. Arbeitgeber", 1 },
                    { 9, -75m, "71808000", null, new DateTime(2021, 3, 1, 15, 30, 0, 0, DateTimeKind.Local), 2, "Ausgabe nach extern, z.B. Aldi Kartenzahlung", 2 },
                    { 13, -250m, "Negative-Sample", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "[Kontoinitialisierung]", 1 },
                    { 12, 0m, "0-Sample", null, new DateTime(2021, 1, 1, 15, 30, 0, 0, DateTimeKind.Local), null, "[Kontoinitialisierung]", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_AccountHolderId",
                table: "BankAccounts",
                column: "AccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_BankAccountIdentifierIban",
                table: "BankAccounts",
                column: "BankAccountIdentifierIban");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalParties_BankAccountIdentifierIban",
                table: "ExternalParties",
                column: "BankAccountIdentifierIban");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountAccountNumber",
                table: "Transactions",
                column: "BankAccountAccountNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_ExternalPartyId",
                table: "Transactions",
                column: "ExternalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_TransactionCategoryId",
                table: "Transactions",
                column: "TransactionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "ExternalParties");

            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropTable(
                name: "AccountHolders");

            migrationBuilder.DropTable(
                name: "BankAccountIdentifier");
        }
    }
}
