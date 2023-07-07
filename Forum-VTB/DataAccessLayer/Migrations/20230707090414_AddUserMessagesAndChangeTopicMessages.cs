using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddUserMessagesAndChangeTopicMessages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_Messages_MessageId",
                table: "MessageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Topics_TopicId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "TopicMessages");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_UserId",
                table: "TopicMessages",
                newName: "IX_TopicMessages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_TopicId",
                table: "TopicMessages",
                newName: "IX_TopicMessages_TopicId");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfCreation",
                table: "TopicMessages",
                type: "timestamp",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsReply",
                table: "TopicMessages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ParentMessageId",
                table: "TopicMessages",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TopicMessages",
                table: "TopicMessages",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<string>(type: "text", nullable: true),
                    ReceiverId = table.Column<string>(type: "text", nullable: true),
                    ParentMessageId = table.Column<int>(type: "integer", nullable: false),
                    Text = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_SenderId",
                        column: x => x.SenderId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserMessages_UserMessages_ParentMessageId",
                        column: x => x.ParentMessageId,
                        principalTable: "UserMessages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopicMessages_ParentMessageId",
                table: "TopicMessages",
                column: "ParentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ParentMessageId",
                table: "UserMessages",
                column: "ParentMessageId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_ReceiverId",
                table: "UserMessages",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_SenderId",
                table: "UserMessages",
                column: "SenderId");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_TopicMessages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "TopicMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TopicMessages_AspNetUsers_UserId",
                table: "TopicMessages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicMessages_TopicMessages_ParentMessageId",
                table: "TopicMessages",
                column: "ParentMessageId",
                principalTable: "TopicMessages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TopicMessages_Topics_TopicId",
                table: "TopicMessages",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_TopicMessages_MessageId",
                table: "MessageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicMessages_AspNetUsers_UserId",
                table: "TopicMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicMessages_TopicMessages_ParentMessageId",
                table: "TopicMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_TopicMessages_Topics_TopicId",
                table: "TopicMessages");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TopicMessages",
                table: "TopicMessages");

            migrationBuilder.DropIndex(
                name: "IX_TopicMessages_ParentMessageId",
                table: "TopicMessages");

            migrationBuilder.DropColumn(
                name: "DateOfCreation",
                table: "TopicMessages");

            migrationBuilder.DropColumn(
                name: "IsReply",
                table: "TopicMessages");

            migrationBuilder.DropColumn(
                name: "ParentMessageId",
                table: "TopicMessages");

            migrationBuilder.RenameTable(
                name: "TopicMessages",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_TopicMessages_UserId",
                table: "Messages",
                newName: "IX_Messages_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TopicMessages_TopicId",
                table: "Messages",
                newName: "IX_Messages_TopicId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_Messages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "Messages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_AspNetUsers_UserId",
                table: "Messages",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Topics_TopicId",
                table: "Messages",
                column: "TopicId",
                principalTable: "Topics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
