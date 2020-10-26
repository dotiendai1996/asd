
using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Hinnova.QLNS.Dtos
{
    public class CreateOrEditQuyTrinhCongTacDto : EntityDto<int?>
    {

		public string TenCty { get; set; }
		
		
		public DateTime DateTo { get; set; }
		
		
		public DateTime DateFrom { get; set; }
		
		
		public string ViTriCongViecCode { get; set; }

		public string QuanLyTrucTiep { get; set; }

		public int? DonViCongTacID { get; set; }


        public string TrangThaiCode { get; set; }
        
		public string GhiChu { get; set; }

        public string LyDo { get; set; }

		public int MaHoSo { get; set; }

        public DateTime NgayDuyet { get; set; }

        public string NguoiDuyetId { get; set; }
        
        public long CreatorUserId { get; set; }

        public string TruongBoPhanId { get; set; }

        public string GiamDocBoPhanId { get; set; }

        public string PhongCTNSId { get; set; }

        public string GiamDocId { get; set; }

        public string Status { get; set; }

	}
}