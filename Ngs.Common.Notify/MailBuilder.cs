using System.Net.Mail;
using System.Net.Mime;
using Net.Glow.Studios.Common.Notify.Models;

namespace Net.Glow.Studios.Common.Notify;

public class MailBuilder
{
    private string Subject { get; }
    private bool IsBodyHtml { get; set; }

    private ICollection<string> To { get; }
    private ICollection<string> Cc { get; }
    private ICollection<Attachment> Attachments { get; }
    
    private MailBodyBuilder MailBodyBuilder { get; set; }

    public MailBuilder(string subject)
    {
        Subject = subject;
        To = new List<string>();
        Cc = new List<string>();
        Attachments = new List<Attachment>();
        MailBodyBuilder = new MailBodyBuilder(subject);
    }

    public void AddAttachment(Stream stream, ContentType? contentType = default) 
        => Attachments.Add(new Attachment(stream, contentType ?? new ContentType()));
    
    public void AddAttachment(string fileName, ContentType? contentType = default) 
        => Attachments.Add(new Attachment(fileName, contentType ?? new ContentType()));

    public void AddAttachment(Attachment attachment) => Attachments.Add(attachment);

    public void AddAttachment(IEnumerable<Attachment> attachments) => Attachments.ToList().AddRange(attachments);

    public void AddTo(string email) => To.Add(email);
    public void AddTo(IEnumerable<string> emails) => To.ToList().AddRange(emails);

    public void AddCc(string email) => Cc.Add(email);
    public void AddCc(IEnumerable<string> emails) => Cc.ToList().AddRange(emails);

    public void AddBody(string body, bool isBodyHtml = false)
    {
        MailBodyBuilder = new MailBodyBuilder(body);
        IsBodyHtml = isBodyHtml;
    }

    public void AddBody(MailBodyBuilder mailBodyBuilder)
    {
        MailBodyBuilder = mailBodyBuilder;
        IsBodyHtml = true;
    }

    public string GetSubject() => Subject;
    
    public IReadOnlyCollection<string> GetTo() => To.ToList();
    public IReadOnlyCollection<string> GetCc() => Cc.ToList();
    public IReadOnlyCollection<Attachment> GetAttachments() => Attachments.ToList();

    public Mail Build()
    {
        return new Mail
        {
            Subject = Subject,
            Content = MailBodyBuilder.ToString(),
            To = To,
            Cc = Cc,
            Attachments = Attachments,
            IsBodyHtml = IsBodyHtml
        };
    }
}