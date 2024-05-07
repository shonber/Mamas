using System.Globalization;
using System.Reflection.Metadata.Ecma335;
using System.Collections;
using System;
using System.Text;
using System.ComponentModel;

namespace MyProject;

public class NumericalExpression
{
    private static Dictionary<string, string[]> unitsArrayTranslations = new Dictionary<string, string[]> (){
        {"English", new string[]
            {"Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" }
        },
        {"Hebrew", new string[]
            {"אפס", "אחת", "שתיים", "שלוש", "ארבע", "חמש", "שש", "שבע", "שמונה", "תשע", "עשר", "אחת עשרה", "שתיים עשרה", "שלוש עשרה", "ארבע עשרה", "חמש עשרה", "שש עשרה", "שבע עשרה", "שמונה עשרה", "תשע עשרה" }
        },
    };

    private static Dictionary<string, string[]> tensArrayTranslations = new Dictionary<string, string[]> (){
        {"English", new string[] { "Zero", "Ten", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" }},
        {"Hebrew", new string[]
            {"אפס", "עשר", "עשרים", "שלושים", "ארבעים", "חמישים", "שישים", "שבעים", "שמונים", "תשעים" }
        },
    };

    private static Dictionary<string, string[]> multiplierTextsTranslations = new Dictionary<string, string[]> (){
        {"English", new string[] { "Hundred", "Thousand", "Million", "Billion", "Trillion" }},
        {"Hebrew", new string[]
            {"מאה", "אלף", "מיליון", "מיליארד", "טריליון"}
        },
    };

    private static readonly Func<string, string[]> GetUnitsArray = str => unitsArrayTranslations[str];
    private static readonly Func<string, string[]> GetTensArray = str => tensArrayTranslations[str];
    private static readonly Func<string, string[]> GetMultiplierTextsTranslations = str => multiplierTextsTranslations[str];

    private static long[] multipliers =  new long[] { 100, 1000, 1000000, 1000000000, 1000000000000 };
    private static long maxNumber = 999000000000000;

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
        string[] unitsArray = GetUnitsArray(selectedLanguage);
        string[] tensArray = GetTensArray(selectedLanguage);
        string[] multiplierTexts = GetMultiplierTextsTranslations(selectedLanguage);

        if (n > maxNumber)
            return "[-] Number is too big.";

        if (n == 0)
            return unitsArray[0];

        string retVal = "";

        for (int i = multipliers.Length - 1; i >= 0 ; i--)
        {

            if ((n / multipliers[i]) > 0)
            {
                retVal += ConvertToString(n / multipliers[i]) + $" {multiplierTexts[i]} ";
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

        string[] multiplierTexts = GetMultiplierTextsTranslations(selectedLanguage);

        if (multiplierTexts.Contains(newMaxNumberName) || newMaxNumberName == "" || newMaxNumberName == " "){
            Console.WriteLine("[-] That name is already being used or invalid.");
            return;
        }

        Array.Resize(ref multiplierTexts, multiplierTexts.Length + 1);
        Array.Resize(ref multipliers, multipliers.Length + 1);

        maxNumber = 999 * newMaxNumber;
        multipliers[multipliers.Length - 1] = newMaxNumber;
        multiplierTexts[multiplierTexts.Length - 1] = newMaxNumberName;

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

    public static void AddLanguage(){
        // The method allows the client to add additional languages to the class

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
        Console.Write("[!] New language name >>> ");
        string languageName = UTF8ReadLine();

        if (supportedLanguages.Contains(languageName) || languageName == "" || languageName == " "){
            Console.WriteLine("[!] The selected language is already supported or not valid.");
            return;
        }

        Console.WriteLine("[!] Insert a name for the following fields");
        Console.WriteLine("______________________________________________");

        string[] unitsArray = GetUnitsArray(selectedLanguage);
        string[] tensArray = GetTensArray(selectedLanguage);
        string[] multiplierTexts = GetMultiplierTextsTranslations(selectedLanguage);

        string[] newUnitsArray = new string[unitsArray.Length];
        string[] newTensArray = new string[tensArray.Length];
        string[] newMultiplierTexts = new string[multiplierTexts.Length];

        Console.WriteLine("****Units****");
        for (int i = 0; i < unitsArray.Length; i++)
        {
            if (i == 3)
                break;

            Console.Write($"\t[!] {unitsArray[i]} = ");
            newUnitsArray[i] = UTF8ReadLine();
        }

        Console.WriteLine("\n****Tens****");
        for (int i = 0; i < tensArray.Length; i++)
        {
            if (i == 3)
                break;

            Console.Write($"\t[!] {tensArray[i]} = ");
            newTensArray[i] = UTF8ReadLine();
        }

        Console.WriteLine("\n****Multipliers****");
        for (int i = 0; i < multiplierTexts.Length; i++)
        {
            if (i == 3)
                break;

            Console.Write($"\t[!] {multiplierTexts[i]} = ");
            newMultiplierTexts[i] = UTF8ReadLine();
        }

        Array.Resize(ref supportedLanguages, supportedLanguages.Length + 1);
        supportedLanguages[supportedLanguages.Length - 1] = languageName;

        unitsArrayTranslations.Add(languageName, newUnitsArray);
        tensArrayTranslations.Add(languageName, newTensArray);
        multiplierTextsTranslations.Add(languageName, newMultiplierTexts);

        Console.WriteLine("[+] Added language successfully.");
    }

    private static string UTF8ReadLine(){
        Console.InputEncoding = Encoding.UTF8;

        StringBuilder sb = new StringBuilder();
        ConsoleKeyInfo keyInfo;

        while (true)
        {
            keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.Enter)
                break;

            if (keyInfo.Key == ConsoleKey.Backspace)
                sb.Length--;

            sb.Append(keyInfo.KeyChar);
        }

        Console.WriteLine();
        return sb.ToString();
    }

    public override string ToString()
    {  
        Console.OutputEncoding = Encoding.UTF8;

        return ConvertToString(this.number);
    }
}