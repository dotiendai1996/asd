using Hinnova.Editions;
using Hinnova.Editions.Dto;
using Hinnova.MultiTenancy.Payments;
using Hinnova.Security;
using Hinnova.MultiTenancy.Payments.Dto;

namespace Hinnova.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
