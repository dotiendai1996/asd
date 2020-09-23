using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_Label : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_HistoryUploads_TenantId",
                table: "HistoryUploads");

            migrationBuilder.DropColumn(
                name: "IsDelimiter",
                table: "CA_Menus");

            migrationBuilder.DropColumn(
                name: "IsParent",
                table: "CA_Menus");

            migrationBuilder.AddColumn<long>(
                name: "CreatorUserId",
                table: "CA_Menus",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "DeleterUserId",
                table: "CA_Menus",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LastModifierUserId",
                table: "CA_Menus",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Label",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Icon = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    Parent = table.Column<int>(nullable: true),
                    Index = table.Column<int>(nullable: false),
                    RequiredPermissionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Label", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Label");

            migrationBuilder.DropColumn(
                name: "CreatorUserId",
                table: "CA_Menus");

            migrationBuilder.DropColumn(
                name: "DeleterUserId",
                table: "CA_Menus");

            migrationBuilder.DropColumn(
                name: "LastModifierUserId",
                table: "CA_Menus");

            migrationBuilder.AddColumn<bool>(
                name: "IsDelimiter",
                table: "CA_Menus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsParent",
                table: "CA_Menus",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_HistoryUploads_TenantId",
                table: "HistoryUploads",
                column: "TenantId");
        }
    }
}
