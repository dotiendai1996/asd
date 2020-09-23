using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Hinnova.QLNS.Dtos;
using System.Collections.Generic;
using Hinnova.Organizations.Dto;

namespace Hinnova.QLNSDtos
{
    public class GetHoSoForEditOutput
    {
		public CreateOrEditHoSoDto HoSo { get; set; }
        public PagedResultDto<GetQuyTrinhCongTacForViewDto> QuaTrinhCongTac { get; set; }
        public List<OrganizationUnitDto> DanhSachCV { get; set; }
        public List<LichSuUploadDto> LichSuUpload { get; set; }

    }
}