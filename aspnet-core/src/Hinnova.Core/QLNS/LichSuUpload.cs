using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("LichSuUploads")]
    public class LichSuUpload : FullAuditedEntity 
    {

		public virtual string TenFile { get; set; }
		
		public virtual string DungLuong { get; set; }
		
		public virtual string TieuDe { get; set; }
		
		public virtual string Type { get; set; }
		
		public virtual string TypeID { get; set; }
		

    }
}