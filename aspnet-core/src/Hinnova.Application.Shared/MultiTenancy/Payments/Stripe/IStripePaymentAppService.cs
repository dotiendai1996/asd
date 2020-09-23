using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.MultiTenancy.Payments.Stripe.Dto;

namespace Hinnova.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        Task CreateSubscription(StripeCreateSubscriptionInput input);

        Task UpdateSubscription(StripeUpdateSubscriptionInput input);
        
        StripeConfigurationDto GetConfiguration();
    }
}