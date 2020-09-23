using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;
using Hinnova.QLNSDtos;

namespace Hinnova.QLNS.Dtos
{
    public class DataOnBackUngVienDto : EntityDto
    {
        public string Filter { get; set; }

        public string MaUngVienFilter { get; set; }

        public string HoVaTenFilter { get; set; }

        public string ViTriUngTuyenCodeFilter { get; set; }

        public string KenhTuyenDungCodeFilter { get; set; }

        public string GioiTinhCodeFilter { get; set; }

        public string TrangThaiCodeFilter { get; set; }

        public DateTime? StartDateFilter { get; set; }
        public DateTime? EndDateFilter { get; set; }

        public DateTime? Day1Filter { get; set; }
        public DateTime? Day2Filter { get; set; }
        public DateTime? Day3Filter { get; set; }

        public bool isCheckTimeFilter { get; set; }
        public DateTime? CreateTimeFilter { get; set; }

        public DateTime? LastModificationTimeFilter { get; set; }

        public string TienDoTuyenDungCodeFilter { get; set; }


    }

}