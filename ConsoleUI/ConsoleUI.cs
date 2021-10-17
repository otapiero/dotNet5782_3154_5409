using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            DAL.Foo c1 = new();
            Console.WriteLine( c1.k);
        }
    }
}
