using System.Net;
using System.Net.Mail;
using Ngs.Common.Notify.Exceptions;
using Ngs.Common.Notify.Models;

namespace Ngs.Common.Notify;

/// <summary>
/// Service for sending emails
/// </summary>
public class SmtpService
{
    /// <summary>
    /// The configuration for the SMTP service
    /// </summary>
    private SmtpConfiguration SmtpConfiguration { get; }

    /// <summary>
    /// Initialises a new instance of the <see cref="SmtpService"/> class
    /// </summary>
    /// <param name="smtpConfiguration"> The configuration for the SMTP service </param>
    public SmtpService(SmtpConfiguration smtpConfiguration)
    {
        SmtpConfiguration = smtpConfiguration;
    }

    /// <summary>
    /// Sends an email
    /// </summary>
    /// <param name="mailModel"> The email to send </param>
    /// <exception cref="NotifySmtpReceiverException"> Thrown when no receiver is specified </exception>
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

    /// <summary>
    /// Sends an email asynchronously
    /// </summary>
    /// <param name="mailModel"> The email to send </param>
    /// <exception cref="NotifySmtpReceiverException"> Thrown when no receiver is specified </exception>
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