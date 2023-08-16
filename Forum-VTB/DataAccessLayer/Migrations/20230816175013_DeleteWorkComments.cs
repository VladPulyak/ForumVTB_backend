using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DeleteWorkComments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkComments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkComments",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    ParentCommentId = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    WorkId = table.Column<string>(type: "text", nullable: true),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
        }
    }
}
