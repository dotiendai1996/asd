using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Add_More_Field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_WorkHandlings",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_WorkHandlings",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_WorkDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_WorkDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_WorkAssigns",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "CA_TypeHandles",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_TypeHandles",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_Schedules",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_Schedules",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_ReceiveUnits",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_Promulgateds",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_DocumentTypes",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_DocumentHandlingDetails",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_DocumentHandlingDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_DocumentHandling",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_DocumentHandling",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "CA_DocumentDetails",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "CA_Document",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_WorkHandlings");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_WorkHandlings");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_WorkDetails");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_WorkDetails");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_WorkAssigns");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "CA_TypeHandles");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_TypeHandles");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_Schedules");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_Schedules");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_ReceiveUnits");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_Promulgateds");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_DocumentTypes");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_DocumentHandlingDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_DocumentHandling");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "CA_DocumentDetails");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "CA_Document");
        }
    }
}
