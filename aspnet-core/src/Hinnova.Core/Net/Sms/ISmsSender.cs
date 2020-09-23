using System.Threading.Tasks;

namespace Hinnova.Net.Sms
{
    public interface ISmsSender
    {
        Task SendAsync(string number, string message);
    }
}