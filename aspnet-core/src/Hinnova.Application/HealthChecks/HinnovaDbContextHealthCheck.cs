using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Hinnova.EntityFrameworkCore;

namespace Hinnova.HealthChecks
{
    public class HinnovaDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public HinnovaDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("HinnovaDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("HinnovaDbContext could not connect to database"));
        }
    }
}
