
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class DataChamCongDto : EntityDto
    {
		public int? MaChamCong { get; set; }
		public DateTime ProcessDate { get; set; }
		public string CheckTime { get; set; }
        public string CheckIn { get; set; }
        public string CheckOut { get; set; }
		public virtual double? TimeWorkDuration { get; set; }
		public virtual double? TimeViolatingRuleFirstDuration { get; set; }
		public virtual double? TimeViolatingRuleLastDuration { get; set; }
		public virtual double? TimeOTDuration { get; set; }
		public string BranchId { get; set; }
		public string Status { get; set; }
        public string HoVaTen { get; set; }
	}
}