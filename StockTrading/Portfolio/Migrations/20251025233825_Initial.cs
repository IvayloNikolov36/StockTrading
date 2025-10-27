using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Portfolio.Service.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "app");

            migrationBuilder.CreateTable(
                name: "Stocks",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Ticker = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CompanyName = table.Column<string>(type: "character varying(90)", maxLength: 90, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPortfolios",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPortfolios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "app",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    TickerId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Side = table.Column<int>(type: "integer", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Stocks_TickerId",
                        column: x => x.TickerId,
                        principalSchema: "app",
                        principalTable: "Stocks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Orders_UserPortfolios_UserId",
                        column: x => x.UserId,
                        principalSchema: "app",
                        principalTable: "UserPortfolios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "app",
                table: "Stocks",
                columns: new[] { "Id", "CompanyName", "CreatedOn", "ModifiedOn", "Ticker" },
                values: new object[,]
                {
                    { 1, "NVIDIA Corporation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "NVDA" },
                    { 2, "Apple Inc.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AAPL" },
                    { 3, "Microsoft Corporation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "MSFT" },
                    { 4, "Alphabet Inc.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "GOOGL" },
                    { 5, "Amazon.com, Inc.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AMZN" },
                    { 6, "Meta Platforms, Inc.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "META" },
                    { 7, "Broadcom Inc.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "AVGO" },
                    { 8, "Tesla, Inc.", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "TSLA" },
                    { 9, "Oracle Corporation", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "ORCL" },
                    { 10, "Eli Lilly and Company", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "LLY" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_TickerId",
                schema: "app",
                table: "Orders",
                column: "TickerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                schema: "app",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_Ticker",
                schema: "app",
                table: "Stocks",
                column: "Ticker",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders",
                schema: "app");

            migrationBuilder.DropTable(
                name: "Stocks",
                schema: "app");

            migrationBuilder.DropTable(
                name: "UserPortfolios",
                schema: "app");
        }
    }
}
