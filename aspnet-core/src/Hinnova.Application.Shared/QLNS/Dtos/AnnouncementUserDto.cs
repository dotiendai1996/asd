using System;
using System.Collections.Generic;
using System.Text;

namespace Hinnova.QLNS.Dtos
{
    public class AnnouncementUserDto
    {
        public int Id { get; set; }
        public long UserId { get; set; }
        public bool HasRead { get; set; }
        public Guid AnnouncementId { get; set; }

        public AnnouncementDto Announcement { get; set; }
    }
}
