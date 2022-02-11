// When builders inherit from other builders, it's only problematic if you use the 
// Fluent Interface approach. No easy way to mitigate the inheritance of fluent interfaces.
// Using "Recursive generics" can get fluent interfaces to inherit and return the proper type.

// To summarize: 
// Fluent builders/interfaces return their own type (return this).
// You're able to chain together multiple calls this way (builder.append("something").append("somethingelse"))
// It gets complicated when you start inheriting the class that is 'Fluent'.
// If you use the derived class to call a method from the Fluent Parent class, it will return the TYPE of the FLUENT PARENT instead
// of the child class where you called it from. Therefore, if you THEN try to call a method that's in the child class, FROM the child class, 
// it won't be recognzied and throw an error (because since it returned as a type of the parent class, the parent class has no idea of the methods or props
// in the child class).

public class Person
{
    public string Name;
    public string Position;

    // To start the generic class, otherwise we wouldn't know what to pass in first (new PersonJobBuilder<???>)
    public class Builder : PersonJobBuilder<Builder> { }

    public static Builder New => new Builder();
    public override string ToString()
    {
        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    }

}

// start of recursive generics
public abstract class PersonBuilder
{
    protected Person person = new Person();
    public Person Build() { return person; }
}

public class PersonInfoBuilder<SELF> : PersonBuilder                                   
    where SELF : PersonInfoBuilder<SELF> // this derived class has to inherit from PersonInfoBuilder
{
    // For example -> PersonJobBuilder inherits PersonInfoBuilder ==
    // PersonInfoBuilder<PersonJobBuilder>
    // where PersonJobBuilder : PersonInfoBuilder<PersonJobBuilder>

    public SELF Called(string name)
    {
        person.Name = name;
        // the return type needs to return 'this' from the class that's inheriting THIS class (PersonInfoBuilder) - achieved through generics
        return (SELF)this;
    }
}

// Simple version to help simplify recursive generics to inherit a fluent api.
// But if we want other classes to inherit PersonJobBuilder, it gets more complicated (see class below this one)

//public class PersonJobBuilder : PersonInfoBuilder<PersonJobBuilder>
//{
//    public PersonJobBuilder WorksAsA(string position)
//    {
//        person.Position = position;
//        return this;
//    }
//}
public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>> // this can get robust if we continue this type of pattern
    where SELF : PersonJobBuilder<SELF>                                         // a list of types aka "Type List" -> well known in C++
{
    public SELF WorksAsA(string position)
    {
        person.Position = position;
        return (SELF) this;
    }
}

internal class Program
{
    public static void Main(string[] args)
    {
        //var builder = new PersonJobBuilder();
        //builder.Called("shane");
        // Calling the "Called" method returns a type of PersonInfoBuilder which doesn't know
        // anything about "PersonJobBuilder" so then calling WorksAsA() from PersonJobBuilder
        // will throw an error - Recursive generics can solve this

        var me = Person.New
            .Called("shane")
            .WorksAsA("developer")
            .Build();

        Console.WriteLine(me);
        
    }
}