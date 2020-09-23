using System;

namespace DeviceAbriDoor.Dto
{
    public class FullAuditedEntity
    {
        public long Id { get; set; }
        public bool IsDeleted { get; set; }
        public int DeleterUserId { get; set; }
        public DateTime DeletionTime { get; set; }
        public DateTime LastModificationTime { get; set; }
        public int LastModifierUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public int CreatorUserId { get; set; }
    }
}
