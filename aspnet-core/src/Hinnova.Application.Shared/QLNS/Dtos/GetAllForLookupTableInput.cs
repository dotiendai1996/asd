using Abp.Application.Services.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}