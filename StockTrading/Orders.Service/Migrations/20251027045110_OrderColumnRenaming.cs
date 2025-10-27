using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Service.Migrations
{
    /// <inheritdoc />
    public partial class OrderColumnRenaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocks_TickerId",
                schema: "app",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TickerId",
                schema: "app",
                table: "Orders",
                newName: "StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_TickerId",
                schema: "app",
                table: "Orders",
                newName: "IX_Orders_StockId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocks_StockId",
                schema: "app",
                table: "Orders",
                column: "StockId",
                principalSchema: "app",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Stocks_StockId",
                schema: "app",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "StockId",
                schema: "app",
                table: "Orders",
                newName: "TickerId");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_StockId",
                schema: "app",
                table: "Orders",
                newName: "IX_Orders_TickerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Stocks_TickerId",
                schema: "app",
                table: "Orders",
                column: "TickerId",
                principalSchema: "app",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
