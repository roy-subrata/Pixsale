using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pixsale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdStockLocationColumToStockTransectiontable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StockLocation",
                table: "StockTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockLocation",
                table: "StockTransactions");
        }
    }
}
