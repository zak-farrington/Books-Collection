using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCollection.Api.Migrations
{
    /// <inheritdoc />
    public partial class CreationDateIsbn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Book",
                newName: "Isbn");

            migrationBuilder.RenameColumn(
                name: "AuthorUid",
                table: "Book",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "Isbn",
                table: "Book",
                newName: "ThumbnailUrl");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Book",
                newName: "AuthorUid");
        }
    }
}
