using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace MyProject;

public class NumericalExpression
{
    private long number;
    private static readonly long MAX_NUMBER = 999000000000000;
    private static readonly Dictionary<string, string> digitNumbers = new Dictionary<string, string>{
        {"0", "Zero"},
        {"1", "One"},
        {"2", "Two"},
        {"3", "Three"},
        {"4", "Four"},
        {"5", "Five"},
        {"6", "Six"},
        {"7", "Seven"},
        {"8", "Eight"},
        {"9", "Nine"},
    };

    private static readonly Dictionary<string, string> teenNumbers = new Dictionary<string, string>{
        {"10", "Ten"},
        {"11", "Eleven"},
        {"12", "Twelve"},
        {"13", "Thirteen"},
        {"14", "Fourteen"},
        {"15", "Fifteen"},
        {"16", "Sixteen"},
        {"17", "Seventeen"},
        {"18", "Eighteen"},
        {"19", "Nineteen"},
    };

    private static readonly Dictionary<string, string> tensNumbers = new Dictionary<string, string>{
        {"20", "Twenty"},
        {"30", "Thirty"},
        {"40", "Forty"},
        {"50", "Fifty"},
        {"60", "Sixty"},
        {"70", "Seventy"},
        {"80", "Eighty"},
        {"90", "Ninety"},
        {"100", "Hundred"},
        {"1000", "Thousand"},
        {"1000000", "Million"},
        {"1000000000", "Billion"},
        {"1000000000000", "Trillion"},
    };

    public NumericalExpression (long n){
        this.number = n;
    }

    public long GetValue (){
        return this.number;
    } 

    public static int SumLetters (long n){
        int counter = 0;

        return counter;
    } 

    public static string ConvertToString (long n){
        if (n > MAX_NUMBER)
            return "";

        string retVal = "";

        string numberInText = n.ToString();
        int numberLength = numberInText.Length;

        int index = 0;
        int digit = 0;
        int fullNumber = 0;
        double modifiers = 0;

        while (n > 0){
            // TODO: Fix the code below
            modifiers = Math.Pow(10, numberLength - index - 1);
            digit = (int)(n / modifiers);

            fullNumber = (int) modifiers * digit;

            Console.WriteLine(fullNumber);
            // retVal += numbersInWords[numberInText[index].ToString()];
            // retVal += " ";
            // retVal += numbersInWords[position.ToString()];
            // retVal += " ";
            
            index ++;
            n /= 10;
        }

        return retVal;
    } 

    public override string ToString()
    {  
        return $"";
    }
}