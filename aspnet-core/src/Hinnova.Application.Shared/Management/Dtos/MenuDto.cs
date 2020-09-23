using Abp.Application.Services.Dto;
using System;

namespace Hinnova.Management.Dtos
{
    public class MenuDto : EntityDto
    {
        public string Name { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public string Description { get; set; }

        public int? Parent { get; set; }

        //public bool IsParent { get; set; }

        public string Link { get; set; }

        //public string Type { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime? LastModificationTime { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletionTime { get; set; }

        public int Index { get; set; }

        //public bool IsDelimiter { get; set; }

        public string RequiredPermissionName { get; set; }

        public int Dept { get; set; }

        public string Num { get; set; }

        public MenuDto[] Children { get; set; }

        public int NumBeforeComma { get; set; }

        public int NumAfterComma { get; set; }

        //public string UserRoleName { get; set; }
    }
}