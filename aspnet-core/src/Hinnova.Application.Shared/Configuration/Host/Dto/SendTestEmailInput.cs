using System.ComponentModel.DataAnnotations;
using Abp.Authorization.Users;

namespace Hinnova.Configuration.Host.Dto
{
    public class SendTestEmailInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        public string mailTo { get; set; }
        public string mailForm { get; set; }
        public string subject { get; set; }

        public string body { get; set; }
        public string filedinhkem { get; set; }

        public string tenTruyCap { get; set; }
        public string matKhau { get; set; }

        public int congSMTP { get; set; }
        public string diaChiIP { get; set; }

        public string curentTime { get; set; }
    }
}