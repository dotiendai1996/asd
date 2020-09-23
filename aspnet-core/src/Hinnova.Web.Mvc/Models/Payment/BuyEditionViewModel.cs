using System.Collections.Generic;
using Hinnova.Editions;
using Hinnova.Editions.Dto;
using Hinnova.MultiTenancy.Payments;
using Hinnova.MultiTenancy.Payments.Dto;

namespace Hinnova.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
