using System.Threading.Tasks;
using Hinnova.Sessions.Dto;

namespace Hinnova.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
