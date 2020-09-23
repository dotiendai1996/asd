using System;
using System.Collections.Generic;
using System.Text;
using Hinnova.QLNS.Dtos;
using Hinnova.QLNSDtos;

namespace Hinnova.QLNS
{
  public  class CreateOrEditHoSoInput
    {
        public CreateOrEditHoSoDto HoSo { get; set; }


        public List<LichSuUploadNewDto> LichSuUpLoad { get; set; }


        public CreateOrEditHoSoInput()
        {
            LichSuUpLoad = new List<LichSuUploadNewDto>();
        }
    }
}
