using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pixsale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedNationalIdPropertyToCustomerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NationalId",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NationalId",
                table: "Customers");
        }
    }
}
