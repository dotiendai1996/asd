using Abp.Application.Services.Dto;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}