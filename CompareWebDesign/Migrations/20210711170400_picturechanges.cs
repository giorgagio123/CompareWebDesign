using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CompareWebDesign.Migrations
{
    public partial class picturechanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_ProjectItem_ProjectItemId",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectItemId",
                table: "Pictures",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLiveImage",
                table: "Pictures",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "PictureSrc",
                table: "Pictures",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LiveComment = table.Column<string>(nullable: true),
                    SelectionId = table.Column<int>(nullable: false),
                    ProjectItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_ProjectItem_ProjectItemId",
                        column: x => x.ProjectItemId,
                        principalTable: "ProjectItem",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Comments_ProjectItemId",
                table: "Comments",
                column: "ProjectItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_ProjectItem_ProjectItemId",
                table: "Pictures",
                column: "ProjectItemId",
                principalTable: "ProjectItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_ProjectItem_ProjectItemId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropColumn(
                name: "IsLiveImage",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "PictureSrc",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "ProjectItemId",
                table: "Pictures",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_ProjectItem_ProjectItemId",
                table: "Pictures",
                column: "ProjectItemId",
                principalTable: "ProjectItem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
