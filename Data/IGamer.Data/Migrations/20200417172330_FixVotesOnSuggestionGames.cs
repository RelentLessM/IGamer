using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IGamer.Data.Migrations
{
    public partial class FixVotesOnSuggestionGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Votes",
                table: "SuggestionGame");

            migrationBuilder.CreateTable(
                name: "VoteOnSuggestionGame",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    SuggestionGameId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoteOnSuggestionGame", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VoteOnSuggestionGame_SuggestionGame_SuggestionGameId",
                        column: x => x.SuggestionGameId,
                        principalTable: "SuggestionGame",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_VoteOnSuggestionGame_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VoteOnSuggestionGame_SuggestionGameId",
                table: "VoteOnSuggestionGame",
                column: "SuggestionGameId");

            migrationBuilder.CreateIndex(
                name: "IX_VoteOnSuggestionGame_UserId",
                table: "VoteOnSuggestionGame",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VoteOnSuggestionGame");

            migrationBuilder.AddColumn<int>(
                name: "Votes",
                table: "SuggestionGame",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
