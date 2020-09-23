using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.MultiTenancy.Payments.PayPal.Dto;

namespace Hinnova.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
