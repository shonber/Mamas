using System.Dynamic;

namespace MyProject;

class Program
{
    static void Main(string[] args)
    {   
        // *********************************
        // Node and LinkedList Classes
        // *********************************

        // Node n1 = new(1);
        // LinkedList<int> ls = new();

        // ls.Append(2);
        // ls.Append(5);
        // ls.Append(2);
        // ls.Append(5);

        // Console.WriteLine(ls);

        // ls.Prepend(-1);
        // ls.Prepend(-23);
        // ls.Prepend(-2);
        // ls.Prepend(4222);
        // ls.Prepend(0);
        // ls.Prepend(-44121);

        // Console.WriteLine(ls);

        // Console.WriteLine(ls.Pop());
        // Console.WriteLine(ls);

        // Console.WriteLine(ls.Unqueue());
        // Console.WriteLine(ls);

        // var list = ls.ToList();
        // foreach (var node in list)
        // {
        //     Console.WriteLine(node);
        // }

        // bool isCircularCheck = ls.IsCircular();
        // Console.WriteLine(isCircularCheck);

        // Console.WriteLine(ls);
        // ls.Sort();

        // Console.WriteLine(ls.GetMaxNode());
        // Console.WriteLine(ls.GetMinNode());


        // *********************************
        // NumericalExpression Class
        // *********************************

        // NumericalExpression ne = new(10);
        // Console.WriteLine(ne);
        Console.WriteLine(NumericalExpression.SumLetters(25));

    }
}