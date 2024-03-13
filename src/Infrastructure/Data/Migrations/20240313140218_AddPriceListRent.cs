using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyDozerBeMain.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPriceListRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PriceListRents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NameUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PriceRentUnit = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceListRents", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PriceListRents");
        }
    }
}
