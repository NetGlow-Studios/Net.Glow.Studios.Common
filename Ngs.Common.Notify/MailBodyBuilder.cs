using HtmlAgilityPack;

namespace Net.Glow.Studios.Common.Notify;

public class MailBodyBuilder
{
    private HtmlDocument HtmlDocument { get; }
    
    public MailBodyBuilder()
    {
        HtmlDocument = new HtmlDocument();
        HtmlDocument.Load(File.OpenText(Path.Combine(Directory.GetCurrentDirectory(), "Templates","htmlTemplate.html")));
    }

    public MailBodyBuilder(string body) : this()
    {
    }

    public void AddClass(string className, params string[] styles)
    {
        var stylesNode = HtmlDocument.DocumentNode.SelectSingleNode("//styles") ?? HtmlNode.CreateNode("style");
    }
    
    #region Headers
    public void AddH1(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<h1></h1>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }

    public void AddH2(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<h2></h2>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }

    public void AddH3(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<h3></h3>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }
    
    public void AddH4(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<h4></h4>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }
    
    public void AddH5(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<h5></h5>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }
    
    #endregion
    
    public void AddP(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<p></p>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }

    public void AddI(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<i></i>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }

    public void AddB(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<b></b>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }

    public void AddU(string text, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<u></u>");

        node.InnerHtml = text;
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }

    public void AddLink(string text, string link, string classes = "", params string[] styles)
    {
        var node = HtmlNode.CreateNode("<a></a>");

        node.InnerHtml = text;
        node.Attributes.Add("href", link);
        
        classes.Split(' ').ToList().ForEach(x=>node.AddClass(x));

        if (styles.Any())
        {
            var style = string.Empty;
            styles.ToList().ForEach(x=>style += $"{x};");
            node.Attributes.Add("style", style);   
        }
        
        AddNode(node);
    }
    
    public void AddNode(HtmlNode node)
    {
        var body = HtmlDocument.DocumentNode.SelectSingleNode("//body");

        body?.AppendChild(node);
    }
    
    public override string ToString() => HtmlDocument.DocumentNode.InnerHtml;
}