using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllMemorize_KeywordsesInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string TenGoiNhoFilter { get; set; }

		public string XuLyChinhFilter { get; set; }

		public string DongXuLyFilter { get; set; }

		public string DeBietFilter { get; set; }

		public int? MaxHead_IDFilter { get; set; }
		public int? MinHead_IDFilter { get; set; }

		public string Full_NameFilter { get; set; }

		public string PrefixFilter { get; set; }

		public DateTime? MaxHire_DateFilter { get; set; }
		public DateTime? MinHire_DateFilter { get; set; }

		public string KeyWordFilter { get; set; }

		public int? MaxIsActiveFilter { get; set; }
		public int? MinIsActiveFilter { get; set; }



    }
}