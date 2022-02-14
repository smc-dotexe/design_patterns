public class Person
{
    // address
    public string StreetAddress, PostCode, City;

    // employment
    public string CompanyName, Position;
    public int AnnualIncome;

    public override string ToString()
    {
        return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(PostCode)}: {PostCode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
    }
}

public class PersonBuilder // facade (another type of design pattern)
                           // where it's a component that hides a lot of additional information
{
    // Reference!! If you were building with structs, that would be a problem
    protected Person person = new Person();

    public PersonJobBuilder Works => new PersonJobBuilder(person);
    public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

    // not too correct in a programming approach (teacher's words)
    public static implicit operator Person(PersonBuilder pb)
    {
        // allows to return the type of Person so we can console.writeline the properties
        return pb.person;
    }
}

public class PersonJobBuilder : PersonBuilder
{
    public PersonJobBuilder(Person person)
    {
        this.person = person;
    }

    public PersonJobBuilder At(string companyName)
    {
        person.CompanyName = companyName;
        return this;
    }

    public PersonJobBuilder AsA(string position)
    {
        person.Position = position;
        return this;
    }

    public PersonJobBuilder Earning(int amount)
    {
        person.AnnualIncome = amount;
        return this;
    }
}

public class PersonAddressBuilder : PersonBuilder
{
    public PersonAddressBuilder(Person person)
    {
        this.person = person;
    }

    public PersonAddressBuilder At(string streetAddress)
    {
        person.StreetAddress = streetAddress;
        return this;
    }

    public PersonAddressBuilder WithPostCode(string postCode)
    {
        person.PostCode = postCode;
        return this;
    }

    public PersonAddressBuilder In(string city)
    {
        person.City = city;
        return this;
    }
}

public class FacetedBuilder
{
    static void Main(string[] args)
    {
        var pb = new PersonBuilder();
        Person person = pb
            .Works.At("Vog")
                  .AsA("Maintenance Dev")
                  .Earning(27000)
            .Lives.At("123 dot road")
                  .WithPostCode("T2J 2X8")
                  .In("Calgary");
        Console.WriteLine(person);
    }
}