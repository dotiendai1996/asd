using DeviceAbriDoor.Configs;
using DeviceAbriDoor.Models;
using DeviceAbriDoor.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDeviceAbriDoor;

namespace DeviceAbriDoor.Utils
{
    public class DeviceUtils
    {
        private static DeviceUtils instance = new DeviceUtils();
        public static DeviceUtils Instance
        {
            get
            {
                if (instance == null) instance = new DeviceUtils();
                return instance;
            }
        }

        public IList<CreateOrEditDataChamCongDto> GetDataChamCongAll()
        {
            IList<CreateOrEditDataChamCongDto> result = new List<CreateOrEditDataChamCongDto>();
            var configs = AppConfigSection.GetInstance();
            if (configs.Device.Enable)
            {
                LogUtils.WirteLogInfo("Schedule Get Data From Device Start");

                AbriDoorSDK.Instance.Connect_Net(configs.Device.DeviceAddress, configs.Device.Port);

                int dwTMachineNumber = 0, dwEnrollNumber = 0, dwEMachineNumber = 0, dwVerifyMode = 0,
                    dwInOutMode = 0, dwYear = 0, dwMonth = 0, dwDay = 0, dwHour = 0, dwMinute = 0;

                AbriDoorSDK.Instance.ReadAllGLogData();

                while (AbriDoorSDK.Instance.GetAllGLogData(ref dwTMachineNumber, ref dwEnrollNumber, ref dwEMachineNumber, ref dwVerifyMode, ref dwInOutMode, ref dwYear, ref dwMonth, ref dwDay, ref dwHour, ref dwMinute))
                {
                    var timekeepingDate = new DateTime(dwYear, dwMonth, dwDay, dwHour, dwMinute, 0);
                    var dataChamCong = new CreateOrEditDataChamCongDto();
                    dataChamCong.MaChamCong = dwEnrollNumber;
                    dataChamCong.TimeCheck = timekeepingDate.ToString(BaseConfig.FormatDate);

                    result.Add(dataChamCong);
                }

                AbriDoorSDK.Instance.Disconnect();

                LogUtils.WirteLogInfo($"Schedule Get Data From Device End - Count: {result.Count}");
            }

            return result;
        }

        public IEnumerable<CreateOrEditDataChamCongDto> GetDataChamCongByDate(DateTime processDate)
        {
            var dataChamCongList = GetDataChamCongAll();

            var startDate = processDate.Date;
            var endDate = DateTime.Now.AddDays(1).Date.AddTicks(-1);

            var result = dataChamCongList.Where(x => x.TimeCheckDate >= startDate && x.TimeCheckDate <= endDate);
            return result;
        }

        public IList<CreateOrEditDataChamCongDto> GetDataChamCongByDate(DateTime processDateStart, DateTime processDateEnd)
        {
            var dataChamCongList = GetDataChamCongAll();

            var startDate = processDateStart.Date;
            var endDate = processDateEnd.AddDays(1).Date.AddTicks(-1);

            var result = dataChamCongList.Where(x => x.TimeCheckDate >= startDate && x.TimeCheckDate <= endDate);
            return result.ToList();
        }
    }
}
