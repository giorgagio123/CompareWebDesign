using Microsoft.EntityFrameworkCore.Migrations;

namespace CompareWebDesign.Migrations
{
    public partial class cropset : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CropSettings",
                table: "ProjectItem",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CropSettings",
                table: "ProjectItem");
        }
    }
}
