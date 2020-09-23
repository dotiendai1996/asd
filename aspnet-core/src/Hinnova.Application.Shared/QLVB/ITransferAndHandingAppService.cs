using Abp.Application.Services;
using Hinnova.QLVB.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hinnova.QLVB
{
    public interface ITransferAndHandingAppService : IApplicationService
    {
        Task<List<DocumentHandlingDto>> GetListHandlingByDocumentId(int documentId);
    }
}
