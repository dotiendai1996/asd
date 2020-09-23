using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("HopDong")]
    public class HopDong : FullAuditedEntity 
    {

		public virtual string NhanVienId { get; set; }
		
		public virtual string HoTenNhanVien { get; set; }
		
		public virtual string ViTriCongViecCode { get; set; }
		
		public virtual string SoHopDong { get; set; }
		
		public virtual DateTime? NgayKy { get; set; }
		
		public virtual int? DonViCongTacID { get; set; }
		
		public virtual string TenHopDong { get; set; }
		
		public virtual string LoaiHopDongCode { get; set; }
		
		public virtual string HinhThucLamViecCode { get; set; }
		
		public virtual DateTime? NgayCoHieuLuc { get; set; }
		
		public virtual DateTime? NgayHetHan { get; set; }
		
		public virtual double? LuongCoBan { get; set; }
		
		public virtual double? LuongDongBaoHiem { get; set; }
		
		public virtual double? TyLeHuongLuong { get; set; }
		
		public virtual string NguoiDaiDienCongTy { get; set; }
		
		public virtual string ChucDanh { get; set; }
		
		public virtual string TrichYeu { get; set; }
		
		public virtual string TepDinhKem { get; set; }
		
		public virtual string GhiChu { get; set; }
		
		public virtual string RECORD_STATUS { get; set; }
		
		public virtual int? MARKER_ID { get; set; }
		
		public virtual string AUTH_STATUS { get; set; }
		
		public virtual int? CHECKER_ID { get; set; }
		
		public virtual DateTime? APPROVE_DT { get; set; }
		
		public virtual string ThoiHanHopDong { get; set; }

        //public virtual string DonViCongTac { get; set; }

        public virtual string LinkTemplate { get; set; }

        public virtual int? LoaiHopDongID { get; set; }

        public virtual string tenCTY { get; set; }


	}
}