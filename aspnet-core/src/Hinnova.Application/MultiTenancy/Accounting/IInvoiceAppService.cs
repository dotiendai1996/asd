using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Hinnova.MultiTenancy.Accounting.Dto;

namespace Hinnova.MultiTenancy.Accounting
{
    public interface IInvoiceAppService
    {
        Task<InvoiceDto> GetInvoiceInfo(EntityDto<long> input);

        Task CreateInvoice(CreateInvoiceDto input);
    }
}
