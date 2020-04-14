using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IGamer.Data.Migrations
{
    public partial class AddReportsAndRemoveGuideComments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotesOnGuideComments");

            migrationBuilder.DropTable(
                name: "ReplyOnGuideComments");

            migrationBuilder.DropTable(
                name: "CommentOnGuides");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    Reason = table.Column<string>(nullable: false),
                    GuideId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reports_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_GuideId",
                table: "Reports",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_UserId",
                table: "Reports",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.CreateTable(
                name: "CommentOnGuides",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuideId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentOnGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentOnGuides_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentOnGuides_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ReplyOnGuideComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                name: "VotesOnGuideComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CommentId = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReplyOnGuideCommentId = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotesOnGuideComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotesOnGuideComments_CommentOnGuides_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentOnGuides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotesOnGuideComments_ReplyOnGuideComments_ReplyOnGuideCommentId",
                        column: x => x.ReplyOnGuideCommentId,
                        principalTable: "ReplyOnGuideComments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotesOnGuideComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnGuides_GuideId",
                table: "CommentOnGuides",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnGuides_IsDeleted",
                table: "CommentOnGuides",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnGuides_UserId",
                table: "CommentOnGuides",
                column: "UserId");

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
                name: "IX_VotesOnGuideComments_CommentId",
                table: "VotesOnGuideComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuideComments_ReplyOnGuideCommentId",
                table: "VotesOnGuideComments",
                column: "ReplyOnGuideCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuideComments_UserId",
                table: "VotesOnGuideComments",
                column: "UserId");
        }
    }
}
