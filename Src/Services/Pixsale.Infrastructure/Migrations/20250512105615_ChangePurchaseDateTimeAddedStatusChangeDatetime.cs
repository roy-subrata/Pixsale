using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pixsale.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangePurchaseDateTimeAddedStatusChangeDatetime : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_StockTransactions_Units_UnitId",
                table: "StockTransactions");

            migrationBuilder.DropIndex(
                name: "IX_StockTransactions_UnitId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "UnitId",
                table: "StockTransactions");

            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "Purchases");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Purchases",
                type: "datetime2",
                nullable: false,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "PurchaseDetails",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_PurchaseDetails_Products_ProductId",
                table: "PurchaseDetails");

            migrationBuilder.DropIndex(
                name: "IX_PurchaseDetails_ProductId",
                table: "PurchaseDetails");

            migrationBuilder.AddColumn<Guid>(
                name: "UnitId",
                table: "StockTransactions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.DropColumn( name: "PurchaseDate", table: "Purchases");

            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "Purchases",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: Guid.NewGuid());

            migrationBuilder.AlterColumn<Guid>(
                name: "UnitId",
                table: "Products",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_StockTransactions_UnitId",
                table: "StockTransactions",
                column: "UnitId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Units_UnitId",
                table: "Products",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StockTransactions_Units_UnitId",
                table: "StockTransactions",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
