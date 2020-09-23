using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Regen_label_AddIsActiveField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Label",
                nullable: false,
                defaultValue: false);

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Label");
        }
    }
}
