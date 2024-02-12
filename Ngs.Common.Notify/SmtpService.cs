using System.Net;
using System.Net.Mail;
using Net.Glow.Studios.Common.Notify.Exceptions;
using Net.Glow.Studios.Common.Notify.Models;

namespace Net.Glow.Studios.Common.Notify;

public class SmtpService
{
    private SmtpConfiguration SmtpConfiguration { get; }

    public SmtpService(SmtpConfiguration smtpConfiguration)
    {
        SmtpConfiguration = smtpConfiguration;
    }

    public void SendMail(MailModel mailModel)
    {
        using var mailMessage = new MailMessage();
        mailMessage.Subject = mailModel.Subject;
        mailMessage.Body = mailModel.Content;
        mailMessage.IsBodyHtml = mailModel.IsBodyHtml;
        mailMessage.From = new MailAddress(SmtpConfiguration.Email);

        if (!mailModel.To.Any())
        {
            throw new NotifySmtpReceiverException();
        }
        
        mailModel.To.ToList().ForEach(x => mailMessage.To.Add(x));
        mailModel.Attachments.ToList().ForEach(x => mailMessage.Attachments.Add(x));
        mailModel.Cc.ToList().ForEach(x => mailMessage.CC.Add(x));

        try
        {
            using var smtp = new SmtpClient(SmtpConfiguration.Host, SmtpConfiguration.Port);
            smtp.Credentials = new NetworkCredential(SmtpConfiguration.Email, SmtpConfiguration.Token);
            smtp.EnableSsl = SmtpConfiguration.EnableSsl;
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

    public async Task SendMailAsync(MailModel mailModel)
    {
        using var mailMessage = new MailMessage();
        mailMessage.Subject = mailModel.Subject;
        mailMessage.Body = mailModel.Content;
        mailMessage.IsBodyHtml = mailModel.IsBodyHtml;
        mailMessage.From = new MailAddress(SmtpConfiguration.Email);

        if (!mailModel.To.Any() || mailModel.To.Any(string.IsNullOrWhiteSpace))
        {
            throw new NotifySmtpReceiverException();
        }
        
        mailModel.To.ToList().ForEach(x => mailMessage.To.Add(x));
        mailModel.Attachments.ToList().ForEach(x => mailMessage.Attachments.Add(x));
        mailModel.Cc.ToList().ForEach(x => mailMessage.CC.Add(x));

        using var smtp = new SmtpClient(SmtpConfiguration.Host, SmtpConfiguration.Port);
        smtp.Credentials = new NetworkCredential(SmtpConfiguration.Email, SmtpConfiguration.Token);
        smtp.EnableSsl = SmtpConfiguration.EnableSsl;
        await smtp.SendMailAsync(mailMessage);
    }
}