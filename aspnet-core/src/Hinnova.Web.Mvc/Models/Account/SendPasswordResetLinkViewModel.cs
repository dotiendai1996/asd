using System.ComponentModel.DataAnnotations;

namespace Hinnova.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}