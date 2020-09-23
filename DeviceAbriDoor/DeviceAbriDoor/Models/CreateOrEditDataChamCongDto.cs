
using DeviceAbriDoor.Dto;
using DeviceAbriDoor.Models.Base;
using System;
using System.Globalization;

namespace DeviceAbriDoor.Models
{
    public class CreateOrEditDataChamCongDto : EntityDto
    {
        public int MaChamCong { get; set; }
        public string TimeCheck { get; set; }

        public DateTime? TimeCheckDate
        {
            get
            {
                DateTime dateTime;
                DateTime.TryParseExact(TimeCheck, BaseConfig.FormatDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                return dateTime;
            }
        }
    }
}