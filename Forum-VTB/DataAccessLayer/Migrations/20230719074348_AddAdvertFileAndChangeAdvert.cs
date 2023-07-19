using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertFileAndChangeAdvert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_AdvertMessages_MessageId",
                table: "MessageFiles");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Adverts",
                newName: "Title");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Adverts",
                type: "character varying(3000)",
                maxLength: 3000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Price",
                table: "Adverts",
                type: "character varying(10)",
                maxLength: 10,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "AdvertFile",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdvertId = table.Column<string>(type: "text", nullable: true),
                    FileURL = table.Column<string>(type: "text", nullable: true),
                    AdvertId1 = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertFile_Adverts_AdvertId1",
                        column: x => x.AdvertId1,
                        principalTable: "Adverts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertFile_AdvertId1",
                table: "AdvertFile",
                column: "AdvertId1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertFile");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Adverts");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Adverts");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Adverts",
                newName: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_AdvertMessages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "AdvertMessages",
                principalColumn: "Id");
        }
    }
}
