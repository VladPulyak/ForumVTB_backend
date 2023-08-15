using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class AddAdvertIdToUserChatAndOnDeleteCascade : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertComments_AspNetUsers_UserId",
                table: "AdvertComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_AspNetUsers_UserId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Subsections_SubsectionId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_UserMessages_MessageId",
                table: "MessageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_AspNetUsers_ReceiverId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_AspNetUsers_SenderId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkComments_AspNetUsers_UserId",
                table: "WorkComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_UserId",
                table: "Works");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Subsections_SubsectionId",
                table: "Works");

            migrationBuilder.AddColumn<string>(
                name: "AdvertId",
                table: "UserChats",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_AdvertId",
                table: "UserChats",
                column: "AdvertId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertComments_AspNetUsers_UserId",
                table: "AdvertComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_AspNetUsers_UserId",
                table: "Adverts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Subsections_SubsectionId",
                table: "Adverts",
                column: "SubsectionId",
                principalTable: "Subsections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_UserMessages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "UserMessages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Adverts_AdvertId",
                table: "UserChats",
                column: "AdvertId",
                principalTable: "Adverts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_AspNetUsers_ReceiverId",
                table: "UserMessages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_AspNetUsers_SenderId",
                table: "UserMessages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages",
                column: "ChatId",
                principalTable: "UserChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_WorkComments_AspNetUsers_UserId",
                table: "WorkComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_UserId",
                table: "Works",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Subsections_SubsectionId",
                table: "Works",
                column: "SubsectionId",
                principalTable: "Subsections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdvertComments_AspNetUsers_UserId",
                table: "AdvertComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_AspNetUsers_UserId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_Adverts_Subsections_SubsectionId",
                table: "Adverts");

            migrationBuilder.DropForeignKey(
                name: "FK_MessageFiles_UserMessages_MessageId",
                table: "MessageFiles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Adverts_AdvertId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_AspNetUsers_ReceiverId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_AspNetUsers_SenderId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_WorkComments_AspNetUsers_UserId",
                table: "WorkComments");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_AspNetUsers_UserId",
                table: "Works");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Subsections_SubsectionId",
                table: "Works");

            migrationBuilder.DropIndex(
                name: "IX_UserChats_AdvertId",
                table: "UserChats");

            migrationBuilder.DropColumn(
                name: "AdvertId",
                table: "UserChats");

            migrationBuilder.AddForeignKey(
                name: "FK_AdvertComments_AspNetUsers_UserId",
                table: "AdvertComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_AspNetUsers_UserId",
                table: "Adverts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Adverts_Subsections_SubsectionId",
                table: "Adverts",
                column: "SubsectionId",
                principalTable: "Subsections",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MessageFiles_UserMessages_MessageId",
                table: "MessageFiles",
                column: "MessageId",
                principalTable: "UserMessages",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_AspNetUsers_ReceiverId",
                table: "UserMessages",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_AspNetUsers_SenderId",
                table: "UserMessages",
                column: "SenderId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_UserChats_ChatId",
                table: "UserMessages",
                column: "ChatId",
                principalTable: "UserChats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkComments_AspNetUsers_UserId",
                table: "WorkComments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_AspNetUsers_UserId",
                table: "Works",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Subsections_SubsectionId",
                table: "Works",
                column: "SubsectionId",
                principalTable: "Subsections",
                principalColumn: "Id");
        }
    }
}
