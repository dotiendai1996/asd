using Abp.Application.Services.Dto;
using System;

namespace Hinnova.QLVB.Dtos
{
    public class GetAllDynamicActionsForExcelInput
    {
		public string Filter { get; set; }

		public int? MaxLabelIdFilter { get; set; }
		public int? MinLabelIdFilter { get; set; }

		public int IsActiveFilter { get; set; }

		public int HasSaveFilter { get; set; }

		public int HasReturnFilter { get; set; }

		public int HasTransferFilter { get; set; }

		public int HasSaveAndTransferFilter { get; set; }

		public int HasFinishFilter { get; set; }

		public int IsTopPositionFilter { get; set; }

		public int IsBackFilter { get; set; }

		public int HasAssignWorkFilter { get; set; }

		public string DescriptionFilter { get; set; }

		public int? MaxOrderFilter { get; set; }
		public int? MinOrderFilter { get; set; }



    }
}