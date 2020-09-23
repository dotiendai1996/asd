using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Hinnova
{
    [DependsOn(typeof(HinnovaCoreSharedModule))]
    public class HinnovaApplicationSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaApplicationSharedModule).GetAssembly());
        }
    }
}