using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Prices.Service.Migrations
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
                name: "Stocks",
                schema: "app");
        }
    }
}
