using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("QuanLyTrucTiepPNPs")]
    public class QuanLyTrucTiepPNP : AuditedEntity 
    {

		public virtual int PhieuNghiPhepID { get; set; }
		
		public virtual string QLTT { get; set; }
		
		public virtual string GhiChu { get; set; }
		
		public virtual string MaHoSo { get; set; }
		

    }
}