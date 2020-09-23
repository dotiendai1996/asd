using System;
using System.Collections.Generic;
using System.Text;
using Hinnova.QLNSDtos;

namespace Hinnova.QLNS.Dtos
{
   public class CreateOrEditHopDongInput
    {
        public CreateOrEditHopDongDto HopDong { get; set; }


        public List<LichSuUploadNewDto> LichSuUpLoad { get; set; }


        public CreateOrEditHopDongInput()
        {
            LichSuUpLoad = new List<LichSuUploadNewDto>();
        }
    }
}
