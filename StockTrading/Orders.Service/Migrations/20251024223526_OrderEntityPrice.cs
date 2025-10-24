using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orders.Service.Migrations
{
    /// <inheritdoc />
    public partial class OrderEntityPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Price",
                schema: "app",
                table: "Orders",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                schema: "app",
                table: "Orders");
        }
    }
}
