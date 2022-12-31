using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class TransactionModification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditCards_CCId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CCId",
                table: "Transactions",
                newName: "CreditCardId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CCId",
                table: "Transactions",
                newName: "IX_Transactions_CreditCardId");

            migrationBuilder.AddColumn<string>(
                name: "CCIban",
                table: "Transactions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditCards_CreditCardId",
                table: "Transactions",
                column: "CreditCardId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditCards_CreditCardId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CCIban",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CreditCardId",
                table: "Transactions",
                newName: "CCId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CreditCardId",
                table: "Transactions",
                newName: "IX_Transactions_CCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditCards_CCId",
                table: "Transactions",
                column: "CCId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId");
        }
    }
}
