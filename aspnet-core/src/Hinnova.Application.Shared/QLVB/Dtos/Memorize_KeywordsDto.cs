
using System;
using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class Memorize_KeywordsDto : EntityDto
    {
		public string TenGoiNho { get; set; }

		public string XuLyChinh { get; set; }

		public string DongXuLy { get; set; }

		public string DeBiet { get; set; }

		public int Head_ID { get; set; }

		public string Full_Name { get; set; }

		public string Prefix { get; set; }

		public DateTime Hire_Date { get; set; }

		public string KeyWord { get; set; }

		public int IsActive { get; set; }



    }
}