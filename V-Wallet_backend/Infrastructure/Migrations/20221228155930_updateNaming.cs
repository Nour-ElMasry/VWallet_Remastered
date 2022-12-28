using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    public partial class updateNaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditCards_SendingCCId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SendingCCId",
                table: "Transactions",
                newName: "CCId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SendingCCId",
                table: "Transactions",
                newName: "IX_Transactions_CCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditCards_CCId",
                table: "Transactions",
                column: "CCId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CreditCards_CCId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CCId",
                table: "Transactions",
                newName: "SendingCCId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_CCId",
                table: "Transactions",
                newName: "IX_Transactions_SendingCCId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CreditCards_SendingCCId",
                table: "Transactions",
                column: "SendingCCId",
                principalTable: "CreditCards",
                principalColumn: "CreditCardId");
        }
    }
}
