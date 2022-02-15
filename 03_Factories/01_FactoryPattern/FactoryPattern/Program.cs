// One of the reasons factories exist is because constructors are not that good
// Example:

public class Point
{
    private double x, y;

    //public Point(double x, double y)
    //{
    //    this.x = x;
    //    this.y = y;
    //}

    // you cannot over load the constructor with the same exact constructor types:
    //public Point(double rho, double theta)
    // doing this you have to add cases to the constructor (if this is passed in then
    // do this), hence overloading. Factories help solve this problem:

    // it turns the constructor private:
    private Point(double x, double y)
    {
        this.x = x;
        this.y = y;
    }

    // now you can create methods that invoke the correct constructor
    // with a 'factory method' design pattern
    public static Point NewCartesianPoint(double x, double y)
    {
        return new Point(x, y);
    }

    public static Point NewPolarPoint(double rho, double theta)
    {
        return new Point(rho*Math.Cos(theta), rho*Math.Sin(theta));
    }

    public override string ToString()
    {
        return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
    }
}

public class FactoryPattern
{
    static void Main(string[] args)
    {
        var point = Point.NewPolarPoint(1.0, Math.PI / 2);
        Console.WriteLine(point);
    }
}

// Summary:
// Advantages of the Factory Method: 
// 1. You get to have an overload with the same sets of arguments.
// 2. The names of the factory methods are also unique in the sense
// we can make it clear to the user using the api what type of parameters to pass in