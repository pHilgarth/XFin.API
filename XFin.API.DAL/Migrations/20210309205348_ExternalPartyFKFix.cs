using Microsoft.EntityFrameworkCore.Migrations;

namespace XFin.API.DAL.Migrations
{
    public partial class ExternalPartyFKFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalParty_BankAccountIdentifier_BankAccountIdentifierIban",
                table: "ExternalParty");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ExternalParty_ExternalPartyId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalParty",
                table: "ExternalParty");

            migrationBuilder.DropColumn(
                name: "BankAccountIban",
                table: "ExternalParty");

            migrationBuilder.RenameTable(
                name: "ExternalParty",
                newName: "ExternalParties");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalParty_BankAccountIdentifierIban",
                table: "ExternalParties",
                newName: "IX_ExternalParties_BankAccountIdentifierIban");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalParties",
                table: "ExternalParties",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ExternalParties",
                keyColumn: "Id",
                keyValue: 1,
                column: "BankAccountIdentifierIban",
                value: "Arbeitgeber_Iban");

            migrationBuilder.UpdateData(
                table: "ExternalParties",
                keyColumn: "Id",
                keyValue: 2,
                column: "BankAccountIdentifierIban",
                value: "Aldi_Iban");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalParties_BankAccountIdentifier_BankAccountIdentifierIban",
                table: "ExternalParties",
                column: "BankAccountIdentifierIban",
                principalTable: "BankAccountIdentifier",
                principalColumn: "Iban",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ExternalParties_ExternalPartyId",
                table: "Transactions",
                column: "ExternalPartyId",
                principalTable: "ExternalParties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalParties_BankAccountIdentifier_BankAccountIdentifierIban",
                table: "ExternalParties");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_ExternalParties_ExternalPartyId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExternalParties",
                table: "ExternalParties");

            migrationBuilder.RenameTable(
                name: "ExternalParties",
                newName: "ExternalParty");

            migrationBuilder.RenameIndex(
                name: "IX_ExternalParties_BankAccountIdentifierIban",
                table: "ExternalParty",
                newName: "IX_ExternalParty_BankAccountIdentifierIban");

            migrationBuilder.AddColumn<string>(
                name: "BankAccountIban",
                table: "ExternalParty",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExternalParty",
                table: "ExternalParty",
                column: "Id");

            migrationBuilder.UpdateData(
                table: "ExternalParty",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "Arbeitgeber_Iban", null });

            migrationBuilder.UpdateData(
                table: "ExternalParty",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "BankAccountIban", "BankAccountIdentifierIban" },
                values: new object[] { "Aldi_Iban", null });

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalParty_BankAccountIdentifier_BankAccountIdentifierIban",
                table: "ExternalParty",
                column: "BankAccountIdentifierIban",
                principalTable: "BankAccountIdentifier",
                principalColumn: "Iban",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_ExternalParty_ExternalPartyId",
                table: "Transactions",
                column: "ExternalPartyId",
                principalTable: "ExternalParty",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
