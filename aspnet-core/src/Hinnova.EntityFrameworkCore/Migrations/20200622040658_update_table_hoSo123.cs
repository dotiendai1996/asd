using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class update_table_hoSo123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "LuongCoBan",
            //    table: "HoSo");

            migrationBuilder.AlterColumn<int>(
                name: "LoaiHopDongID",
                table: "HoSo",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "LoaiHopDongID",
                table: "HoSo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldNullable: true);

        //    migrationBuilder.AddColumn<double>(
        //        name: "LuongCoBan",
        //        table: "HoSo",
        //        type: "float",
        //        nullable: true);
        }
    }
}
