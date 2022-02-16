﻿// The use of an abstract factory is to give out ABSTRACT objects
// as opposed to concrete objects.

// In an abstract setting, you're not returning the types you are creating.
// You're returning abstract classes or interfaces

public interface IHotDrink
{
    void Consume();
}

internal class Tea : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This Tea is nice but I'd prefer it with milk");
    }
}

internal class Coffee : IHotDrink
{
    public void Consume()
    {
        Console.WriteLine("This coffee is sensational");
    }
}

public interface IHotDrinkFactory
{
    IHotDrink Prepare(int amount);
}

internal class TeaFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
        return new Tea();
    }
}

internal class CoffeeFactory : IHotDrinkFactory
{
    public IHotDrink Prepare(int amount)
    {
        Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
        return new Coffee();
    }
}

public class HotDrinkMachine
{
    //public enum AvailableDrink
    //{
    //    Coffee, Tea
    //}

    //private Dictionary<AvailableDrink, IHotDrinkFactory> factories = 
    //    new Dictionary<AvailableDrink,IHotDrinkFactory>();

    //public HotDrinkMachine()
    //{
    //    foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
    //    {
    //        var factory = (IHotDrinkFactory)Activator.CreateInstance(
    //            Type.GetType(Enum.GetName(typeof(AvailableDrink), drink) + " Factory")
    //        );
    //        factories.Add(drink, factory);
    //    }
    //}

    //public IHotDrink MakeDrink(AvailableDrink drink, int amount)
    //{
    //    return factories[drink].Prepare(amount);
    //}
    private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
    public HotDrinkMachine()
    {
        foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
        {
            if (typeof(IHotDrinkFactory).IsAssignableFrom(t) &&
                !t.IsInterface)
            {
#pragma warning disable CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
                factories.Add(Tuple.Create(
                    t.Name.Replace("Factory", string.Empty),
                    (IHotDrinkFactory)Activator.CreateInstance(t)
                    ));
#pragma warning restore CS8620 // Argument cannot be used for parameter due to differences in the nullability of reference types.
            }
        }
    }

    public IHotDrink MakeDrink()
    {
        Console.WriteLine("Available drinks:");
        for (var index = 0; index < factories.Count; index++)
        {
            var tuple = factories[index];
            Console.WriteLine($"{index}: {tuple.Item1}");
        }

        return null;
    }
}

public class AbstractFactory
{
    static void Main(string[] args)
    {
        var machine = new HotDrinkMachine();
        var drink = machine.MakeDrink();
    }
}
