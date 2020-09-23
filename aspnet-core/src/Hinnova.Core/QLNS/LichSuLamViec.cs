using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLNS
{
	[Table("LichSuLamViec")]
    public class LichSuLamViec : FullAuditedEntity 
    {

        public virtual int? UngVienId { get; set; }

        public virtual int? PhieuNghiPhepID { get; set; }

		public virtual string NoiDung { get; set; }
		
		public virtual string TepDinhKem { get; set; }

        public virtual string ChuDe { get; set; }



	}
}