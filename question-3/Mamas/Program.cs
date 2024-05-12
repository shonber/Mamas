using System.Dynamic;

namespace MyProject;

static class Program
{
    static void Main(string[] args)
    {   
        // *********************************
        // Node and LinkedList Classes
        // *********************************

        // LinkedList<int> ls = new();
        // Console.WriteLine($"Empty: {ls}");

        // ls.Append(3);
        // ls.Append(5);
        // ls.Append(2);
        // ls.Append(8);
        // Console.WriteLine($"After Append {ls}");

        // ls.Prepend(-23);
        // ls.Prepend(7);
        // ls.Prepend(-1);
        // ls.Prepend(-2);
        // Console.WriteLine($"After Prepend {ls}");

        // Console.WriteLine($"Pop Value {ls.Pop()}");
        // Console.WriteLine($"After Pop {ls}");

        // Console.WriteLine($"Unqueue Value {ls.Unqueue()}");
        // Console.WriteLine($"After Unqueue {ls}");

        // Console.WriteLine($"Max Value: {ls.GetMaxNode()}");
        // Console.WriteLine($"Min Value: {ls.GetMinNode()}");

        // IEnumerable<int> list = ls.ToList();
        // foreach (var value in list)
        // {
        //     Console.WriteLine($"Iterated Value: {value}");
        // }

        // bool isCircularCheck = ls.IsCircular();
        // Console.WriteLine($"The list is circular: {isCircularCheck}");

        // ls.Sort();
        // Console.WriteLine($"Sorted List: {ls}");

        // *********************************
        // NumericalExpression Class
        // *********************************

        NumericalExpression ne = new(25);
        Console.WriteLine(ne);

        NumericalExpression ne1 = new(99900000000000000);
        Console.WriteLine(ne1);

        NumericalExpression.SetMaxNumber();
        Console.WriteLine(ne1);

        NumericalExpression ne2 = new(23198398);
        Console.WriteLine(ne2);

        NumericalExpression.SelectLanguage();
        Console.WriteLine(NumericalExpression.GetSelectedLanguage());

        Console.WriteLine(ne1);

        NumericalExpression ne5 = new(0);
        Console.WriteLine(ne5);

        // NumericalExpression ne6 = new(-321);
        // Console.WriteLine(ne6);

        NumericalExpression.AddLanguage();
        Console.WriteLine(ne);

        NumericalExpression.SelectLanguage();
        Console.WriteLine(ne);

        NumericalExpression.SelectLanguage();
        Console.WriteLine(ne);

        NumericalExpression.SelectLanguage();
        Console.WriteLine(ne);
    }
}