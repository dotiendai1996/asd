using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditMobileDataChamCongDto : EntityDto
    {
        [Required]
        public int MaChamCong { get; set; }
        [Required]
        public string CheckTime { get; set; }
        [Required]
        public DateTime ProcessDate { get; set; }
    }
}
