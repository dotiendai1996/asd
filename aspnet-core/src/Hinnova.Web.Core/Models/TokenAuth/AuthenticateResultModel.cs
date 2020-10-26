using System.Collections.Generic;

namespace Hinnova.Web.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public string EncryptedAccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public bool ShouldResetPassword { get; set; }

        public string PasswordResetCode { get; set; }

        public long UserId { get; set; }

        public bool RequiresTwoFactorVerification { get; set; }

        public IList<string> TwoFactorAuthProviders { get; set; }

        public string TwoFactorRememberClientToken { get; set; }

        public string ReturnUrl { get; set; }

        public string RefreshToken { get; set; }
        public string BranchId { get; set; }

        public string BranchCode { get; set; }
        public string BranchType { get; set; }
        public string HoVaTen { get; set; }
        public string TenCty { get; set; }
        public int MaChamCong { get; set; }

        public string Image { get; set; }
    }
}