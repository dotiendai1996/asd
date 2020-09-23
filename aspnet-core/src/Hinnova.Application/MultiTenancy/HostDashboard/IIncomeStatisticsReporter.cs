using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Hinnova.MultiTenancy.HostDashboard.Dto;

namespace Hinnova.MultiTenancy.HostDashboard
{
    public interface IIncomeStatisticsService
    {
        Task<List<IncomeStastistic>> GetIncomeStatisticsData(DateTime startDate, DateTime endDate,
            ChartDateInterval dateInterval);
    }
}