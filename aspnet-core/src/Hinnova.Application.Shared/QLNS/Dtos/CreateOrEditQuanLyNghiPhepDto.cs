
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditQuanLyNghiPhepDto : EntityDto<int?>
    {

		public string TenNhanVien { get; set; }
		
		
		public int? DonViCongTacID { get; set; }
		
		
		public string MaNV { get; set; }
		
		
		public bool NghiPhep { get; set; }
		
		
		public bool CongTac { get; set; }
        public bool TangCa { get; set; }


		public DateTime NgayBatDau { get; set; }
		
		
		public DateTime NgayKetThuc { get; set; }
		
		
		public string LyDo { get; set; }

        public string QuanLyTrucTiepID { get; set; }

//		public int? QuanLyTrucTiepID { get; set; }
		
		
		public string TruongBoPhanID { get; set; }
		
		
		public string TepDinhKem { get; set; }
		
		
		public string TenCTY { get; set; }
		
		
		public int? NguoiDuyetID { get; set; }
		
		
		public DateTime NgayDuyet { get; set; }
		
		
		public int? trangThaiID { get; set; }
		
		

    }
}