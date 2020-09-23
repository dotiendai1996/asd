using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_DocumentHandling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CA_TypeHandes");

            migrationBuilder.DropTable(
                name: "CA_WordProcessings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_Documents",
                table: "CA_Documents");

            migrationBuilder.DropColumn(
                name: "Superios",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "DateHundle",
                table: "CA_DocumentDetails");

            migrationBuilder.DropColumn(
                name: "TypeHundle",
                table: "CA_DocumentDetails");

            migrationBuilder.RenameTable(
                name: "CA_Documents",
                newName: "CA_Document");

            migrationBuilder.RenameIndex(
                name: "IX_CA_Documents_TenantId",
                table: "CA_Document",
                newName: "IX_CA_Document_TenantId");

            migrationBuilder.AddColumn<string>(
                name: "Superior",
                table: "CA_DocumentHandlingDetails",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateHandle",
                table: "CA_DocumentDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TypeHandle",
                table: "CA_DocumentDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "CA_Document",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IncommingNumber",
                table: "CA_Document",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "MoreInformation",
                table: "CA_Document",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_Document",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Pages",
                table: "CA_Document",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_Document",
                table: "CA_Document",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CA_DocumentHandling",
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
                    DocumentId = table.Column<int>(nullable: false),
                    Handler = table.Column<string>(nullable: true),
                    HandlingDetailId = table.Column<int>(nullable: true),
                    PlaceReceive = table.Column<string>(nullable: true),
                    KeywordId = table.Column<int>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_DocumentHandling", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CA_TypeHandles",
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
                    Name = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_TypeHandles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CA_WorkHandlings",
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
                    ReceivePlace = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    Comment = table.Column<string>(nullable: true),
                    KeyWordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_WorkHandlings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CA_DocumentHandling_TenantId",
                table: "CA_DocumentHandling",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_TypeHandles_TenantId",
                table: "CA_TypeHandles",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_WorkHandlings_TenantId",
                table: "CA_WorkHandlings",
                column: "TenantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CA_DocumentHandling");

            migrationBuilder.DropTable(
                name: "CA_TypeHandles");

            migrationBuilder.DropTable(
                name: "CA_WorkHandlings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CA_Document",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "Superior",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "DateHandle",
                table: "CA_DocumentDetails");

            migrationBuilder.DropColumn(
                name: "TypeHandle",
                table: "CA_DocumentDetails");

            migrationBuilder.DropColumn(
                name: "Author",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "IncommingNumber",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "MoreInformation",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "Pages",
                table: "CA_Document");

            migrationBuilder.RenameTable(
                name: "CA_Document",
                newName: "CA_Documents");

            migrationBuilder.RenameIndex(
                name: "IX_CA_Document_TenantId",
                table: "CA_Documents",
                newName: "IX_CA_Documents_TenantId");

            migrationBuilder.AddColumn<string>(
                name: "Superios",
                table: "CA_DocumentHandlingDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateHundle",
                table: "CA_DocumentDetails",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "TypeHundle",
                table: "CA_DocumentDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CA_Documents",
                table: "CA_Documents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "CA_TypeHandes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_TypeHandes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CA_WordProcessings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeleterUserId = table.Column<long>(type: "bigint", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    KeyWordId = table.Column<int>(type: "int", nullable: false),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierUserId = table.Column<long>(type: "bigint", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReceivePlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CA_WordProcessings", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CA_TypeHandes_TenantId",
                table: "CA_TypeHandes",
                column: "TenantId");

            migrationBuilder.CreateIndex(
                name: "IX_CA_WordProcessings_TenantId",
                table: "CA_WordProcessings",
                column: "TenantId");
        }
    }
}
