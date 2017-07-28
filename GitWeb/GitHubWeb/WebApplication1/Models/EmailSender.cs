using Microsoft.AspNet.Identity;
using System;
using System.Web;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Configuration;
using System.Web.Configuration;
using System.Configuration;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.IO;

namespace WebApplication1.Models
{
    public class EmailSender : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return configSendGridasync(message);

        }
        private Task configSendGridasync(IdentityMessage message)
        {
            string tomail = string.Empty;
            string cc = string.Empty;
            string subject = string.Empty;
            string bodyText = string.Empty;
            string filepath = string.Empty;
            Configuration configurationFile = WebConfigurationManager.OpenWebConfiguration(HttpContext.Current.Request.ApplicationPath);
            MailSettingsSectionGroup mailSettings = configurationFile.GetSectionGroup("system.net/mailSettings") as MailSettingsSectionGroup;

            using (MailMessage mail = new MailMessage())
            {
                if (!string.IsNullOrEmpty(tomail))
                {
                    var toemails = tomail.Split(';');
                    foreach (string email in toemails)
                    {
                        mail.To.Add(email);
                    }
                }
                if (!string.IsNullOrEmpty(cc))
                {
                    string[] arr = { ",", ";" };
                    var AllCCMail = cc.Split(arr, StringSplitOptions.RemoveEmptyEntries);
                    foreach (string ccmail in AllCCMail)
                    {
                        mail.CC.Add(ccmail);
                    }
                }
                mail.From = new MailAddress(mailSettings.Smtp.From);
                mail.Subject = subject;
                string Body = bodyText;
                mail.Body = Body;
                string fileName = string.Empty;
                if (!string.IsNullOrEmpty(filepath))
                {
                    fileName = Path.GetFileName(filepath);
                }


                mail.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = mailSettings.Smtp.Network.Host;
                    smtp.Port = mailSettings.Smtp.Network.Port;
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential
                    (mailSettings.Smtp.Network.UserName, mailSettings.Smtp.Network.Password);// Enter seders User name and password  
                    smtp.EnableSsl = true;
                    ServicePointManager.ServerCertificateValidationCallback =
                       delegate (object s, X509Certificate certificate,
                                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                       { return true; };
                    if (string.IsNullOrEmpty(fileName) == false)
                    {
                        using (FileStream fs = new FileStream(filepath, FileMode.Open))
                        {
                            mail.Attachments.Add(new System.Net.Mail.Attachment(fs, fileName));
                            return smtp.SendMailAsync(mail);
                        }
                        System.IO.File.Delete(filepath);

                    }
                    else
                    {
                        return smtp.SendMailAsync(mail);
                    }
                }
            }
            //return smtpClient.SendMailAsync(mail);
        }
    }
}