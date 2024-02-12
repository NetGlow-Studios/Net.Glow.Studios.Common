using System.Net.Mail;

namespace Net.Glow.Studios.Common.Notify.Models;

public sealed class MailModel
{
    public required string Subject { get; init; }
    public required string Content { get; init; }
    public required IEnumerable<string> To { get; init; }
    public bool IsBodyHtml { get; init; }
    
    public IEnumerable<System.Net.Mail.Attachment> Attachments { get; init; }
    public IEnumerable<string> Cc { get; init; }
    
    public MailModel()
    {
        Attachments = new List<Attachment>();
        Cc = new List<string>();
        To = new List<string>();
    }
}