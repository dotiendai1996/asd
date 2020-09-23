using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities.Auditing;
using Abp.Domain.Entities;

namespace Hinnova.QLVB
{
	[Table("CA_Document")]
	public class Documents : FullAuditedEntity, IMayHaveTenant
	{
		public int? TenantId { get; set; }


		public virtual string Number { get; set; } // Số/kí hiệu

		public virtual int DocumentTypeId { get; set; }

		//public virtual string DateIssue { get; set; }

		public virtual string PlaceReceive { get; set; }

		public virtual string SaveTo { get; set; }

		public virtual string Summary { get; set; }

		public virtual int IncommingNumber { get; set; } // số đến

		public virtual int? Priority { get; set; }

		public virtual DateTime IncommingDate { get; set; }

		public virtual int Pages { get; set; } = 1;

		public virtual string Author { get; set; } // tác giả

		public virtual string ApprovedBy { get; set; } // người kí/ người duyệt

		public virtual string Attachment { get; set; }

		public virtual string TypeReceive { get; set; } // Loại tiếp nhận

		public virtual DateTime StartDate { get; set; }

		public virtual DateTime EndDate { get; set; }

		public virtual string Status { get; set; }

		public virtual string Note { get; set; }

		public virtual string MoreInformation { get; set; }

		public virtual bool IsActive { get; set; }

		public virtual int Order { get; set; } // Thứ tự

		public virtual string GroupAuthor { get; set; } // nhóm ban hành

		public virtual string Range { get; set; } // Lĩnh vực / phạm vi

		public virtual string Position { get; set; } // chức vụ

		public virtual string LinkedDocument { get; set; }  // văn bản liên kết

		public virtual int? Action { get; set; }
	}
}