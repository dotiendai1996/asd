using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_DynamicValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicField",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    ModuleId = table.Column<int>(nullable: false),
                    TableName = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    TypeField = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: true),
                    NameDescription = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true),
                    Order = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicField", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DynamicValue",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    CreatorUserId = table.Column<long>(nullable: true),
                    LastModificationTime = table.Column<DateTime>(nullable: true),
                    LastModifierUserId = table.Column<long>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    DeleterUserId = table.Column<long>(nullable: true),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    TenantId = table.Column<int>(nullable: true),
                    DynamicFieldId = table.Column<int>(nullable: false),
                    ObjectId = table.Column<int>(nullable: true),
                    Key = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicValue", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DynamicField_TenantId",
                table: "DynamicField",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_DynamicValue_TenantId",
                table: "DynamicValue",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicField");

            migrationBuilder.DropTable(
                name: "DynamicValue");
        }
    }
}
