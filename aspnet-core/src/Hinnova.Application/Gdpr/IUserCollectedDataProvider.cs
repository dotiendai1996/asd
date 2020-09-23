using System.Collections.Generic;
using System.Threading.Tasks;
using Abp;
using Hinnova.Dto;

namespace Hinnova.Gdpr
{
    public interface IUserCollectedDataProvider
    {
        Task<List<FileDto>> GetFiles(UserIdentifier user);
    }
}
