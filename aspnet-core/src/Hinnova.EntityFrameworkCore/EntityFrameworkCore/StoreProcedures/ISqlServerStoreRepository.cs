using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Hinnova.EntityFrameworkCore.StoreProcedure
{
    public interface ISqlServerStoreRepository
    {
        Task<List<T>> SelectDataList<T>(string packname, string procname, Object parameters) where T : new();
        Task<DataSet> SelectDataSet(string packageName, string procName, Object parameters);
    }
}
