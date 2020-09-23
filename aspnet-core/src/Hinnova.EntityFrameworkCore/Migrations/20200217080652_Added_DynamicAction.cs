using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_DynamicAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicAction",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenantId = table.Column<int>(nullable: true),
                    LabelId = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    HasSave = table.Column<bool>(nullable: false),
                    HasReturn = table.Column<bool>(nullable: false),
                    HasTransfer = table.Column<bool>(nullable: false),
                    HasSaveAndTransfer = table.Column<bool>(nullable: false),
                    HasFinish = table.Column<bool>(nullable: false),
                    IsTopPosition = table.Column<bool>(nullable: false),
                    IsBack = table.Column<bool>(nullable: false),
                    HasAssignWork = table.Column<bool>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicAction", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicAction_TenantId",
                table: "DynamicAction",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicAction");
        }
    }
}
