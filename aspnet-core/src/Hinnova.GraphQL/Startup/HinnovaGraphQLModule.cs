using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Hinnova.Startup
{
    [DependsOn(typeof(HinnovaCoreModule))]
    public class HinnovaGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}