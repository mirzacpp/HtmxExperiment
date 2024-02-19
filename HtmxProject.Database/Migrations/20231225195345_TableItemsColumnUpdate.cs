using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HtmxProject.Database.Migrations
{
    /// <inheritdoc />
    public partial class TableItemsColumnUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "description",
                table: "item",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "manufactured_at",
                table: "item",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "description",
                table: "item");

            migrationBuilder.DropColumn(
                name: "manufactured_at",
                table: "item");
        }
    }
}
