using System;

namespace LiskovSubstitutionPrinciple
{
    // Liskov Substitution Principle - Objects of a superclass shall be
    // replaceable with objects of its subclasses without breaking the application.
    // Requires the objects of the subclasses to behave in the same way as the objects 
    // of your superclass. 
    // https://stackify.com/solid-design-liskov-substitution-principle/#:~:text=The%20Liskov%20Substitution%20Principle%20in,the%20objects%20of%20your%20superclass.
    class Program
    {
        static public int Area(Rectangle r) => r.Width * r.Height;
        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(3, 4);
            Console.WriteLine($"{rc} has area of {Area(rc)}");

            Rectangle sq = new Square();
            sq.Width = 4;

            Console.WriteLine($"{sq} has area of {Area(sq)}");
        }
    }

    public class Rectangle
    {
        // Making the properties virtual helps sustain the Liskov Principle
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle() { }
        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        // =================================================================================
            //public new int Width
            //{ set { base.Width = base.Height = value; } }

            //public new int Height
            //{ set { base.Width = base.Height = value; } }

            // This violates the Liskov principle.
            // It should be allowed that we can substitute the Square class for Rectangle
            // like so:
            // Rectangle sq = new Square(); 
            // sq.Width = 4;
            // But doing so will leave the Height property as 0.
            // The solution below is to use "virtual" properties on the base class and instead
            // of using the "new" keyword in this subclass, use override. 
        // =================================================================================
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
}
