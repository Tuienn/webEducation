using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CenterEnglishManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddUrlImgToClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UrlImg",
                table: "Classes",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "Classes");
        }
    }
}
