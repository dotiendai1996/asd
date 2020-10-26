using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("QuyTrinhCongTacs")]
    public class QuyTrinhCongTac : FullAuditedEntity 
    {

		public virtual string TenCty { get; set; }
		
		public virtual DateTime DateTo { get; set; }
		
		public virtual DateTime DateFrom { get; set; }
		
		public virtual string ViTriCongViecCode { get; set; }

        public virtual string QuanLyTrucTiep { get; set; }

		public virtual int? DonViCongTacID { get; set; }
        
		public virtual string TrangThaiCode { get; set; }
		
		public virtual string GhiChu { get; set; }
        public virtual string LyDo { get; set; }

		public virtual int MaHoSo { get; set; }
			   
        public virtual DateTime NgayDuyet { get; set; }
			   
        public virtual string NguoiDuyetId { get; set; }

        public virtual string TruongBoPhanId { get; set; }
			  
        public virtual string GiamDocBoPhanId { get; set; }
			  
        public virtual string PhongCTNSId { get; set; }
			  
        public virtual string GiamDocId { get; set; }
			 
        public virtual string Status { get; set; }


	}
}