using System.Net.Mail;

namespace Net.Glow.Studios.Common.Notify.Models;

public sealed class Mail
{
    public required string Subject { get; init; }
    public required string Content { get; init; }
    public required ICollection<string> To { get; init; }
    public bool IsBodyHtml { get; init; }
    
    public ICollection<System.Net.Mail.Attachment> Attachments { get; init; }
    public ICollection<string> Cc { get; init; }
    
    public Mail()
    {
        Attachments = new List<Attachment>();
        Cc = new List<string>();
        To = new List<string>();
    }
}