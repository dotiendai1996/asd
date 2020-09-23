using System.Collections.Generic;
using Hinnova.Editions.Dto;
using Hinnova.MultiTenancy.Payments;

namespace Hinnova.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}