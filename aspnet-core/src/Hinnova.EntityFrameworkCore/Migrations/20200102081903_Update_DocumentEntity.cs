using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Update_DocumentEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateIssue",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "TypeDocID",
                table: "CA_Document");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "HistoryUploads",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "CA_Document",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DocumentTypeId",
                table: "CA_Document",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "GroupAuthor",
                table: "CA_Document",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Range",
                table: "CA_Document",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Version",
                table: "HistoryUploads");

            migrationBuilder.DropColumn(
                name: "DocumentTypeId",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "GroupAuthor",
                table: "CA_Document");

            migrationBuilder.DropColumn(
                name: "Range",
                table: "CA_Document");

            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "CA_Document",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DateIssue",
                table: "CA_Document",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TypeDocID",
                table: "CA_Document",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
