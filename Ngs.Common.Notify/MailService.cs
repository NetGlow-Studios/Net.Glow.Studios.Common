using System.Net;
using System.Net.Mail;
using Net.Glow.Studios.Common.Notify.Models;

namespace Net.Glow.Studios.Common.Notify;

public class MailService
{
    private MailConfiguration MailConfiguration { get; }

    public MailService(MailConfiguration mailConfiguration)
    {
        MailConfiguration = mailConfiguration;
    }

    public void SendMail(Mail mail)
    {
        using var mailMessage = new MailMessage();
        mailMessage.Subject = mail.Subject;
        mailMessage.Body = mail.Content;
        mailMessage.IsBodyHtml = mail.IsBodyHtml;
        mailMessage.From = new MailAddress(MailConfiguration.Email);
    
        mail.To.ToList().ForEach(x => mailMessage.To.Add(x));
        mail.Attachments.ToList().ForEach(x => mailMessage.Attachments.Add(x));
        mail.Cc.ToList().ForEach(x => mailMessage.CC.Add(x));

        try
        {
            using var smtp = new SmtpClient(MailConfiguration.Host, MailConfiguration.Port);
            smtp.Credentials = new NetworkCredential(MailConfiguration.Email, MailConfiguration.Token);
            smtp.EnableSsl = MailConfiguration.EnableSsl;
            smtp.Send(mailMessage);
        }
        catch (SmtpException e)
        {
            Console.WriteLine(e.Message);

            if (e.Message.Contains("Authentication Required"))
            {
                return;
            }
                
            if (e.InnerException?.Message == "No such host is known.")
            {
            }
        }
    }

    public async Task SendMailAsync(Mail mail)
    {
        using var mailMessage = new MailMessage();
        mailMessage.Subject = mail.Subject;
        mailMessage.Body = mail.Content;
        mailMessage.IsBodyHtml = mail.IsBodyHtml;
        mailMessage.From = new MailAddress(MailConfiguration.Email);

        mail.To.ToList().ForEach(x => mailMessage.To.Add(x));
        mail.Attachments.ToList().ForEach(x => mailMessage.Attachments.Add(x));
        mail.Cc.ToList().ForEach(x => mailMessage.CC.Add(x));

        try
        {
            using var smtp = new SmtpClient(MailConfiguration.Host, MailConfiguration.Port);
            smtp.Credentials = new NetworkCredential(MailConfiguration.Email, MailConfiguration.Token);
            smtp.EnableSsl = MailConfiguration.EnableSsl;
            await smtp.SendMailAsync(mailMessage);
        }
        catch (SmtpException e)
        {
            Console.WriteLine(e.Message);

            if (e.Message.Contains("Authentication Required"))
            {
                return;
            }
                
            if (e.InnerException?.Message == "No such host is known.")
            {
            }
        }
    }
}