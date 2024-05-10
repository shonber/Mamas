
using System.ComponentModel;
using System.Text;

namespace Game2048;

public class Menu()
{
    private Dictionary<string, string> commandsList = new () {
        {"start", "<Enter> to start the game.\n"},
        {"leader board", "<L> to show the game leader board.\n"},
        {"0x435446", "<Escape> for a small challenge ;)\n"},
        {"exit", "<Q> to close the game.\n"},
    };

    public override string ToString()
    {
        // Iterate over the commandList
        string retVal = "";

        Console.Clear();

        // Show welcome message.
        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.WriteLine($"Welcome To 2048!\n");
        Console.ForegroundColor = ConsoleColor.DarkBlue; 

        retVal += $"Menu\n";
        retVal += $"____\n";

        foreach(KeyValuePair<string, string> entry in this.commandsList)
        {
            retVal += entry.Value;
        }

        return retVal.ToString();
    }
}