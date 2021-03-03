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
                name: "TransactionCategory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransactionCategory", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolderId = table.Column<int>(type: "int", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountIban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountIdentifierIban = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
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
                name: "ExternalParty",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountIban = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BankAccountIdentifierIban = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalParty", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalParty_BankAccountIdentifier_BankAccountIdentifierIban",
                        column: x => x.BankAccountIdentifierIban,
                        principalTable: "BankAccountIdentifier",
                        principalColumn: "Iban",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Transaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BankAccountId = table.Column<int>(type: "int", nullable: false),
                    TransactionCategoryId = table.Column<int>(type: "int", nullable: false),
                    CounterPartTransactionToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExternalPartyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transaction_BankAccounts_BankAccountId",
                        column: x => x.BankAccountId,
                        principalTable: "BankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transaction_ExternalParty_ExternalPartyId",
                        column: x => x.ExternalPartyId,
                        principalTable: "ExternalParty",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Transaction_TransactionCategory_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CounterPartTransaction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CounterPartTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CounterPartTransaction_Transaction_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    { "Arbeitgeber_Iban", "Arbeitgeber_Bic" },
                    { "DE66654913200027911403", "GENODES1VBL" },
                    { "DE66654913200027911004", "GENODES1VBL" },
                    { "Aldi_Iban", "Aldi_Bic" },
                    { "DE21654913200071808019", "GENODES1VBL" },
                    { "DE21654913200071808000", "GENODES1VBL" },
                    { "DE21654913200071808400", "GENODES1VBL" }
                });

            migrationBuilder.InsertData(
                table: "CounterPartTransaction",
                columns: new[] { "Id", "TransactionId" },
                values: new object[,]
                {
                    { 1, null },
                    { 2, null },
                    { 3, null },
                    { 4, null }
                });

            migrationBuilder.InsertData(
                table: "ExternalParty",
                columns: new[] { "Id", "BankAccountIban", "BankAccountIdentifierIban", "Name" },
                values: new object[,]
                {
                    { 1, "Arbeitgeber_Iban", null, "Arbeitgeber" },
                    { 2, "Aldi_Iban", null, "Aldi" }
                });

            migrationBuilder.InsertData(
                table: "TransactionCategory",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Nicht zugewiesen" },
                    { 2, "Essen, Trinken" }
                });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountHolderId", "AccountType", "Bank", "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[,]
                {
                    { 1, 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808000", null },
                    { 2, 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808019", null },
                    { 3, 1, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808400", null },
                    { 4, 2, "Girokonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911004", null },
                    { 5, 2, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911403", null }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "Amount", "BankAccountId", "CounterPartTransactionToken", "Date", "ExternalPartyId", "Reference", "TransactionCategoryId" },
                values: new object[,]
                {
                    { 1, 500m, 1, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Kontoinitialisierung", 1 },
                    { 7, -100m, 1, "TOKEN: Umbuchung 000 - 019", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Umbuchung 000 - 019", 1 },
                    { 8, 50m, 1, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1, "Einnahme von extern, z.B. Arbeitgeber", 1 },
                    { 9, -75m, 1, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2, "Ausgabe nach extern, z.B. Aldi Kartenzahlung", 2 },
                    { 10, 10m, 1, "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Umbuchung von 000 Cat1 - 000 Cat2", 2 },
                    { 11, -10m, 1, "TOKEN: Umbuchung von 000 Cat1 - 000 Cat2", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Umbuchung von 000 Cat1 - 000 Cat2", 1 },
                    { 2, 500m, 2, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Kontoinitialisierung", 1 },
                    { 6, 100m, 2, "TOKEN: Umbuchung 000 - 019", new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Umbuchung 000 - 019", 1 },
                    { 3, 500m, 3, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Kontoinitialisierung", 1 },
                    { 4, 500m, 4, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Kontoinitialisierung", 1 },
                    { 5, 500m, 5, null, new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null, "Kontoinitialisierung", 1 }
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
                name: "IX_CounterPartTransaction_TransactionId",
                table: "CounterPartTransaction",
                column: "TransactionId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalParty_BankAccountIdentifierIban",
                table: "ExternalParty",
                column: "BankAccountIdentifierIban");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_BankAccountId",
                table: "Transaction",
                column: "BankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_ExternalPartyId",
                table: "Transaction",
                column: "ExternalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_TransactionCategoryId",
                table: "Transaction",
                column: "TransactionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterPartTransaction");

            migrationBuilder.DropTable(
                name: "Transaction");

            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "ExternalParty");

            migrationBuilder.DropTable(
                name: "TransactionCategory");

            migrationBuilder.DropTable(
                name: "AccountHolders");

            migrationBuilder.DropTable(
                name: "BankAccountIdentifier");
        }
    }
}
