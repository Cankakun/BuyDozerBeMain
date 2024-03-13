using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BuyDozerBeMain.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDetailRent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DetailRents",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    TransactionId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateRent = table.Column<DateOnly>(type: "date", nullable: false),
                    DateReturn = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DetailRents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DetailRents_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_DetailRents_TransactionId",
                table: "DetailRents",
                column: "TransactionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DetailRents");
        }
    }
}
