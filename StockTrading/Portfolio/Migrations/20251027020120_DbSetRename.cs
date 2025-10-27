using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Portfolio.Service.Migrations
{
    /// <inheritdoc />
    public partial class DbSetRename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_UserPortfolios_UserId",
                schema: "app",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPortfolios",
                schema: "app",
                table: "UserPortfolios");

            migrationBuilder.RenameTable(
                name: "UserPortfolios",
                schema: "app",
                newName: "Users",
                newSchema: "app");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                schema: "app",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "app",
                table: "Orders",
                column: "UserId",
                principalSchema: "app",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Users_UserId",
                schema: "app",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                schema: "app",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                schema: "app",
                newName: "UserPortfolios",
                newSchema: "app");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPortfolios",
                schema: "app",
                table: "UserPortfolios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_UserPortfolios_UserId",
                schema: "app",
                table: "Orders",
                column: "UserId",
                principalSchema: "app",
                principalTable: "UserPortfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
