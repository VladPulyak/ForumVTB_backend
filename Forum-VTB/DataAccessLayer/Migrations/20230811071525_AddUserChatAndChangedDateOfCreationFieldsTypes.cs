using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserChatAndChangedDateOfCreationFieldsTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "UserMessages",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.AddColumn<string>(
                name: "ChatId",
                table: "UserMessages",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "Adverts",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp(3) without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "AdvertComments",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp");

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstUserId = table.Column<string>(type: "text", nullable: true),
                    SecondUserId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserChats_AspNetUsers_FirstUserId",
                        column: x => x.FirstUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_AspNetUsers_SecondUserId",
                        column: x => x.SecondUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ChatId",
                table: "UserMessages",
                column: "ChatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_FirstUserId",
                table: "UserChats",
                column: "FirstUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_SecondUserId",
                table: "UserChats",
                column: "SecondUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages",
                column: "ChatId",
                principalTable: "UserChats",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages");

            migrationBuilder.DropTable(
                name: "UserChats");

            migrationBuilder.DropIndex(
                name: "IX_UserMessages_ChatId",
                table: "UserMessages");

            migrationBuilder.DropColumn(
                name: "ChatId",
                table: "UserMessages");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "UserMessages",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "Adverts",
                type: "timestamp(3) without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateOfCreation",
                table: "AdvertComments",
                type: "timestamp",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
