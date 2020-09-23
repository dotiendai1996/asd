using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Hinnova.QLNSDtos;

namespace Hinnova.QLNS.Dtos
{
     public class CreateOrEditUngVienInput
    {
    
        public CreateOrEditUngVienDto UngVien { get; set; }

    
        public List<LichSuUploadNewDto> LichSuUpLoad { get; set; }


        public CreateOrEditUngVienInput()
        {
            LichSuUpLoad = new List<LichSuUploadNewDto>();
        }
    }
}
