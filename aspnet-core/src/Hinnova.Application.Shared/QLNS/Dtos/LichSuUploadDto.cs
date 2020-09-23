
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class LichSuUploadDto : EntityDto
    {
		public string TenFile { get; set; }

		public string DungLuong { get; set; }

		public string TieuDe { get; set; }

		public string Type { get; set; }

		public string TypeID { get; set; }



    }

    public class LichSuUploadNewDto : EntityDto
    {
        public string TenFile { get; set; }

        public string DungLuong { get; set; }

        public string TieuDe { get; set; }
    }
}