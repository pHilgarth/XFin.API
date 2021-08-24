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
                name: "ExternalParties",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalParties", x => x.Id);
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
                name: "InternalBankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountHolderId = table.Column<int>(type: "int", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalBankAccounts_AccountHolders_AccountHolderId",
                        column: x => x.AccountHolderId,
                        principalTable: "AccountHolders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalBankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalPartyId = table.Column<int>(type: "int", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bic = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalBankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalBankAccounts_ExternalParties_ExternalPartyId",
                        column: x => x.ExternalPartyId,
                        principalTable: "ExternalParties",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InternalTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InternalBankAccountId = table.Column<int>(type: "int", nullable: false),
                    TransactionCategoryId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CounterPartTransactionToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InternalTransactions_InternalBankAccounts_InternalBankAccountId",
                        column: x => x.InternalBankAccountId,
                        principalTable: "InternalBankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InternalTransactions_TransactionCategories_TransactionCategoryId",
                        column: x => x.TransactionCategoryId,
                        principalTable: "TransactionCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExternalTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExternalBankAccountId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CounterPartTransactionToken = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExternalTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExternalTransactions_ExternalBankAccounts_ExternalBankAccountId",
                        column: x => x.ExternalBankAccountId,
                        principalTable: "ExternalBankAccounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Nicht zugewiesen" });

            migrationBuilder.InsertData(
                table: "TransactionCategories",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Essen, Trinken" });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalBankAccounts_ExternalPartyId",
                table: "ExternalBankAccounts",
                column: "ExternalPartyId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalTransactions_ExternalBankAccountId",
                table: "ExternalTransactions",
                column: "ExternalBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalBankAccounts_AccountHolderId",
                table: "InternalBankAccounts",
                column: "AccountHolderId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransactions_InternalBankAccountId",
                table: "InternalTransactions",
                column: "InternalBankAccountId");

            migrationBuilder.CreateIndex(
                name: "IX_InternalTransactions_TransactionCategoryId",
                table: "InternalTransactions",
                column: "TransactionCategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExternalTransactions");

            migrationBuilder.DropTable(
                name: "InternalTransactions");

            migrationBuilder.DropTable(
                name: "ExternalBankAccounts");

            migrationBuilder.DropTable(
                name: "InternalBankAccounts");

            migrationBuilder.DropTable(
                name: "TransactionCategories");

            migrationBuilder.DropTable(
                name: "ExternalParties");

            migrationBuilder.DropTable(
                name: "AccountHolders");
        }
    }
}
