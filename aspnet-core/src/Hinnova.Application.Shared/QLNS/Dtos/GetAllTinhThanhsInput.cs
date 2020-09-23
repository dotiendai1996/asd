using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLNSDtos
{
    public class GetAllTinhThanhsInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }

		public string TenTinhThanhFilter { get; set; }

		public string MaTinhThanhFilter { get; set; }



    }
}