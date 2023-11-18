using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace maxutova_altynay_09.Migrations
{
    /// <inheritdoc />
    public partial class ReviewDeleteImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Reviews");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Reviews",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
