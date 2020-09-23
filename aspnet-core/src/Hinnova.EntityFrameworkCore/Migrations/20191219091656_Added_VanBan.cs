using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hinnova.Migrations
{
    public partial class Added_VanBan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VanBans",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TenCongViec = table.Column<string>(nullable: true),
                    NgayGiaoViec = table.Column<DateTime>(nullable: false),
                    HanKetThuc = table.Column<DateTime>(nullable: false),
                    NguoiXuLy = table.Column<string>(nullable: true),
                    TienDoChung = table.Column<int>(nullable: false),
                    TinhTrang = table.Column<string>(nullable: true),
                    NoiDung = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VanBans", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VanBans");
        }
    }
}
