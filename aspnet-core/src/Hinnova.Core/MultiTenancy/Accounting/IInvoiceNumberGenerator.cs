using System.Threading.Tasks;
using Abp.Dependency;

namespace Hinnova.MultiTenancy.Accounting
{
    public interface IInvoiceNumberGenerator : ITransientDependency
    {
        Task<string> GetNewInvoiceNumber();
    }
}