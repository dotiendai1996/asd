//using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;
//using HinnovaAbp.Entities;

namespace Hinnova.QLVB.Dtos
{
    //[AutoMapFrom(typeof(Hinnova.QLVB.Entities.DynamicField))]
    public class DynamicFieldListDto : FullAuditedEntity<int>
    {
        public string NameDescription { get; set; }
        public string Name { get; set; }
        public int TypeField { get; set; }
        public string ClassAttach { get; set; }
        public int Width { get; set; }
        public int WidthDescription { get; set; }
        public int TenantID { get; set; }
        public int ModuleID { get; set; }
        public string Value { get; set; }
        public int Order { get; set; }
        public int? ValueId { get; set; }
    }
}