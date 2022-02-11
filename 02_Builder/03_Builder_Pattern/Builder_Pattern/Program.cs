using System.Text;

public class HtmlElement
{
    public string? Name, Text;
    public List<HtmlElement> Elements = new List<HtmlElement>();
    private const int indentSize = 2;

    public HtmlElement() { }

    public HtmlElement(string name, string text)
    {
        Name = name == null ? null : name;
        Text = text == null ? null : text;
    }

    private string ToStringImpl(int indent)
    {
        var sb = new StringBuilder();
        var i = new string(' ', indentSize * indent);
        sb.AppendLine($"{i}<{Name}>");

        if (!string.IsNullOrWhiteSpace(Text))
        {
            sb.Append(new string(' ', indentSize * (indent) + 1));
            sb.AppendLine(Text);
        }

        foreach (var e in Elements)
        {
            sb.Append(e.ToStringImpl(indent + 1));
        }

        sb.AppendLine($"{i}</{Name}>");
        return sb.ToString();
    }
    public override string ToString()
    {
        return ToStringImpl(0);
    }
}

public class HtmlBuilder
{
    private readonly string rootName;
    HtmlElement root = new HtmlElement();

    public HtmlBuilder(string rootName)
    {
        this.rootName = rootName;
        root.Name = rootName;
    }

    // returning 'this' as HtmlBuilder gives us the ability to chain together
    // multiple 'AddChild' calls as seen on line 78 (also called a 'Fluent' Builder)
    public HtmlBuilder AddChild(string childName, string childText)
    {
        var e = new HtmlElement(childName, childText);
        root.Elements.Add(e);
        return this;
    }

    public override string ToString()
    {
        return root.ToString();
    }

    public void Clear()
    {
        root = new HtmlElement() { Name = rootName };
    }
}
class BuilderPattern
{
    static void Main(string[] args)
    {
        var builder = new HtmlBuilder("ul");
        builder.AddChild("li", "Hello").AddChild("li", "World");
        Console.WriteLine(builder.ToString());
    }
}
