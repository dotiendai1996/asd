using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("TextBooks")]
    public class TextBook : FullAuditedEntity  /* , IMayHaveTenant*/
	{
		//public int? TenantId { get; set; }


		public virtual int SoDen { get; set; }
		
		public virtual DateTime NgayDen { get; set; }
		
		public virtual string SoHieuGoc { get; set; }
		
		public virtual string CoQuanBanHanh { get; set; }
		
		public virtual string TrichYeu { get; set; }
		
		public virtual string NguoiChiDao { get; set; }
		
		public virtual string Nguoi_DonVi { get; set; }
		
		public virtual string FileDinhKem { get; set; }
		

    }
}