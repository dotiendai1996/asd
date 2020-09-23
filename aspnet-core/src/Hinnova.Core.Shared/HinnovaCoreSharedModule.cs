using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Hinnova
{
    public class HinnovaCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HinnovaCoreSharedModule).GetAssembly());
        }
    }
}