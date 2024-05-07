using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyDozerBeMain.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddingPriceListRentToTransaction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NameUnit",
                table: "PriceListRents",
                newName: "NameRent");

            migrationBuilder.AddColumn<string>(
                name: "PriceListRentId",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_PriceListRentId",
                table: "Transactions",
                column: "PriceListRentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_PriceListRents_PriceListRentId",
                table: "Transactions",
                column: "PriceListRentId",
                principalTable: "PriceListRents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_PriceListRents_PriceListRentId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_PriceListRentId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "PriceListRentId",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "NameRent",
                table: "PriceListRents",
                newName: "NameUnit");
        }
    }
}
