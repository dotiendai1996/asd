using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("vanbans")]
    public class Vanban : Entity 
    {

		public virtual string TenCongViec { get; set; }
		
		public virtual DateTime NgayGiaoViec { get; set; }
		
		public virtual DateTime HanKetThuc { get; set; }
		
		public virtual string NguoiXuLy { get; set; }
		
		public virtual int TienDoChung { get; set; }
		
		public virtual string TinhTrang { get; set; }
		
		public virtual string NoiDung { get; set; }
		

    }
}