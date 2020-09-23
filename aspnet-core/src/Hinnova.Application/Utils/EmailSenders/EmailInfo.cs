using System.IO;
using System.Net.Mail;

namespace Hinnova.Utils.EmailSenders
{
    public class EmailInfo
    {
        public string ToEmail { get; set; }
        public string CcEmail { get; set; }
        public string BCCEmail { get; set; }
        public string Subj { get; set; }
        public string Message { get; set; }
        public MemoryStream dataAttach { get; set; }
        public string nameAttach { get; set; }
        public bool isAttach { get; set; }
        public AttachFile dataMultiAttachs { get; set; }

        public AttachmentCollection _attachments;

        public string smtpAddress { get; set; }
        public int portNumber { get; set; }
        public bool enableSSL { get; set; }
        public string displayName { get; set; }
       
        public string password { get; set; }
        public string emailFrom { get; set; }

    }
}
