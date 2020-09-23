using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_SYS_CODEMASTERS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYS_CODEMASTERS",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Prefix = table.Column<string>(nullable: false),
                    CurValue = table.Column<decimal>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Active = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYS_CODEMASTERS", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYS_CODEMASTERS");
        }
    }
}
