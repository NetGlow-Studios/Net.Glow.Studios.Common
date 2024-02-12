namespace Net.Glow.Studios.Common.Notify.Models;

public class SmsModel(string message, ICollection<string> to)
{
    public string Message { get; set; } = message;
    public ICollection<string> To { get; set; } = to;

    public SmsModel() : this(string.Empty, new List<string>())
    {
    }
}