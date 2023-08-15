using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddMainPhotoToAdvertAndWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Works",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainPhoto",
                table: "Adverts",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "MainPhoto",
                table: "Adverts");
        }
    }
}
