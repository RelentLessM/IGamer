using Microsoft.EntityFrameworkCore.Migrations;

namespace IGamer.Data.Migrations
{
    public partial class GuideModelRemovedCategoryId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Guides");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Guides",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
