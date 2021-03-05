using Microsoft.EntityFrameworkCore.Migrations;

namespace XFin.API.DAL.Migrations
{
    public partial class FixFKBankAccount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CounterPartTransaction");

            migrationBuilder.DropColumn(
                name: "BankAccountIban",
                table: "BankAccounts");

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "BankAccountIdentifierIban",
                value: "DE21654913200071808000");

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "BankAccountIdentifierIban",
                value: "DE21654913200071808019");

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 3,
                column: "BankAccountIdentifierIban",
                value: "DE21654913200071808400");

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 4,
                column: "BankAccountIdentifierIban",
                value: "DE66654913200027911004");

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 5,
                column: "BankAccountIdentifierIban",
                value: "DE66654913200027911403");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BankAccountIban",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: true);

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

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "DE21654913200071808000", null });

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "DE21654913200071808019", null });

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "DE21654913200071808400", null });

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "DE66654913200027911004", null });

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "DE66654913200027911403", null });

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

            migrationBuilder.CreateIndex(
                name: "IX_CounterPartTransaction_TransactionId",
                table: "CounterPartTransaction",
                column: "TransactionId");
        }
    }
}
