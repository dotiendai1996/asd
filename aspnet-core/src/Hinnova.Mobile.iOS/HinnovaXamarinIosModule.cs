using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Hinnova
{
    [DependsOn(typeof(HinnovaXamarinSharedModule))]
    public class HinnovaXamarinIosModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaXamarinIosModule).GetAssembly());
        }
    }
}