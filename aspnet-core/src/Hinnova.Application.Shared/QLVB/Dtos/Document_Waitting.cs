using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLVB.Dtos
{
    public  class Document_Waitting
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public string CreatorUserId { get; set; }
        public DateTime LastModificationTime { get; set; }
        public string LastModifierUserId { get; set; }


        public string IsDeleted { get; set; }
        public string DeleterUserId { get; set; }

        public DateTime DeletionTime { get; set; }
        public string TenantId { get; set; }
        public string Number { get; set; }
        public string SaveTo { get; set; }
        public string Summary { get; set; }
        public string ApprovedBy { get; set; }
        public string Attachment { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string Position { get; set; }
        public string GroupAuthor { get; set; }
        public string DocumentTypeId { get; set; }
        public string Priority { get; set; }


        public DateTime IncommingDate { get; set; }
        public string TypeReceive { get; set; }

        public string PlaceReceive { get; set; }
        public string IsActive { get; set; }
        public string Pages { get; set; }
        public string Order { get; set; }
        public string MoreInformation { get; set; }
        public string IncommingNumber { get; set; }
        public string Author { get; set; }
        public string Note { get; set; }
        public string Status { get; set; }

        public string PersonalComment { get; set; }
        public string Person { get; set; }

        public string Group { get; set; }
        public string UserId { get; set; }
        public string beforeStatus { get; set; }
        public string HandlerBeforeContent { get; set; }
        public string HandlerBeforeComment { get; set; }
        public string Handler { get; set; }
        public string Action { get; set; }
        public string LinkedDocument { get; set; }
        public string Range { get; set; }
        public string Type { get; set; }
        public string MainHandling { get; set; }
        public string CoHandling { get; set; }
        public string ToKnow { get; set; }
        public int STT { get; set; }



    }
}
