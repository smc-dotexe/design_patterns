// See https://aka.ms/new-console-template for more information
using BuilderPattern;
using System.Text;

// Builder Pattern:
// Purpose: Some objects are simple and can be created in a single constructor call.
// Other objects require a lot of ceremony to create.
// Having an object with 10 constructor arguements is not efficient
// Instead, opt for 'piecewise' construction
// Builder provides an API for constructing an object step-by-step

HtmlBuilder builder = new("ul");

// Chaining methods like this is called a "Fluent Interface": An interface that allows you to chain
// several calls by returning the object you're working with (Htmlbuilder).
// See the HtmlBuilder.AddChild method. It returns 'this'.
builder.AddChild("li", "Hello").AddChild("li", "World");

Console.Write(builder);

