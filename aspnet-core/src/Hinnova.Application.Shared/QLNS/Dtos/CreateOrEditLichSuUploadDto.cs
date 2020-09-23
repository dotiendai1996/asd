
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditLichSuUploadDto : EntityDto<int?>
    {

		public string TenFile { get; set; }
		
		
		public string DungLuong { get; set; }
		
		
		public string TieuDe { get; set; }
		
		
		public string Type { get; set; }
		
		
		public string TypeID { get; set; }
		
		

    }
}