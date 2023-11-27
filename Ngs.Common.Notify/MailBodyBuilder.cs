using HtmlAgilityPack;

namespace Net.Glow.Studios.Common.Notify;

public class MailBodyBuilder
{
    private HtmlDocument HtmlDocument { get; }

    public MailBodyBuilder(string title)
    {
        HtmlDocument = new HtmlDocument();
        HtmlDocument.Load(File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), "Templates", "htmlTemplate.html")));
        HtmlDocument.DocumentNode.SelectSingleNode("//title").InnerHtml = title;
    }

    public MailBodyBuilder(string title, string body) : this(title)
    {
    }
    
    public override string ToString() => HtmlDocument.DocumentNode.InnerHtml;

    public void AddClass(string className, IEnumerable<string> styles)
    {
        var stylesNode = HtmlDocument.DocumentNode.SelectSingleNode("//styles") ?? HtmlNode.CreateNode("<style></style>");
        
        var style = string.Empty;
        styles.ToList().ForEach(x => style += $"{x};");

        stylesNode.InnerHtml += $".{className}{{{style}}}";
    }

    #region Headers

    public void AddH1(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "h1", classes, styles));
    public void AddH2(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "h2", classes, styles));
    public void AddH3(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "h3", classes, styles));
    public void AddH4(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "h4", classes, styles));
    public void AddH5(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "h5", classes, styles));

    #endregion

    public void AddP(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "p", classes, styles));
    public void AddI(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "i", classes, styles));
    public void AddB(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "b", classes, styles));
    public void AddU(string text, string classes = "", params string[] styles) => AppendNode(CreateNode(text, "u", classes, styles));

    public void AddLink(string text, string link, string classes = "", params string[] styles)
    {
        var node = CreateNode(text, "a", classes, styles);
        
        node.Attributes.Add("href", link);

        AppendNode(node);
    }

    public void AppendNode(HtmlNode node)
    {
        var body = HtmlDocument.DocumentNode.SelectSingleNode("//body");

        body?.AppendChild(node);
    }
    
    private static HtmlNode CreateNode(string text, string nodeText, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode($"<{nodeText}></{nodeText}>");

        node.InnerHtml = text;

        classes.Split(' ').ToList().ForEach(x => node.AddClass(x));

        if (!styles.Any()) return node;

        var style = string.Empty;
        styles.ToList().ForEach(x => style += $"{x};");
        node.Attributes.Add("style", style);

        return node;
    }
}