using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IGamer.Data.Migrations
{
    public partial class AddReplyEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReplyOnPostCommentId",
                table: "VotesOnPostComments",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReplyOnGuideCommentId",
                table: "VotesOnGuideComments",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReplyOnGuideComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CommentId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyOnGuideComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyOnGuideComments_CommentOnGuides_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentOnGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyOnGuideComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReplyOnPostComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    CommentId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyOnPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyOnPostComments_CommentOnPosts_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentOnPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyOnPostComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnPostComments_ReplyOnPostCommentId",
                table: "VotesOnPostComments",
                column: "ReplyOnPostCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuideComments_ReplyOnGuideCommentId",
                table: "VotesOnGuideComments",
                column: "ReplyOnGuideCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyOnGuideComments_CommentId",
                table: "ReplyOnGuideComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyOnGuideComments_IsDeleted",
                table: "ReplyOnGuideComments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyOnGuideComments_UserId",
                table: "ReplyOnGuideComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyOnPostComments_CommentId",
                table: "ReplyOnPostComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyOnPostComments_IsDeleted",
                table: "ReplyOnPostComments",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyOnPostComments_UserId",
                table: "ReplyOnPostComments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_VotesOnGuideComments_ReplyOnGuideComments_ReplyOnGuideCommentId",
                table: "VotesOnGuideComments",
                column: "ReplyOnGuideCommentId",
                principalTable: "ReplyOnGuideComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VotesOnPostComments_ReplyOnPostComments_ReplyOnPostCommentId",
                table: "VotesOnPostComments",
                column: "ReplyOnPostCommentId",
                principalTable: "ReplyOnPostComments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VotesOnGuideComments_ReplyOnGuideComments_ReplyOnGuideCommentId",
                table: "VotesOnGuideComments");

            migrationBuilder.DropForeignKey(
                name: "FK_VotesOnPostComments_ReplyOnPostComments_ReplyOnPostCommentId",
                table: "VotesOnPostComments");

            migrationBuilder.DropTable(
                name: "ReplyOnGuideComments");

            migrationBuilder.DropTable(
                name: "ReplyOnPostComments");

            migrationBuilder.DropIndex(
                name: "IX_VotesOnPostComments_ReplyOnPostCommentId",
                table: "VotesOnPostComments");

            migrationBuilder.DropIndex(
                name: "IX_VotesOnGuideComments_ReplyOnGuideCommentId",
                table: "VotesOnGuideComments");

            migrationBuilder.DropColumn(
                name: "ReplyOnPostCommentId",
                table: "VotesOnPostComments");

            migrationBuilder.DropColumn(
                name: "ReplyOnGuideCommentId",
                table: "VotesOnGuideComments");
        }
    }
}
