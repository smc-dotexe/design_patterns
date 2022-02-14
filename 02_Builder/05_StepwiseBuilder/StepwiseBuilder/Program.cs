// To use a stepwise builder, we have to construct it in a certain order.
// First have to specify the CarType, then specify the WheelSize.
// We are going to return one interface for configuring a CarType.
// Then another interface for WheelSize.
// Then a 3rd interface for building a car.
public enum CarType
{
    Sedan, Crossover
}

public class Car
{
    public CarType Type;
    public int WheelSize;
}

public interface ISpecifyCarType
{
    ISpecifyWheelSize OfType(CarType type);
}

public interface ISpecifyWheelSize 
{
    IBuildCar WithWheels(int size);
}

public interface IBuildCar 
{
    public Car Build();
}

public class CarBuilder
{
    private class Impl :
        ISpecifyCarType,
        ISpecifyWheelSize,
        IBuildCar
    {
        private Car car = new Car();
        public ISpecifyWheelSize OfType(CarType type)
        {
            car.Type = type;
            return this; // returning type of ISpecifyWheelSize
        }

        public IBuildCar WithWheels(int size)
        {
            // validation
            switch (car.Type)
            {
                case CarType.Crossover when size < 17 || size > 20:
                case CarType.Sedan when size < 15 || size > 17:
                    throw new ArgumentException($"Wrong size of wheel for {car.Type}");
            }

            car.WheelSize = size;
            return this;
        }
        public Car Build()
        {
            return car;
        }
    }
    public static ISpecifyCarType Create()
    {
        return new Impl();
    }
}

public class StepwiseCarBuilder
{
    static void Main(string[] args)
    {
        var car = CarBuilder.Create()
            .OfType(CarType.Crossover) // ISpecifyCarType
            .WithWheels(19)            // ISpecifyWheelSize
            .Build();                  // IBuildCar       
    }
}


