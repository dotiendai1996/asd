
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditDataChamCongDto : EntityDto<int?>
    {
        [Required]
        public int MaChamCong { get; set; }
        [Required]
        public string TimeCheck { get; set; }

        public DateTime? TimeCheckDate
        {
            get
            {
                DateTime dateTime;
                DateTime.TryParseExact(TimeCheck, AppConsts.DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime);
                return dateTime;
            }
        }
    }
}