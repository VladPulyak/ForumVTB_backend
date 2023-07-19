using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTopicAndTopicMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_TopicMessages_MessageId",
                table: "MessageFiles");

            migrationBuilder.DropTable(
                name: "TopicMessages");

            migrationBuilder.DropTable(
                name: "Topics");

            migrationBuilder.CreateTable(
                name: "Adverts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    SubsectionId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adverts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Adverts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Adverts_Subsections_SubsectionId",
                        column: x => x.SubsectionId,
                        principalTable: "Subsections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AdvertMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    AdvertId = table.Column<int>(type: "integer", nullable: false),
                    ParentCommentId = table.Column<string>(type: "text", nullable: true),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    IsReply = table.Column<bool>(type: "boolean", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvertMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvertMessages_AdvertMessages_ParentCommentId",
                        column: x => x.ParentCommentId,
                        principalTable: "AdvertMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AdvertMessages_Adverts_AdvertId",
                        column: x => x.AdvertId,
                        principalTable: "Adverts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AdvertMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvertMessages_AdvertId",
                table: "AdvertMessages",
                column: "AdvertId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertMessages_ParentCommentId",
                table: "AdvertMessages",
                column: "ParentCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvertMessages_UserId",
                table: "AdvertMessages",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_SubsectionId",
                table: "Adverts",
                column: "SubsectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Adverts_UserId",
                table: "Adverts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_AdvertMessages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "AdvertMessages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_AdvertMessages_MessageId",
                table: "MessageFiles");

            migrationBuilder.DropTable(
                name: "AdvertMessages");

            migrationBuilder.DropTable(
                name: "Adverts");

            migrationBuilder.CreateTable(
                name: "Topics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubsectionId = table.Column<int>(type: "integer", nullable: true),
                    Name = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Topics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Topics_Subsections_SubsectionId",
                        column: x => x.SubsectionId,
                        principalTable: "Subsections",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TopicMessages",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ParentMessageId = table.Column<string>(type: "text", nullable: true),
                    TopicId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp", nullable: false),
                    IsReply = table.Column<bool>(type: "boolean", nullable: false),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopicMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopicMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TopicMessages_TopicMessages_ParentMessageId",
                        column: x => x.ParentMessageId,
                        principalTable: "TopicMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_TopicMessages_Topics_TopicId",
                        column: x => x.TopicId,
                        principalTable: "Topics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Topics",
                columns: new[] { "Id", "Name", "SubsectionId" },
                values: new object[,]
                {
                    { 1, "Transport", null },
                    { 2, "Electronics", null },
                    { 3, "Sports and relax", null },
                    { 4, "Animals", null },
                    { 5, "Read estate", null },
                    { 6, "Clothes", null },
                    { 7, "Home and garden", null }
                });

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

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_TopicMessages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "TopicMessages",
                principalColumn: "Id");
        }
    }
}
