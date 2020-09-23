using DeviceAbriDoor.Models.Base;
using DeviceAbriDoor.RestSharp;
using DeviceAbriDoor.Utils;
using Quartz;
using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace ScheduledService.Schedules
{
    public class DeviceJob : IJob
    {
        public void Execute(IJobExecutionContext context)
        {
            LogUtils.WirteLogInfo($"Job Schedule Start");

            var processDateStr = WebApiHelper.Instance.Get(WebApiConstant.ADMIN_DATACHAMCONG_GETPROCESSDATEMAX)
                .Result.ToObjectModel<string>();

            DateTime processDate;
            if (DateTime.TryParseExact(processDateStr, BaseConfig.FormatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out processDate))
            {
                var chamCongList = DeviceUtils.Instance.GetDataChamCongByDate(processDate);

                LogUtils.WirteLogInfo($"Get Data From Device GetDataChamCongByDate - Count: {chamCongList.Count()}");

                foreach (var chamCong in chamCongList)
                {
                    WebApiHelper.Instance.Post(WebApiConstant.ADMIN_DATACHAMCONG_CREATEOREDIT, chamCong);
                }

                LogUtils.WirteLogInfo($"Sync data cham cong to database is success - Count: {chamCongList.Count()}");
            }
            else
            {
                LogUtils.WirteLogError($"Get processDateMax Error - Url: {WebApiConstant.ADMIN_DATACHAMCONG_GETPROCESSDATEMAX}");
            }

            LogUtils.WirteLogInfo($"Job Schedule End");
        }
    }
}
