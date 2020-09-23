using System.ComponentModel.DataAnnotations;

namespace Hinnova.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}