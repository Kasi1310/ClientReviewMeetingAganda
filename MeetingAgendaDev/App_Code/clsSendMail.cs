using System;
using System.Net.Mail;

namespace ClientMeetingAgenda.App_Code
{
    public class clsSendMail
    {
        public bool SendMail(string To, string CC, string BCC, string Subject, string Body, string Attachement)
        {
            try
            {
                //To = "arengasamy@medicount.com";
                To = "vanithac@medicount.com";
                CC = "";
                BCC = "";
                //BCC = "arengasamy@medicount.com";
                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress("meetingagenda@medicount.com");

                    mailMessage.Subject = Subject;
                    mailMessage.Body = Body;
                    mailMessage.IsBodyHtml = true;

                    foreach (var ToMailID in To.Split(','))
                    {
                        mailMessage.To.Add(new MailAddress(ToMailID));
                    }

                    if (CC != "")
                    {
                        foreach (var CCMailID in CC.Split(','))
                        {
                            mailMessage.CC.Add(new MailAddress(CCMailID));
                        }
                    }
                    if (BCC != "")
                    {
                        foreach (var BCCMailID in BCC.Split(','))
                        {
                            mailMessage.Bcc.Add(new MailAddress(BCCMailID));
                        }
                    }
                    if (Attachement != "")
                    {
                        mailMessage.Attachments.Add(new Attachment(Attachement));
                    }

                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = "smtp.office365.com";
                    System.Net.NetworkCredential NetworkCred = new System.Net.NetworkCredential();
                    NetworkCred.UserName = "meetingagenda@medicount.com";
                    NetworkCred.Password = "**64=Noise=Short=Bicycle=99**";
                    smtp.UseDefaultCredentials = false;
                    smtp.Credentials = NetworkCred;
                    smtp.EnableSsl = true;
                    smtp.Port = 587;
                    smtp.Send(mailMessage);

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}