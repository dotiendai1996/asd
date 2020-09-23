using System.Threading.Tasks;
using Hinnova.Security.Recaptcha;

namespace Hinnova.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
