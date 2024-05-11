using System.Text;

namespace Game2048;

public class Menu()
{
    private readonly Dictionary<string, string> mainMenuCommands = new () {
        {"start", "<Enter> to start the game.\n"},
        {"leaderBoard", "<L> to show the game leader board.\n"},
        {"0x435446", "<Escape> for a small challenge ;)\n"},
        {"exit", "<Q> to close the game.\n"},
    };

    private readonly Dictionary<string, string> endMenuCommands = new () {
        {"playAgain", "<Enter> to save the game and play again.\n"},
        {"saveRun", "<S> to save the game and return to the main menu.\n"},
    };

    private readonly Dictionary<string, string> leaderBoardMenuCommands = new () {
        {"goBack", "<Q> to go back to main menu.\n"},
    };

    public void MainMenu(){
        // Prints the Main Menu.

        StringBuilder retVal = new();

        // Show welcome message.
        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.WriteLine("Welcome To 2048!\n");
        Console.ForegroundColor = ConsoleColor.DarkBlue; 

        retVal.Append("Main Menu\n");
        retVal.Append("_________\n\n");

        foreach(KeyValuePair<string, string> entry in this.mainMenuCommands)
        {
            retVal.Append(entry.Value);
        }

        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine(retVal);
        Console.ForegroundColor = ConsoleColor.Gray; 
    }

    public void EndMenu(){
        // Prints the End Menu.

        StringBuilder retVal = new();

        retVal.Append("End Menu\n");
        retVal.Append("________\n\n");

        foreach(KeyValuePair<string, string> entry in this.endMenuCommands)
        {
            retVal.Append(entry.Value);
        }

        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine(retVal);
        Console.ForegroundColor = ConsoleColor.Gray; 
    }

    public void LeaderBoardMenu(){
        // Prints the leader board menu.                

        StringBuilder retVal = new();

        foreach(KeyValuePair<string, string> entry in this.leaderBoardMenuCommands)
        {
            retVal.Append(entry.Value);
        }

        Console.ForegroundColor = ConsoleColor.Yellow; 
        Console.WriteLine(retVal.ToString());
        Console.ForegroundColor = ConsoleColor.Gray; 
    }
}