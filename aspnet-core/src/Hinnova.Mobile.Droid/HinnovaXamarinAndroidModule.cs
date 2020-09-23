using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Hinnova
{
    [DependsOn(typeof(HinnovaXamarinSharedModule))]
    public class HinnovaXamarinAndroidModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaXamarinAndroidModule).GetAssembly());
        }
    }
}