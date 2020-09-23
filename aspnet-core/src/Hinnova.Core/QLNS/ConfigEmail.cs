using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("ConfigEmails")]
    public class ConfigEmail : Entity 
    {

		public virtual string DiaChiEmail { get; set; }
		
		public virtual string TenHienThi { get; set; }
		
		public virtual string DiaChiIP { get; set; }
		
		public virtual int CongSMTP { get; set; }
		
		public virtual bool CheckSSL { get; set; }
		
		public virtual bool CheckThongTin { get; set; }
		
		public virtual string TenMien { get; set; }
		
		public virtual string TenTruyCap { get; set; }
		
		public virtual string MatKhau { get; set; }
		
		public virtual string TenCTY { get; set; }
		

    }
}