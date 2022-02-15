// See https://aka.ms/new-console-template for more information

// This is a classic Fluent Builder approach. There is no problem until you want to start
// inheriting PersonInfoBuilder.
//namespace FluentBuilder.Program
//{ 
    //public class Person
    //{
    //    public string Name;
    //    public string Position;

    //    public override string ToString()
    //    {
    //        return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
    //    }
    //}

    //public class PersonInfoBuilder
    //{
    //    protected Person person = new Person();

    //    public PersonInfoBuilder Called(string name)
    //    {
    //        person.Name = name;
    //        return this;
    //    }
    //}

    //public class PersonJobBuilder : PersonInfoBuilder
    //{
    //    public PersonJobBuilder WorkAsA(string position)
    //    {
    //        person.Position = position;
    //        return this;
    //    }
    //}
    //public class Program
    //{
    //    public static void Main(string[] args)
    //    {
    //        var builder = new PersonJobBuilder();
    //        builder.Called("shane");
    //        builder.WorkAsA();

            // WorkAsA shows an error because when calling ".Called("shane")" this method returns 
            // a "PersonInfoBuilder" and this class doesn't know anything about the .WorkAsA method.

            // The Question is how can a derived class propagate the information about the return type
            // to it's own base class?

            // The answer is Recursive Generics:
//        }
//    }

//}

namespace FluentBuilder
{
    public class Person
    {
        public string Name;
        public string Position;
        public class Builder : PersonJobBuilder<Builder> { }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();
        
        public Person Build() { return person; }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF : PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder<SELF> : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorkAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // PersonJobBuilder<Person.Builder> me
            var me = Person.New
                .Called("shane")
                .WorkAsA("dev")
                .Build();

            Console.WriteLine(me);
        }
    }
}

