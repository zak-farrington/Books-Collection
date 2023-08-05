using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookCollection.Api.Migrations
{
    /// <inheritdoc />
    public partial class ThumbnailImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OtherCateogryName",
                table: "Book",
                newName: "ThumbnailUrl");

            migrationBuilder.AddColumn<string>(
                name: "OtherCategoryName",
                table: "Book",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OtherCategoryName",
                table: "Book");

            migrationBuilder.RenameColumn(
                name: "ThumbnailUrl",
                table: "Book",
                newName: "OtherCateogryName");
        }
    }
}
