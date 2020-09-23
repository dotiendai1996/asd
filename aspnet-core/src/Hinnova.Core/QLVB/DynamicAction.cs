using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("DynamicAction")]
    public class DynamicAction : Entity , IMayHaveTenant
    {
			public int? TenantId { get; set; }
			

		public virtual int LabelId { get; set; }

		public virtual int RoleId { get; set; }
		
		//public virtual bool IsActive { get; set; }
		
		public virtual bool HasSave { get; set; } // Lưu
		
		public virtual bool HasReturn { get; set; } //Trả về
		
		public virtual string HasTransfer { get; set; } // Chuyển
		
		public virtual bool HasSaveAndTransfer { get; set; } // Lưu và chuyển
		
		public virtual bool HasFinish { get; set; } // Kết thúc
		
		public virtual int Position { get; set; } // vị trí
		
		public virtual bool IsBack { get; set; } // Trở về
		
		public virtual string HasAssignWork { get; set; } // Giao việc

		public virtual bool HasDelete { get; set; } // Xóa

		public virtual bool HasCreate { get; set; } // Thêm mới

		public virtual bool HasReport { get; set; } // Phiếu trình

		//public virtual bool HasRemoveStar { get; set; } // Bỏ sao

		public virtual bool HasSaveAndCreate { get; set; } // Lưu và thêm mới
		
		public virtual string Description { get; set; }
		
		public virtual int Order { get; set; }

		public virtual string CellTemplate { get; set; }
    }
}