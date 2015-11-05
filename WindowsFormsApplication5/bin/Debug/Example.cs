using System;
class Example
{
    public static void Main(String[] args)
    {
        float b=0, c=0, d=0;
        
        b=Convert.ToSingle(Console.ReadLine());
        c = Convert.ToSingle(Console.ReadLine());
        d = c + b;
        Console.WriteLine("Sum is " + d);
        d = b - c;
        Console.WriteLine("Difference is " + d);
        d = b * c;
        Console.WriteLine("Product is " + d);
        d = b / c;
        Console.WriteLine("Remainder is " + d);
	Console.ReadLine();
    }
}