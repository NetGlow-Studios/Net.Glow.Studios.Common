using System.Net.Mail;

namespace Ngs.Common.AspNetCore.Notify.Models;

/// <summary>
/// Mail model
/// </summary>
public sealed class MailModel
{
    /// <summary>
    /// Mail subject
    /// </summary>
    public required string Subject { get; init; }
    
    /// <summary>
    /// Mail content
    /// </summary>
    public required string Content { get; init; }
    
    /// <summary>
    /// Main recipients
    /// </summary>
    public required IEnumerable<string> To { get; init; }
    
    /// <summary>
    /// Is body html
    /// </summary>
    public bool IsBodyHtml { get; init; }
    
    /// <summary>
    /// Mail attachments
    /// </summary>
    public IEnumerable<System.Net.Mail.Attachment> Attachments { get; init; }
    
    /// <summary>
    /// Mail cc
    /// </summary>
    public IEnumerable<string> Cc { get; init; }
    
    public MailModel()
    {
        Attachments = new List<Attachment>();
        Cc = new List<string>();
        To = new List<string>();
    }
}