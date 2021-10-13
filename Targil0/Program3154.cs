using System;

namespace Targil0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3154();
            Wellcome5409();
            Console.ReadKey();
        }
        static partial void Wellcome5409();
        private static void Welcome3154()
        {
            Console.WriteLine("Enter your name: ");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", name);
        }
    }
}
