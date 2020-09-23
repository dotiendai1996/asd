using System;
using System.IO;
using System.Net.Mail;
using System.Threading.Tasks;
using Abp.Configuration;
using Abp.Dependency;
using Abp.Net.Mail;
using Abp.Runtime.Security;

namespace Hinnova.Utils.EmailSenders
{
    public class CoreEmailSender : IEmailSender
    {
        public static bool SendEmail(EmailInfo EmailInfo)
        {
            try
            {
                #region comment

                #endregion
                var settingManager = IocManager.Instance.IocContainer.Resolve<ISettingManager>();

                string smtpAddress = EmailInfo.smtpAddress;

                int portNumber = EmailInfo.portNumber;

                bool enableSSL = EmailInfo.enableSSL;

                //new Attachment(EmailInfo.dataAttach, EmailInfo.nameAttach, "text/plain"

                //string emailFrom = "no_reply@gsoft.com.vn";
                //string password = "Ggroup0000))))";
                string emailFrom = EmailInfo.emailFrom;
                string password = EmailInfo.password;
           

                string displayName = EmailInfo.displayName;

                string subject = EmailInfo.Subj;
                string body = EmailInfo.Message;

                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(emailFrom, displayName);
                    if (!string.IsNullOrEmpty(EmailInfo.ToEmail))
                    {
                        mail.To.Add(EmailInfo.ToEmail);
                    }
                    if (!string.IsNullOrEmpty(EmailInfo.CcEmail))
                    {
                        mail.CC.Add(EmailInfo.CcEmail);
                    }
                    if (!string.IsNullOrEmpty(EmailInfo.BCCEmail))
                    {
                        mail.CC.Add(EmailInfo.BCCEmail);
                    }
                    mail.Subject = subject;
                    mail.Body = body;
                    mail.IsBodyHtml = true;

                    //media type that is respective of the data attach file
                    int i = 0;
                    //EmailInfo.isAttach = true;
                    if (EmailInfo.isAttach)
                        mail.Attachments.Add(new Attachment(EmailInfo.dataAttach, EmailInfo.nameAttach, "text/plain"));
                    //mail.Attachments.Add(); = (EmailInfo._attachments);
                    if (EmailInfo.dataMultiAttachs != null)
                    {
                        if (EmailInfo.dataMultiAttachs.isMulti)
                        {
                            foreach (MemoryStream item in EmailInfo.dataMultiAttachs.dataAttachs)
                            {
                                mail.Attachments.Add(new Attachment(item, EmailInfo.dataMultiAttachs.names[i], "text/plain"));
                                i++;
                            }
                        }

                    }
                    // Can set to false, if you are sending pure text.
                    using (SmtpClient smtp = new SmtpClient(smtpAddress, portNumber))
                    {
                        smtp.Credentials = new System.Net.NetworkCredential(emailFrom, password);
                        smtp.EnableSsl = enableSSL;
                        smtp.Send(mail);
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public void Send(string to, string subject, string body, bool isBodyHtml = true)
        {
            EmailInfo emailInfo = new EmailInfo()
            {
                ToEmail = to,
                Subj = subject,
                Message = body
            };

            SendEmail(emailInfo);
        }

        public void Send(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            EmailInfo emailInfo = new EmailInfo()
            {
                ToEmail = to,
                Subj = subject,
                Message = body
            };

            SendEmail(emailInfo);
        }
        //EDIT
        public void Send(MailMessage mail, bool normalize = true)
        {
            foreach (var mailInfo in mail.To)
            {
                var emailInfo = new EmailInfo()
                {   
                    ToEmail = mailInfo.Address,
                    Subj = mail.Subject,
                    Message = mail.Body ,
                    _attachments = mail.Attachments,
                

                };
                SendEmail(emailInfo);
            }
        }

        public async Task SendAsync(string to, string subject, string body, bool isBodyHtml = true)
        {
            EmailInfo emailInfo = new EmailInfo()
            {
                ToEmail = to,
                Subj = subject,
                Message = body
            };

            SendEmail(emailInfo);
            await Task.Delay(0);
        }

        public async Task SendAsync(string from, string to, string subject, string body, bool isBodyHtml = true)
        {
            EmailInfo emailInfo = new EmailInfo()
            {
                ToEmail = to,
                Subj = subject,
                Message = body
            };

            SendEmail(emailInfo);
            await Task.Delay(0);
        }

        public async Task SendAsync(MailMessage mail, bool normalize = true)
        {
            foreach (var mailInfo in mail.To)
            {
                var emailInfo = new EmailInfo()
                {
                    ToEmail = mailInfo.Address,
                    Subj = mail.Subject,
                    Message = mail.Body
                };
                SendEmail(emailInfo);
            }

            await Task.Delay(0);
        }
    }
}
