using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedUserTheme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Subsections_SubsectionId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Adverts_AdvertId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AdvertId",
                table: "Favourites",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "character varying(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OtherInfo",
                table: "Events",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "character varying(300)",
                maxLength: 300,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UserThemes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Theme = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserThemes_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserThemes_UserId",
                table: "UserThemes",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Subsections_SubsectionId",
                table: "Events",
                column: "SubsectionId",
                principalTable: "Subsections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Adverts_AdvertId",
                table: "Favourites",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Subsections_SubsectionId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Adverts_AdvertId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites");

            migrationBuilder.DropTable(
                name: "UserThemes");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "Favourites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "AdvertId",
                table: "Favourites",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "OtherInfo",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Events",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(300)",
                oldMaxLength: 300);

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Subsections_SubsectionId",
                table: "Events",
                column: "SubsectionId",
                principalTable: "Subsections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Adverts_AdvertId",
                table: "Favourites",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_AspNetUsers_UserId",
                table: "Favourites",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
