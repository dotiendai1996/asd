using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_RoleMapper : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RoleMapper",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    RoleId = table.Column<int>(nullable: false),
                    LabelId = table.Column<int>(nullable: false),
                    MenuId = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMapper", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoleMapper_TenantId",
                table: "RoleMapper",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleMapper");
        }
    }
}
