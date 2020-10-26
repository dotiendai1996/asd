using System;
using System.Collections.Generic;
using System.Text;
using Hinnova.Authorization.Users.Dto;

namespace Hinnova.QLNS.Dtos
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Image { get; set; }
        public long UserId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool Status { get; set; }
    }
}