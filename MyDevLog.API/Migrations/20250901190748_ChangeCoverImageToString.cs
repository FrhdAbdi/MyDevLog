using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyDevLog.API.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCoverImageToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImage",
                table: "Posts");

            migrationBuilder.AddColumn<string>(
                name: "CoverImageFileName",
                table: "Posts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImageFileName",
                table: "Posts");

            migrationBuilder.AddColumn<byte[]>(
                name: "CoverImage",
                table: "Posts",
                type: "bytea",
                nullable: true);
        }
    }
}
