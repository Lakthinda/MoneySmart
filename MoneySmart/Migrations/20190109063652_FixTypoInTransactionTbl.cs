using Microsoft.EntityFrameworkCore.Migrations;

namespace MoneySmart.API.Migrations
{
    public partial class FixTypoInTransactionTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SavingAccounts_SavingAcountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SavingAcountId",
                table: "Transactions",
                newName: "SavingAccountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SavingAcountId",
                table: "Transactions",
                newName: "IX_Transactions_SavingAccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SavingAccounts_SavingAccountId",
                table: "Transactions",
                column: "SavingAccountId",
                principalTable: "SavingAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_SavingAccounts_SavingAccountId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "SavingAccountId",
                table: "Transactions",
                newName: "SavingAcountId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_SavingAccountId",
                table: "Transactions",
                newName: "IX_Transactions_SavingAcountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_SavingAccounts_SavingAcountId",
                table: "Transactions",
                column: "SavingAcountId",
                principalTable: "SavingAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
