using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;
using Hinnova.Organizations.Dto;
using System.Collections.Generic;
using Hinnova.QLNS.Dtos;

namespace Hinnova.QLNSDtos
{
    public class GetUngVienForEditOutput
    {
        public CreateOrEditUngVienDto UngVien { get; set; }
        public ListResultDto<OrganizationUnitDto> OrganizationUnitList { get; set;}
        public List<NoiDaoTaoDto> NoiDaoTaoList { get; set; }
        public List<TinhThanhDto> TinhThanhList { get; set; }
        public List<TemplateDto> TemplateList { get; set; }
        public List<OrganizationUnitDto> CongViecList { get; set; }
        public List<LichSuLamViecDto> LichSuLamViecList { get; set; }
        public List<LichSuUploadDto> LichSuUploadList { get; set; }
        public GetConfigEmailForViewDto ConfigEmail { get; set; }
        public List<TruongGiaoDichDto> ViTriCongViec { get; set; }
        public List<TruongGiaoDichDto> TienDoTuyenDung { get; set; }
        public List<TruongGiaoDichDto> TrangThai { get; set; }
        public List<TruongGiaoDichDto> KenhTuyenDung { get; set; }
        public List<TruongGiaoDichDto> Congty { get; set; }
        public List<TruongGiaoDichDto> GioiTinh { get; set; }
        public List<TruongGiaoDichDto> TinhTrangHonNhan { get; set; }
        public List<TruongGiaoDichDto> TrinhDoDaoTao { get; set; }
        public List<TruongGiaoDichDto> XepLoaiHocLuc { get; set; }
        public List<TruongGiaoDichDto> TinhTrangNhanSu { get; set; }
        public List<DangKyKCBDto> NoiKhamBenhList { get; set; }

        public List<TruongGiaoDichDto> DefaultCbbOption { get; set; }
        

    }
}
