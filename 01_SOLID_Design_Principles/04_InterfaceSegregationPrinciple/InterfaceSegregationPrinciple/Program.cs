using System;

namespace InterfaceSegregationPrinciple
{
    // Interface Segregation Principle - States that no code should be forced to
    // depend on methods it does not use. ISP splits interfaces that are very large
    // into smaller and more specific ones so that clients will only have to know about
    // the methods that are of interest to them. Shruken interfaces are aka "role interfaces"
    // https://en.wikipedia.org/wiki/Interface_segregation_principle
    class Program
    {
        public class Document
        {

        }

        public interface IMachine
        {
            void Print(Document d);
            void Scan(Document d);
            void Fax(Document d);
        }

        public class MultiFunctionPrinter : IMachine
        {
            // This class can use all the methods in the interface
            // No problems here
            public void Fax(Document d)
            {
                //
            }

            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        public class OldFashionedPrinter : IMachine
        {
            // This class does not need all the methods from the interface
            public void Fax(Document d)
            {
                // this printer cannot fax
            }

            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                // this printer cannot scan
            }
        }

        // ======================================================
        // Implementing the Interface Segregation Principle
        // ======================================================

        public interface IPrinter
        {
            void Print(Document d);
        }

        public interface IScanner
        {
            void Scan(Document d);
        }

        public interface IFax
        {
            void Fax(Document d);
        }

        public class PhotoCopier : IPrinter, IScanner
        {
            public void Print(Document d)
            {
                //
            }

            public void Scan(Document d)
            {
                //
            }
        }

        // If you want an higher level interface:
        public interface IMultiFunctionDevice : IScanner, IPrinter //...
        {

        }

        public class MultiFunctionMachine : IMultiFunctionDevice
        {
            private IPrinter printer;
            private IScanner scanner;

            public MultiFunctionMachine(IPrinter printer, IScanner scanner)
            { 
                this.printer = printer;
                this.scanner = scanner;
            }

            // Decorator pattern: delegating calls to the inner printer and scanner variables.
            public void Print(Document d)
            {
                printer.Print(d);
            }

            public void Scan(Document d)
            {
                scanner.Scan(d);
            }
        }

        static void Main(string[] args)
        {
        }
    }
}
