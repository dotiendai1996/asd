using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("QuanLyNghiPheps")]
    public class QuanLyNghiPhep : AuditedEntity 
    {

		public virtual string TenNhanVien { get; set; }
		
		public virtual int? DonViCongTacID { get; set; }
		
		public virtual string MaNV { get; set; }
		
		public virtual bool NghiPhep { get; set; }

        public virtual bool TangCa { get; set; }

		public virtual bool CongTac { get; set; }
		
		public virtual DateTime NgayBatDau { get; set; }
		
		public virtual DateTime NgayKetThuc { get; set; }
		
		public virtual string LyDo { get; set; }

       // public virtual int[] QuanLyTrucTiepID { get; set; }
//	public virtual int? QuanLyTrucTiepID { get; set; }

    public virtual string QuanLyTrucTiepID { get; set; }

		public virtual string TruongBoPhanID { get; set; }
		
		public virtual string TepDinhKem { get; set; }
		
		public virtual string TenCTY { get; set; }
		
		public virtual int? NguoiDuyetID { get; set; }
		
		public virtual DateTime NgayDuyet { get; set; }
		
		

		public virtual int? TrangThaiID { get; set; }
	}
}