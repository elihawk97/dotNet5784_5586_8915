
using System.Security;

partial class Program
{
    private static void Main(string[] args)
    {
        Welcome5586();
    }

    private static void Welcome5586()
    {
        Console.WriteLine("Enter your name: ");
        string name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first console application", name);
    }

    static partial void Welcome8915();
}