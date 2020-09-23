using System.Threading.Tasks;
using Abp.Application.Services;

namespace Hinnova.MultiTenancy
{
    public interface ISubscriptionAppService : IApplicationService
    {
        Task DisableRecurringPayments();

        Task EnableRecurringPayments();
    }
}
