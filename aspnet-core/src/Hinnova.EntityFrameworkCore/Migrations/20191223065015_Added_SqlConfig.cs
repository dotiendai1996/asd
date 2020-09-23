using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_SqlConfig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SqlConfig",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    IsRawQuery = table.Column<bool>(nullable: false),
                    SqlContent = table.Column<string>(nullable: true),
                    GroupLevel = table.Column<int>(nullable: true),
                    DisplayType = table.Column<int>(nullable: true),
                    Version = table.Column<int>(nullable: true),
                    IsDynamicColumn = table.Column<bool>(nullable: false),
                    TypeGetColumn = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SqlConfig", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SqlConfig_TenantId",
                table: "SqlConfig",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SqlConfig");
        }
    }
}
