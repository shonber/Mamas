using System.Globalization;
using System.Reflection.Metadata.Ecma335;

namespace MyProject;

public class NumericalExpression
{
    private static readonly string[] unitsArray = new string[] { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
    private static readonly string[] tensArray = new string[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
    private static string[] MultiplierTexts = new string[] { "Hundred", "Thousand", "Million", "Billion", "Trillion" };
    private static long[] multipliers =  new long[] { 100, 1000, 1000000, 1000000000, 1000000000000 };
    private static long maxNumber = 999000000000000;

    Func<string, string> translateMethod = str => str;
    private static string[] supportedLanguages = new string[] { "English", "Hebrew" };
    private static string selectedLanguage = supportedLanguages[0];

    private long number;

    public NumericalExpression (long n){
        this.number = n;
    }

    public long GetValue (){
        // The method return the number attribute value.

        return this.number;
    } 

    public static int SumLetters (long n){
        // The method uses another method to print all number word form from 0 to @n parameter attribute value.

        if (n > maxNumber)
            return -1;

        int charCounter = 0;
        for (long i = 0; i <= n; i++)
        {
            string word = ConvertToString(i).Replace(" ", "");
            charCounter += word.Length;
        }

        return charCounter;
    } 

    // "Mehtod Overloading" is the OOP principle being used.
    public static int SumLetters (NumericalExpression n){
        // The method uses another method to print all number word form from 0 to @n parameter.number attribute value.

        if (n.number > maxNumber)
            return -1;

        int charCounter = 0;
        for (long i = 0; i <= n.number; i++)
        {
            string word = ConvertToString(i).Replace(" ", "");
            charCounter += word.Length;
        }

        return charCounter;
    } 

    private static string ConvertToString (long n){
        // The method uses recursion technique to convert a @number parameter to its word form.

        if (n > maxNumber)
            return "[-] Number is too big.";

        if (n == 0)
            return "Zero";

        string retVal = "";

        for (int i = multipliers.Length - 1; i >= 0 ; i--)
        {

            if ((n / multipliers[i]) > 0)
            {
                retVal += ConvertToString(n / multipliers[i]) + $" {MultiplierTexts[i]} ";
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

    public static void SetMaxNumber(){
        // The method allows the client to change the max number.

        Console.WriteLine($"[!] The current max number is >> {maxNumber}");

        Console.Write("[!] New max number (100, 1000, 1000000, etc.) >>> ");
        long newMaxNumber = long.Parse(Console.ReadLine());

        if (!(newMaxNumber >= multipliers[multipliers.Length - 1] * 1000) && newMaxNumber % 10 != 0){
            Console.WriteLine("[-] Insert a number that is bigger by minimum 1000 and divided by 10.");
            return;
        }

        Console.Write("[!] New max number name (Hundred, Thousand, Million, etc.) >>> ");
        string newMaxNumberName = Console.ReadLine();

        if (MultiplierTexts.Contains(newMaxNumberName) || newMaxNumberName == "" || newMaxNumberName == " "){
            Console.WriteLine("[-] That name is already being used or invalid.");
            return;
        }

        Array.Resize(ref MultiplierTexts, MultiplierTexts.Length + 1);
        Array.Resize(ref multipliers, multipliers.Length + 1);

        maxNumber = 999 * newMaxNumber;
        multipliers[multipliers.Length - 1] = newMaxNumber;
        MultiplierTexts[MultiplierTexts.Length - 1] = newMaxNumberName;

        Console.WriteLine("[+] Updated the max number successfully.");
    }

    public static void SelectLanguage(){
        string supportedLanguagesRetVal = "";
        for (int i = 0; i < supportedLanguages.Length; i++)
        {
            if (i == supportedLanguages.Length - 1){
                supportedLanguagesRetVal += "and " + supportedLanguages[i] + ".";
            }else{
                supportedLanguagesRetVal += supportedLanguages[i] + ", ";
            }
        }

        Console.WriteLine($"[!] Current supported languages >> {supportedLanguagesRetVal}");
        Console.Write("[!] Select a language >>> ");
        string newSelected = Console.ReadLine();

        if (!supportedLanguages.Contains(newSelected)){
            Console.WriteLine("[-] The selected language is yet to be supported.");
            return;
        }

        selectedLanguage = newSelected;

    }

    public static string GetSelectedLanguage(){
        return selectedLanguage;
    }

        // Console.Write("[!] New language name >>> ");
        // string newLanguageName = Console.ReadLine();

        // if (MultiplierTexts.Contains(newMaxNumberName) || newMaxNumberName == "" || newMaxNumberName == " "){
        //     Console.WriteLine($"[-] That name is already being used or invalid.");
        //     return;
        // }

    public override string ToString()
    {  
        return ConvertToString(this.number);
    }
}