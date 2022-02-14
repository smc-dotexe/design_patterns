using System.Text;

public class CodeBuilder
{
    public string ClassName;
    private const int indentSize = 2;

    public List<Property> PropertyList = new List<Property>();
    public CodeBuilder(string className)
    {
        ClassName = className;
    }

    public CodeBuilder AddField(string propName, string propType)
    {
        var property = new Property(propName, propType);
        PropertyList.Add(property);
        return this;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"public class {ClassName}");
        sb.AppendLine("{");

        foreach (var prop in PropertyList)
        {
            sb.Append(' ', indentSize);
            sb.AppendLine($"public {prop.PropertyType} {prop.PropertyName}");
        }

        sb.AppendLine("}");

        return sb.ToString(); 
    }
}

public class Property
{
    public string PropertyName { get; set; }
    public string PropertyType { get; set; }

    public Property(string propertyname, string propertyType)
    {
        PropertyName = propertyname;
        PropertyType = propertyType;
    }
}

public class CodingExercise
{
    static void Main(string[] args)
    {
        var cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
        Console.WriteLine(cb.ToString());
    }
}