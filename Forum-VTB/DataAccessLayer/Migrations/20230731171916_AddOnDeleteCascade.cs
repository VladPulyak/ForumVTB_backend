using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddOnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertComments_Adverts_AdvertId",
                table: "AdvertComments");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertFiles_Adverts_AdvertId",
                table: "AdvertFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertComments_Adverts_AdvertId",
                table: "AdvertComments",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertFiles_Adverts_AdvertId",
                table: "AdvertFiles",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertComments_Adverts_AdvertId",
                table: "AdvertComments");

            migrationBuilder.DropForeignKey(
                name: "FK_AdvertFiles_Adverts_AdvertId",
                table: "AdvertFiles");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertComments_Adverts_AdvertId",
                table: "AdvertComments",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertFiles_Adverts_AdvertId",
                table: "AdvertFiles",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id");
        }
    }
}
