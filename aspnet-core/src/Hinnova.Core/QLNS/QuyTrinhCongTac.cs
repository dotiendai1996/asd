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
		
		public virtual int? DonViCongTacID { get; set; }
		
		public virtual string QuanLyTrucTiep { get; set; }
		
		public virtual string TrangThaiCode { get; set; }
		
		public virtual string GhiChu { get; set; }
        public virtual int MaHoSo { get; set; }


	}
}