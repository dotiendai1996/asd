
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditUngVienDto : EntityDto<int?>
    {

        public string MaUngVien { get; set; }

        public string HoVaTen { get; set; }

        public int? DonViCongTacID { get; set; }

        public string ViTriUngTuyenCode { get; set; }

        public string KenhTuyenDungCode { get; set; }

        public string GioiTinhCode { get; set; }

        public DateTime? NgaySinh { get; set; }

        public string SoCMND { get; set; }

        public DateTime? NgayCap { get; set; }

        public string NoiCap { get; set; }

        public int? TinhThanhID { get; set; }

        public string TinhTrangHonNhanCode { get; set; }

        public string TrinhDoDaoTaoCode { get; set; }

        public string TrinhDoVanHoa { get; set; }

        public int? NoiDaoTaoID { get; set; }

        public string Khoa { get; set; }

        public string ChuyenNganh { get; set; }

        public string XepLoaiCode { get; set; }

        public int? NamTotNghiep { get; set; }

        public string TrangThaiCode { get; set; }

        public string TienDoTuyenDungCode { get; set; }

        public string TepDinhKem { get; set; }

        public string RECORD_STATUS { get; set; }

        public int? MARKER_ID { get; set; }

        public string AUTH_STATUS { get; set; }

        public int? CHECKER_ID { get; set; }

        public DateTime? APPROVE_DT { get; set; }

        public string DienThoai { get; set; }

        public string Email { get; set; }

        public string DiaChi { get; set; }

        public string Time1 { get; set; }
        public DateTime? Day1 { get; set; }
        public string Time2 { get; set; }
        public DateTime? Day2 { get; set; }
        public DateTime? LastModificationTime { get; set; }

        public string Time3 { get; set; }
        public DateTime? Day3 { get; set; }

        public string Note { get; set; }

        public string TenCTY { get; set; }

        public string NVPhuTrach { get; set; }

        public  int? MaNguoiPheDuyet { get; set; }

        public DateTime? NgayPheDuyet { get; set; }

        public string DungLuong { get; set; }

        public string TieuDe { get; set; }
	}
}