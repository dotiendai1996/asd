using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Hinnova.Configure;
using Hinnova.Startup;
using Hinnova.Test.Base;

namespace Hinnova.GraphQL.Tests
{
    [DependsOn(
        typeof(HinnovaGraphQLModule),
        typeof(HinnovaTestBaseModule))]
    public class HinnovaGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaGraphQLTestModule).GetAssembly());
        }
    }
}