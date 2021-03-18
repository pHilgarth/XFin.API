using Microsoft.EntityFrameworkCore.Migrations;

namespace XFin.API.DAL.Migrations
{
    public partial class UpdateBankAccountPK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankAccountId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyColumnType: "int",
                keyValue: 5);

            migrationBuilder.DropColumn(
                name: "BankAccountId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BankAccounts");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountAccountNumber",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountNumber",
                table: "BankAccounts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "AccountNumber");

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "AccountNumber", "AccountHolderId", "AccountType", "Bank", "BankAccountIdentifierIban" },
                values: new object[,]
                {
                    { "71808000", 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808000" },
                    { "71808019", 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808019" },
                    { "71808400", 1, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808400" },
                    { "27911004", 2, "Girokonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911004" },
                    { "27911403", 2, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911403" }
                });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "BankAccountAccountNumber",
                value: "71808000");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "BankAccountAccountNumber",
                value: "71808019");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "BankAccountAccountNumber",
                value: "71808400");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4,
                column: "BankAccountAccountNumber",
                value: "27911004");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "BankAccountAccountNumber",
                value: "27911403");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6,
                column: "BankAccountAccountNumber",
                value: "71808019");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7,
                column: "BankAccountAccountNumber",
                value: "71808000");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 8,
                column: "BankAccountAccountNumber",
                value: "71808000");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 9,
                column: "BankAccountAccountNumber",
                value: "71808000");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 10,
                column: "BankAccountAccountNumber",
                value: "71808000");

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 11,
                column: "BankAccountAccountNumber",
                value: "71808000");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountAccountNumber",
                table: "Transactions",
                column: "BankAccountAccountNumber");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountAccountNumber",
                table: "Transactions",
                column: "BankAccountAccountNumber",
                principalTable: "BankAccounts",
                principalColumn: "AccountNumber",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountAccountNumber",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_BankAccountAccountNumber",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNumber",
                keyColumnType: "nvarchar(450)",
                keyValue: "27911004");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNumber",
                keyColumnType: "nvarchar(450)",
                keyValue: "27911403");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNumber",
                keyColumnType: "nvarchar(450)",
                keyValue: "71808000");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNumber",
                keyColumnType: "nvarchar(450)",
                keyValue: "71808019");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "AccountNumber",
                keyColumnType: "nvarchar(450)",
                keyValue: "71808400");

            migrationBuilder.DropColumn(
                name: "BankAccountAccountNumber",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "AccountNumber",
                table: "BankAccounts");

            migrationBuilder.AddColumn<int>(
                name: "BankAccountId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BankAccounts",
                table: "BankAccounts",
                column: "Id");

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountHolderId", "AccountType", "Bank", "BankAccountIdentifierIban" },
                values: new object[,]
                {
                    { 1, 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808000" },
                    { 2, 1, "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808019" },
                    { 3, 1, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE21654913200071808400" },
                    { 4, 2, "Girokonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911004" },
                    { 5, 2, "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "DE66654913200027911403" }
                });

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 1,
                column: "BankAccountId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 2,
                column: "BankAccountId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 3,
                column: "BankAccountId",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 4,
                column: "BankAccountId",
                value: 4);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 5,
                column: "BankAccountId",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 6,
                column: "BankAccountId",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 7,
                column: "BankAccountId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 8,
                column: "BankAccountId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 9,
                column: "BankAccountId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 10,
                column: "BankAccountId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Transactions",
                keyColumn: "Id",
                keyValue: 11,
                column: "BankAccountId",
                value: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_BankAccountId",
                table: "Transactions",
                column: "BankAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_BankAccounts_BankAccountId",
                table: "Transactions",
                column: "BankAccountId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
