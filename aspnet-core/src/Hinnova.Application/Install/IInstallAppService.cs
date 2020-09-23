using System.Threading.Tasks;
using Abp.Application.Services;
using Hinnova.Install.Dto;

namespace Hinnova.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}