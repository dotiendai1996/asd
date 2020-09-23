using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Hinnova.Configuration;
using Hinnova.Web;

namespace Hinnova.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class HinnovaDbContextFactory : IDesignTimeDbContextFactory<HinnovaDbContext>
    {
        public HinnovaDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HinnovaDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            HinnovaDbContextConfigurer.Configure(builder, configuration.GetConnectionString(HinnovaConsts.ConnectionStringName));

            return new HinnovaDbContext(builder.Options);
        }
    }
}