using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("DataChamCongs")]
    public class DataChamCong : FullAuditedEntity 
    {
		public virtual int MaChamCong { get; set; }
		public virtual DateTime ProcessDate { get; set; }
		/// <summary>
		/// Lưu lịch sử thời gian check trong ngày
		/// HH:mm:ss~HH:mm:ss~HH:mm:ss~HH:mm:ss
		/// </summary>
		public virtual string CheckTime { get; set; }
		public virtual string CheckIn { get; set; }
		public virtual string CheckOut { get; set; }
		public virtual double? TimeWorkMorningDuration { get; set; }
		public virtual double? TimeWorkAfternoonDuration { get; set; }
		public virtual double? TimeViolatingRuleFirstDuration { get; set; }
		public virtual double? TimeViolatingRuleLastDuration { get; set; }
		public virtual double? TimeOTDuration { get; set; }
		public virtual string Status { get; set; }
	}
}