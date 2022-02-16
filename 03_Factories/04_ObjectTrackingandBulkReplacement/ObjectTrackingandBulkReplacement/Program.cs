using System.Text;

public interface ITheme
{
    string TextColor { get; }
    string BgrColor { get; }
}


class LightTheme : ITheme
{
    public string TextColor => "black";
    public string BgrColor => "white";
}

class DarkTheme : ITheme
{
    public string TextColor => "white";

    public string BgrColor => "dark grey";
}

public class TrackingThemeFactory
{
    private readonly List<WeakReference<ITheme>> themes = new();
    public ITheme CreateTheme(bool dark)
    {
        ITheme theme = dark ? new DarkTheme() : new LightTheme();
        themes.Add(new WeakReference<ITheme>(theme));
        return theme;
    }

    public string Info
    {
        get
        {
            var sb = new StringBuilder();
            foreach (var reference in themes)
            {
                if (reference.TryGetTarget(out var theme))
                {
                    bool dark = theme is DarkTheme;
                    sb.Append(dark ? "Dark" : "Light")
                        .AppendLine(" theme");
                }
            }
            return sb.ToString();
        }
    }
}

public class ReplaceableThemeFactory
{
    private readonly List<WeakReference<Ref<ITheme>>> themes = new();

    private ITheme createThemeImpl(bool dark)
    {
        return dark ? new DarkTheme() : new LightTheme();
    }

    public Ref<ITheme> CreateTheme(bool dark)
    {
        var r = new Ref<ITheme>(createThemeImpl(dark));
        themes.Add(new(r));
        return r;
    }

    // Bulk Replacement
    public void ReplaceTheme(bool dark)
    {
        foreach(var wr in themes)
        {

            if (wr.TryGetTarget(out var reference))
            {
                // going through each of the Refs that have been given out from the factory
                // and changing it's value.
                reference.Value = createThemeImpl(dark);
            }
        }
    }
}

// acts as a wrapper to be able to work with tracking and bulk replacement
public class Ref<T> where T : class
{
    public T Value;

    public Ref(T value)
    {
        Value = value;
    }
}

public class Program
{
    static void Main(string[] args)
    {
        var factory = new TrackingThemeFactory();
        var theme1 = factory.CreateTheme(false);
        var theme2 = factory.CreateTheme(true);
        Console.WriteLine(factory.Info);

        var factory2 = new ReplaceableThemeFactory();
        var magicTheme = factory2.CreateTheme(true);
        Console.WriteLine(magicTheme.Value.BgrColor);
        factory2.ReplaceTheme(false);
        Console.WriteLine(magicTheme.Value.BgrColor);
    }
}