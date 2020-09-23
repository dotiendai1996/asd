using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("UngVien")]
    public class UngVien : FullAuditedEntity 
    {

	public virtual string MaUngVien { get; set; }
	
	public virtual string HoVaTen { get; set; }

    public virtual int? DonViCongTacID { get; set; }

    public virtual string ViTriUngTuyenCode { get; set; }
	
	public virtual string KenhTuyenDungCode { get; set; }
	
	public virtual string GioiTinhCode { get; set; }
	
	public virtual DateTime? NgaySinh { get; set; }
	
	public virtual string SoCMND { get; set; }
	
	public virtual DateTime? NgayCap { get; set; }
	
	public virtual string NoiCap { get; set; }
	
	public virtual int? TinhThanhID { get; set; }
	
	public virtual string TinhTrangHonNhanCode { get; set; }
	
	public virtual string TrinhDoDaoTaoCode { get; set; }
	
	public virtual string TrinhDoVanHoa { get; set; }
	
	public virtual int? NoiDaoTaoID { get; set; }
	
	public virtual string Khoa { get; set; }
	
	public virtual string ChuyenNganh { get; set; }
	
	public virtual string XepLoaiCode { get; set; }
	
	public virtual int? NamTotNghiep { get; set; }
	
	public virtual string TrangThaiCode { get; set; }
	
	public virtual string TienDoTuyenDungCode { get; set; }
	
	public virtual string TepDinhKem { get; set; }
	
	public virtual string RECORD_STATUS { get; set; }
	
	public virtual int? MARKER_ID { get; set; }
	
	public virtual string AUTH_STATUS { get; set; }
	
	public virtual int? CHECKER_ID { get; set; }
	
	public virtual DateTime? APPROVE_DT { get; set; }
	
	public virtual string DienThoai { get; set; }
	
	public virtual string Email { get; set; }
	
	public virtual string DiaChi { get; set; }

	public virtual string Time1 { get; set; }
	public virtual DateTime? Day1 { get; set; }
	public virtual string Time2 { get; set; }
	public virtual DateTime? Day2 { get; set; }

	public virtual string Time3 { get; set; }
	public virtual DateTime? Day3 { get; set; }

	public virtual string Note { get; set; }

	public virtual string TenCTY { get; set; }

    public virtual string NVPhuTrach{ get; set; }

    public virtual int? MaNguoiPheDuyet { get; set; }

    public virtual DateTime? NgayPheDuyet { get; set; }

    public virtual string DungLuong { get; set; }

    public virtual string TieuDe { get; set; }
	}
}