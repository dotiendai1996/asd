using Abp.Application.Services.Dto;

namespace Hinnova.Management.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
		public string Filter { get; set; }
    }
}