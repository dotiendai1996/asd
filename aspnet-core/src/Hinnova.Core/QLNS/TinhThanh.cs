using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("TinhThanh")]
    public class TinhThanh : Entity 
    {

		public virtual string TenTinhThanh { get; set; }
		
		public virtual string MaTinhThanh { get; set; }
		

    }
}