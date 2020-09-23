using Microsoft.Extensions.Configuration;

namespace Hinnova.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
