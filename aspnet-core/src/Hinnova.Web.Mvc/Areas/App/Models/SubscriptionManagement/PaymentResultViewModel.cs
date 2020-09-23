using Abp.AutoMapper;
using Hinnova.Editions;
using Hinnova.MultiTenancy.Payments.Dto;

namespace Hinnova.Web.Areas.App.Models.SubscriptionManagement
{
    [AutoMapTo(typeof(ExecutePaymentDto))]
    public class PaymentResultViewModel : SubscriptionPaymentDto
    {
        public EditionPaymentType EditionPaymentType { get; set; }
    }
}