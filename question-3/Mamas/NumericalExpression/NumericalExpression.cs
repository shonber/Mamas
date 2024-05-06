using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace MyProject;

public class NumericalExpression
{
    private static readonly string[] unitsArray = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    private static readonly string[] tensArray = new string[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };

    private enum MultiplierTexts { Hundred, Thousand, Million, Billion, Trillion };


    private static long[] multipliers =  new long[] { 100, 1000, 1000000, 1000000000, 1000000000000 };
    private static long maxNumber = 999000000000000;

    private long number;

    public NumericalExpression (long n){
        this.number = n;
    }

    public long GetValue (){
        return this.number;
    } 

    public static int SumLetters (long n){
        if (n > maxNumber)
            return -1;

        int charCounter = 0;
        for (long i = 0; i <= n; i++)
        {
            string word = ConvertToString(i).Replace(" ", "");
            Console.WriteLine(word);
            charCounter += word.Length;
        }

        return charCounter;
    } 

    private static string ConvertToString (long n){
        if (n > maxNumber)
            return "[-] Number is too big.";

        if (n == 0)
            return "Zero";

        string retVal = "";

        for (int i = multipliers.Length - 1; i >= 0 ; i--)
        {

            if ((n / multipliers[i]) > 0)
            {
                retVal += ConvertToString(n / multipliers[i]) + $" {(MultiplierTexts)i} ";
                n %= multipliers[i];
            }            
        }


        if (n > 0){
            if (n < 20)
                retVal += unitsArray[n];
            else{
                retVal += tensArray[(n / 10)];
                if ((n % 10) > 0)
                    retVal += " " + unitsArray[(n % 10)];
            }
        }
        return retVal.Replace("  ", " ");
    } 

    public override string ToString()
    {  
        return ConvertToString(this.number);
    }
}