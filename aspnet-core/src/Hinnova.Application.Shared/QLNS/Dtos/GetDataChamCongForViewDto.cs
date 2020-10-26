using Hinnova.Organizations.Dto;
using Hinnova.QLNSDtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Hinnova.QLNS.Dtos
{
    public class DataChamCongFilter
    {
        public List<TruongGiaoDichDto> Congty { get; set; }
        public List<OrganizationUnitDto> CongViecList { get; set; }
        public List<TruongGiaoDichDto> ViTriCongViec { get; set; }
    }

    public class GetDataChamCongForViewDto
    {
        public DataChamCongDto DataChamCong { get; set; }
        public int RowIndex { get; set; }
        public int? MaChamCong { get; set; }
        public string MaNhanVien { get; set; }
        public string HoVaTen { get; set; }
        public string TenCTy { get; set; }
        public string DonViCongTacName { get; set; }
        public string ChucDanh { get; set; }
        public DateTime ProcessDate { get; set; }
        public string CheckTime { get; set; }
        public string CheckTimeIn
        {
            get
            {
                var timeCheckTime = CheckTime?.Split('~');
                if (timeCheckTime != null && timeCheckTime.Count() > 0)
                    return timeCheckTime.FirstOrDefault();
                return string.Empty;
            }
        }
        public string CheckTimeOut
        {
            get
            {
                var timeCheckTime = CheckTime?.Split('~');
                if (timeCheckTime != null && timeCheckTime.Count() > 1)
                    return timeCheckTime.LastOrDefault();
                return string.Empty;
            }
        }
        public double? TimeViolatingRuleFirstDuration { get; set; }
        public double? TimeViolatingRuleLastDuration { get; set; }
        public double? TimeOTDuration { get; set; }
        public double? TimeWorkDuration { get; set; }
        public string WorkOffDate { get; set; }
        public string MissionDate { get; set; }

    }
}