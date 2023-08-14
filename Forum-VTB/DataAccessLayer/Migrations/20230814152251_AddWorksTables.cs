using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddWorksTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Favourites");

            migrationBuilder.DropColumn(
                name: "IsReply",
                table: "AdvertComments");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Adverts",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdvertFavourites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AdvertId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertFavourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertFavourites_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertFavourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Works",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    SubsectionId = table.Column<int>(type: "integer", nullable: true),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    Price = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    PhoneNumber = table.Column<string>(type: "character varying(13)", maxLength: 13, nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Works", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Works_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Works_Subsections_SubsectionId",
                        column: x => x.SubsectionId,
                        principalTable: "Subsections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WorkComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    WorkId = table.Column<string>(type: "text", nullable: true),
                    ParentCommentId = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkComments_WorkComments_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "WorkComments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WorkComments_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFavourites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    WorkId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFavourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFavourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkFavourites_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    WorkId = table.Column<string>(type: "text", nullable: true),
                    FileURL = table.Column<string>(type: "text", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkFiles_Works_WorkId",
                        column: x => x.WorkId,
                        principalTable: "Works",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertFavourites_AdvertId",
                table: "AdvertFavourites",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertFavourites_UserId_AdvertId",
                table: "AdvertFavourites",
                columns: new[] { "UserId", "AdvertId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkComments_ParentCommentId",
                table: "WorkComments",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkComments_UserId",
                table: "WorkComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkComments_WorkId",
                table: "WorkComments",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFavourites_UserId_WorkId",
                table: "WorkFavourites",
                columns: new[] { "UserId", "WorkId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkFavourites_WorkId",
                table: "WorkFavourites",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkFiles_WorkId",
                table: "WorkFiles",
                column: "WorkId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_SubsectionId",
                table: "Works",
                column: "SubsectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Works_UserId",
                table: "Works",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvertFavourites");

            migrationBuilder.DropTable(
                name: "WorkComments");

            migrationBuilder.DropTable(
                name: "WorkFavourites");

            migrationBuilder.DropTable(
                name: "WorkFiles");

            migrationBuilder.DropTable(
                name: "Works");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Adverts");

            migrationBuilder.AddColumn<bool>(
                name: "IsReply",
                table: "AdvertComments",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Favourites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    AdvertId = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Favourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Favourites_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Favourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_AdvertId",
                table: "Favourites",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId_AdvertId",
                table: "Favourites",
                columns: new[] { "UserId", "AdvertId" },
                unique: true);
        }
    }
}
