using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("NoiDaoTao")]
    public class NoiDaoTao : Entity 
    {

		public virtual string TenNoiDaoTao { get; set; }
		
		public virtual string MaNoiDaoTao { get; set; }
		
		public virtual string DiaChi { get; set; }
		
		public virtual string KhuVuc { get; set; }
		

    }
}