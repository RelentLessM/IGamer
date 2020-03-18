using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IGamer.Data.Migrations
{
    public partial class AddVoteEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentOnBlogs");

            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Guides");

            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "CommentOnGuides");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "CommentOnGuides");

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Title = table.Column<string>(maxLength: 20, nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Category = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Posts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VotesOnGuideComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CommentId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    VoteType = table.Column<int>(nullable: false)
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
                        name: "FK_VotesOnGuideComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VotesOnGuides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    GuideId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    VoteType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotesOnGuides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotesOnGuides_Guides_GuideId",
                        column: x => x.GuideId,
                        principalTable: "Guides",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotesOnGuides_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentOnPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeletedOn = table.Column<DateTime>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    PostId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentOnPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentOnPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentOnPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VotesOnPosts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    PostId = table.Column<string>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    VoteType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotesOnPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotesOnPosts_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotesOnPosts_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "VotesOnPostComments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    CommentId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    VoteType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VotesOnPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VotesOnPostComments_CommentOnPosts_CommentId",
                        column: x => x.CommentId,
                        principalTable: "CommentOnPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VotesOnPostComments_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnPosts_IsDeleted",
                table: "CommentOnPosts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnPosts_PostId",
                table: "CommentOnPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnPosts_UserId",
                table: "CommentOnPosts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_IsDeleted",
                table: "Posts",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_UserId",
                table: "Posts",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuideComments_CommentId",
                table: "VotesOnGuideComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuideComments_UserId",
                table: "VotesOnGuideComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuides_GuideId",
                table: "VotesOnGuides",
                column: "GuideId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnGuides_UserId",
                table: "VotesOnGuides",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnPostComments_CommentId",
                table: "VotesOnPostComments",
                column: "CommentId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnPostComments_UserId",
                table: "VotesOnPostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnPosts_PostId",
                table: "VotesOnPosts",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_VotesOnPosts_UserId",
                table: "VotesOnPosts",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VotesOnGuideComments");

            migrationBuilder.DropTable(
                name: "VotesOnGuides");

            migrationBuilder.DropTable(
                name: "VotesOnPostComments");

            migrationBuilder.DropTable(
                name: "VotesOnPosts");

            migrationBuilder.DropTable(
                name: "CommentOnPosts");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "CommentOnGuides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "CommentOnGuides",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Blogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CommentOnBlogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlogId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dislikes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Likes = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentOnBlogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentOnBlogs_Blogs_BlogId",
                        column: x => x.BlogId,
                        principalTable: "Blogs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CommentOnBlogs_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_IsDeleted",
                table: "Blogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_Blogs_UserId",
                table: "Blogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnBlogs_BlogId",
                table: "CommentOnBlogs",
                column: "BlogId");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnBlogs_IsDeleted",
                table: "CommentOnBlogs",
                column: "IsDeleted");

            migrationBuilder.CreateIndex(
                name: "IX_CommentOnBlogs_UserId",
                table: "CommentOnBlogs",
                column: "UserId");
        }
    }
}
