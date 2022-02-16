
public class Point
{
    private double x, y;

    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }

    // Unrelated note. You can have Factory Properties instead of Factory methods.
    
    //public static Point Origin => new Point(0, 0); // "=>" Makes this is a property
    // Use a property if you need to instantiate a NEW object any time it is being asked for something.
    //public static Point Origin2 = new Point(0, 0); // "=" makes this a field.
    // Use a Field if you want to initialize the field once and then it is available
    // to use anywhere you choose 
    
    // Properties and factories are similar in that they are both just a combination of methods

    // Inner factory for if you want to keep the constructor private
    public static class Factory
    {
        public static Point NewCartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point NewPolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }
    }
}

public class InnerFactory
{
    static void Main(string[] args)
    {
        var point = Point.Factory.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);

        //var origin = Point.Origin; // public static Point Origin => new Point(0, 0);

    }
}
