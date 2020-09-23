
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNSDtos
{
    public class CreateOrEditLichSuLamViecDto : EntityDto<int?>
    {

		public int? UngVienId { get; set; }

        public int? PhieuNghiPhepID { get; set; }
		public string NoiDung { get; set; }
        public string ChuDe { get; set; }


		public string TepDinhKem { get; set; }
		
		

    }
}