using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Create_table_Documents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Documentses",
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
                    Number = table.Column<int>(nullable: false),
                    TypeDocID = table.Column<int>(nullable: false),
                    DateIssue = table.Column<string>(nullable: true),
                    PlaceRecevie = table.Column<string>(nullable: true),
                    SaveTo = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    ApprovedBy = table.Column<string>(nullable: true),
                    Attachment = table.Column<string>(nullable: true),
                    TypeRecevie = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentses_TenantId",
                table: "Documentses",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Documentses");
        }
    }
}
