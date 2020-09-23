using Abp.Domain.Repositories;
using Dapper;
using Hinnova.Configuration;
using Hinnova.QLVB.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hinnova.QLVB
{
    public class TransferAndHandingAppService : HinnovaAppServiceBase, ITransferAndHandingAppService
    {
        private readonly IRepository<DocumentHandling> _documentHandlingRepository;
        private readonly string connectionString;

        public TransferAndHandingAppService(IRepository<DocumentHandling> documentHandlingRepository, IWebHostEnvironment env)
        {
            _documentHandlingRepository = documentHandlingRepository;
            connectionString = env.GetAppConfiguration().GetConnectionString("Default");
        }
        public async Task<List<DocumentHandlingDto>> GetListHandlingByDocumentId(int documentId)
        {
            var result = await _documentHandlingRepository.GetAll().Where(x => x.DocumentId == documentId).ToListAsync();
            return ObjectMapper.Map<List<DocumentHandlingDto>>(result);

            //using (SqlConnection conn = new SqlConnection(connectionString))
            //{
            //    if (conn.State == ConnectionState.Closed)
            //    {
            //        await conn.OpenAsync();
            //    }
            //    var num = await conn.QueryAsync<int>(sql: "dbo.GetNextIncommingNumber", commandType: CommandType.StoredProcedure);
            //    return num.AsQueryable().First();
            //}
        }
    }
}
