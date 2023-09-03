using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DropForeignKeyInUserChats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Adverts_AdvertId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages");

            migrationBuilder.DropIndex(
                name: "IX_UserChats_AdvertId",
                table: "UserChats");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages",
                column: "ChatId",
                principalTable: "UserChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_AdvertId",
                table: "UserChats",
                column: "AdvertId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Adverts_AdvertId",
                table: "UserChats",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages",
                column: "ChatId",
                principalTable: "UserChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
