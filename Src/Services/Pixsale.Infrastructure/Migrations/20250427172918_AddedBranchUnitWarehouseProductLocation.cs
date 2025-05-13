using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pixsale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddedBranchUnitWarehouseProductLocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StockLocation",
                table: "StockTransactions");

            migrationBuilder.AddColumn<Guid>(
                name: "TargetWarehouseId",
                table: "StockTransactions",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "StockTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WarehouseId",
                table: "StockTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Branches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Branches", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    shortName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConversionFactor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BranchWarehouses",
                columns: table => new
                {
                    BranchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BranchWarehouses", x => new { x.BranchId, x.WarehouseId });
                    table.ForeignKey(
                        name: "FK_BranchWarehouses_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BranchWarehouses_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductLocations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecificLocation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLocations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLocations_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductLocations_Warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_TargetWarehouseId",
                table: "StockTransactions",
                column: "TargetWarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_UnitId",
                table: "StockTransactions",
                column: "UnitId");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_WarehouseId",
                table: "StockTransactions",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_BranchWarehouses_WarehouseId",
                table: "BranchWarehouses",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocations_ProductId",
                table: "ProductLocations",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLocations_WarehouseId",
                table: "ProductLocations",
                column: "WarehouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Units_UnitId",
                table: "StockTransactions",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Warehouses_TargetWarehouseId",
                table: "StockTransactions",
                column: "TargetWarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Warehouses_WarehouseId",
                table: "StockTransactions",
                column: "WarehouseId",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Units_UnitId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Warehouses_TargetWarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Warehouses_WarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropTable(
                name: "BranchWarehouses");

            migrationBuilder.DropTable(
                name: "ProductLocations");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Branches");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_TargetWarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_UnitId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_WarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "TargetWarehouseId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "WarehouseId",
                table: "StockTransactions");

            migrationBuilder.AddColumn<string>(
                name: "StockLocation",
                table: "StockTransactions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
