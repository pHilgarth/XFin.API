using Microsoft.EntityFrameworkCore.Migrations;

namespace XFin.API.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Depositors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depositors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BankAccounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepositorId = table.Column<int>(type: "int", nullable: false),
                    AccountNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Iban = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bic = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bank = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccountType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankAccounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BankAccounts_Depositors_DepositorId",
                        column: x => x.DepositorId,
                        principalTable: "Depositors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Depositors",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Patrick Hilgarth" });

            migrationBuilder.InsertData(
                table: "Depositors",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Ilona Schuhmacher" });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountNumber", "AccountType", "Bank", "Bic", "DepositorId", "Iban" },
                values: new object[,]
                {
                    { 1, "71808000", "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "GENODES1VBL", 1, "DE66654913200071808000" },
                    { 2, "71808019", "Konto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "GENODES1VBL", 1, "DE214913200071808019" },
                    { 3, "71808400", "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "GENODES1VBL", 1, "DE45654913200071808400" },
                    { 4, "27911004", "Girokonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "GENODES1VBL", 2, "DE45654913200027911004" },
                    { 5, "27911403", "Sparkonto", "Volksbank-Raiffeisenbank Laupheim-Illertal eG", "GENODES1VBL", 2, "DE45654913200027911403" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BankAccounts_DepositorId",
                table: "BankAccounts",
                column: "DepositorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankAccounts");

            migrationBuilder.DropTable(
                name: "Depositors");
        }
    }
}
