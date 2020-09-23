using System;
using System.Collections.Generic;
using System.Text;
using Hinnova.QLNS.Dtos;

namespace Hinnova.Management.Dtos
{
     public class CreateOrEditNghiPhepInput
    {
        public CreateOrEditQuanLyNghiPhepDto NghiPhep { get; set; }


        public List<LichSuUploadNewDto> LichSuUpLoad { get; set; }

        public List<QuanLyTrucTiepPNPDto> QuanLyTrucTiepPNP { get; set; }


        public CreateOrEditNghiPhepInput()
        {
            LichSuUpLoad = new List<LichSuUploadNewDto>();

            QuanLyTrucTiepPNP = new List<QuanLyTrucTiepPNPDto>();
        }
    }
}
