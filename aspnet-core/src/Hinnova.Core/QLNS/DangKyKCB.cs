using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("DangKyKCB")]
    public class DangKyKCB : Entity 
    {

		public virtual string TenNoiKCB { get; set; }
		
		public virtual string MaNoiKCB { get; set; }
		
		public virtual string DiaChi { get; set; }
		
		public virtual int? TinhThanhID { get; set; }
		
		public virtual string GhiChu { get; set; }
		

    }
}