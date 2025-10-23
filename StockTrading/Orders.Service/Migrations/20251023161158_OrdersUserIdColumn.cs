using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Service.Migrations
{
    /// <inheritdoc />
    public partial class OrdersUserIdColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "app",
                table: "Orders",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "app",
                table: "Orders");
        }
    }
}
