
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNSDtos
{
    public class LichSuLamViecDto : FullAuditedEntityDto
    {
		public int? UngVienId { get; set; }
        public  int? PhieuNghiPhepID { get; set; }
        public string NoiDung { get; set; }

        public string ChuDe { get; set; }

        public string TepDinhKem { get; set; }

        public string FullName { get; set; }

    }
}