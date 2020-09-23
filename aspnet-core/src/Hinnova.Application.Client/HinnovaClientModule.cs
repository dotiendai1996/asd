using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Hinnova
{
    public class HinnovaClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaClientModule).GetAssembly());
        }
    }
}
