using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCollection.Api.Migrations
{
    /// <inheritdoc />
    public partial class LastUpdatedDate_MsrpUnit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "MsrpUnit",
                table: "Book",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "Book");

            migrationBuilder.DropColumn(
                name: "MsrpUnit",
                table: "Book");
        }
    }
}
