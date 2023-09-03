using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddedTopicsEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FindComments_FindComments_ParentCommentId",
                table: "FindComments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserMessages_ParentMessageId",
                table: "UserMessages");

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    SubsectionId = table.Column<int>(type: "integer", nullable: true),
                    MainPhoto = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Topics_Subsections_SubsectionId",
                        column: x => x.SubsectionId,
                        principalTable: "Subsections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicFavourites",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    TopicId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicFavourites", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicFavourites_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicFavourites_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicFiles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    TopicId = table.Column<string>(type: "text", nullable: true),
                    FileURL = table.Column<string>(type: "text", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicFiles_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TopicMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    TopicId = table.Column<string>(type: "text", nullable: true),
                    ParentMessageId = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicMessages_TopicMessages_ParentMessageId",
                        column: x => x.ParentMessageId,
                        principalTable: "TopicMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TopicMessages_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicFavourites_TopicId",
                table: "TopicFavourites",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicFavourites_UserId_TopicId",
                table: "TopicFavourites",
                columns: new[] { "UserId", "TopicId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TopicFiles_TopicId",
                table: "TopicFiles",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicMessages_ParentMessageId",
                table: "TopicMessages",
                column: "ParentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicMessages_TopicId",
                table: "TopicMessages",
                column: "TopicId");

            migrationBuilder.CreateIndex(
                name: "IX_TopicMessages_UserId",
                table: "TopicMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_SubsectionId",
                table: "Topics",
                column: "SubsectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Topics_UserId",
                table: "Topics",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FindComments_FindComments_ParentCommentId",
                table: "FindComments",
                column: "ParentCommentId",
                principalTable: "FindComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserMessages_ParentMessageId",
                table: "UserMessages",
                column: "ParentMessageId",
                principalTable: "UserMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FindComments_FindComments_ParentCommentId",
                table: "FindComments");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserMessages_ParentMessageId",
                table: "UserMessages");

            migrationBuilder.DropTable(
                name: "TopicFavourites");

            migrationBuilder.DropTable(
                name: "TopicFiles");

            migrationBuilder.DropTable(
                name: "TopicMessages");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.AddForeignKey(
                name: "FK_FindComments_FindComments_ParentCommentId",
                table: "FindComments",
                column: "ParentCommentId",
                principalTable: "FindComments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserMessages_ParentMessageId",
                table: "UserMessages",
                column: "ParentMessageId",
                principalTable: "UserMessages",
                principalColumn: "Id");
        }
    }
}
