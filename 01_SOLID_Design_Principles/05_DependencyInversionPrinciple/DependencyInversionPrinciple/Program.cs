using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversionPrinciple
{
    // DIP - High-level modules should not import anything from low-level modules.
    // Both should depend on abstractions.
    // Abstractions should not depend on details. Details (concrete implementations)
    // should depend on abstractions.
    // https://en.wikipedia.org/wiki/Dependency_inversion_principle

    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name;
    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // Low-level
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations
            = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }
        // ====================================================================
        // Violating DIP here as we allow access to a concrete implementation
        // of a private member.
        //public List<(Person, Relationship, Person)> Relations => relations;
        // ====================================================================


        // Solution. We now depend on an abstraction
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            foreach (var r in relations.Where(
                x => x.Item1.Name == name &&
                x.Item2 == Relationship.Parent
            ))
            {
                yield return r.Item3;
            }
        }

    }

    // High-level
    public class Research
    {
        // ====================================================================
        // This violates the DIP. We are accessing low level information in a high level
        // module. This dependency disallows the Relationships class to be able to change
        // if needed (for example if we wanted to change from using Tuples to a Dictionary).
        // The solution is to provide an abstraction i.e. an Interface (IRelationshipBrowser).

        //public Research(Relationships relationships)
        //{
        //    var relations = relationships.Relations;

        //    foreach (var r in relations.Where(
        //        x => x.Item1.Name == "John" &&
        //        x.Item2 == Relationship.Parent
        //    ))
        //    {
        //        Console.WriteLine($"John has a child called {r.Item3.Name}");
        //    }
        //}

        // ====================================================================

        // ====================================================================
        // Solution. Now we don't depend on "Relationship" class. Instead now
        // we use an abstraction "IRelationshipBrowser".
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
                Console.WriteLine($"John has a child called {p.Name}");
        }
        // ====================================================================

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };

            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);

            new Research(relationships);
        }
    }
}
