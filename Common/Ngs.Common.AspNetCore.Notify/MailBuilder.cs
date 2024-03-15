using System.Net.Mail;
using System.Net.Mime;
using Ngs.Common.AspNetCore.Notify.Models;

namespace Ngs.Common.AspNetCore.Notify;

/// <summary>
/// MailBuilder is a builder class for MailModel.
/// </summary>
public class MailBuilder
{
    /// <summary>
    /// The subject of the mail.
    /// </summary>
    private string Subject { get; }
    
    /// <summary>
    /// The body of the mail.
    /// </summary>
    private bool IsBodyHtml { get; set; }

    /// <summary>
    /// The recipients of the mail.
    /// </summary>
    private List<string> To { get; }
    
    /// <summary>
    /// The carbon copy recipients of the mail.
    /// </summary>
    private List<string> Cc { get; }
    
    /// <summary>
    /// The attachments of the mail.
    /// </summary>
    private List<Attachment> Attachments { get; }
    
    /// <summary>
    /// The mail body builder.
    /// </summary>
    private MailBodyBuilder MailBodyBuilder { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="MailBuilder"/> class.
    /// </summary>
    /// <param name="subject"> The subject of the mail. </param>
    public MailBuilder(string subject)
    {
        Subject = subject;
        To = new List<string>();
        Cc = new List<string>();
        Attachments = new List<Attachment>();
        MailBodyBuilder = new MailBodyBuilder(subject);
    }

    /// <summary>
    /// Adds an attachment to the mail.
    /// </summary>
    /// <param name="stream"> The stream of the attachment. </param>
    /// <param name="contentType"> The content type of the attachment. </param>
    public void AddAttachment(Stream stream, ContentType? contentType = default) 
        => Attachments.Add(new Attachment(stream, contentType ?? new ContentType()));
    
    /// <summary>
    /// Adds an attachment to the mail.
    /// </summary>
    /// <param name="fileName"> The file name of the attachment. </param>
    /// <param name="contentType"> The content type of the attachment. </param>
    public void AddAttachment(string fileName, ContentType? contentType = default) 
        => Attachments.Add(new Attachment(fileName, contentType ?? new ContentType()));

    /// <summary>
    /// Adds an attachment to the mail.
    /// </summary>
    /// <param name="attachment"> The attachment to add. </param>
    public void AddAttachment(Attachment attachment) => Attachments.Add(attachment);

    /// <summary>
    /// Adds attachments to the mail.
    /// </summary>
    /// <param name="attachments"> The attachments to add. </param>
    public void AddAttachment(IEnumerable<Attachment> attachments) => Attachments.ToList().AddRange(attachments);

    /// <summary>
    /// Adds a recipient to the mail.
    /// </summary>
    /// <param name="email"> The email of the recipient. </param>
    public void AddTo(string email) => To.Add(email);
    
    /// <summary>
    /// Adds recipients to the mail.
    /// </summary>
    /// <param name="emails"> The emails of the recipients. </param>
    public void AddTo(IEnumerable<string> emails) => To.ToList().AddRange(emails);

    /// <summary>
    /// Adds a carbon copy recipient to the mail.
    /// </summary>
    /// <param name="email"> The email of the carbon copy recipient. </param>
    public void AddCc(string email) => Cc.Add(email);
    
    /// <summary>
    /// Adds carbon copy recipients to the mail.
    /// </summary>
    /// <param name="emails"> The emails of the carbon copy recipients. </param>
    public void AddCc(IEnumerable<string> emails) => Cc.ToList().AddRange(emails);

    /// <summary>
    /// Adds the body of the mail.
    /// </summary>
    /// <param name="body"> The body of the mail. </param>
    /// <param name="isBodyHtml"> A value indicating whether the body is HTML. </param>
    public void AddBody(string body, bool isBodyHtml = false)
    {
        MailBodyBuilder = new MailBodyBuilder(body);
        IsBodyHtml = isBodyHtml;
    }

    /// <summary>
    /// Adds the body of the mail.
    /// </summary>
    /// <param name="mailBodyBuilder"> The mail body builder. </param>
    public void AddBody(MailBodyBuilder mailBodyBuilder)
    {
        MailBodyBuilder = mailBodyBuilder;
        IsBodyHtml = true;
    }

    /// <summary>
    /// Gets the subject of the mail.
    /// </summary>
    /// <returns> The subject of the mail. </returns>
    public string GetSubject() => Subject;
    
    /// <summary>
    /// Gets the body of the mail.
    /// </summary>
    /// <returns> The body of the mail. </returns>
    public IReadOnlyCollection<string> GetTo() => To.ToList();
    
    /// <summary>
    /// Gets the carbon copy recipients of the mail.
    /// </summary>
    /// <returns> The carbon copy recipients of the mail. </returns>
    public IReadOnlyCollection<string> GetCc() => Cc.ToList();
    
    /// <summary>
    /// Gets the attachments of the mail.
    /// </summary>
    /// <returns> The attachments of the mail. </returns>
    public IReadOnlyCollection<Attachment> GetAttachments() => Attachments.ToList();

    /// <summary>
    /// Gets build mail model.
    /// </summary>
    /// <returns> The build mail model. </returns>
    public MailModel Build()
    {
        return new MailModel
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