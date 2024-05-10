using System.ComponentModel.Design;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game2048;

public class ConsoleGame : Game
{
    // TODO: Implement Command design pattern and use here for each command in the Menu

    public ConsoleGame() : base(){

    }

    protected override void StartGame(){
        // Overriding the inherited method.

        Console.Clear();
 
        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;
        Console.Title = "Is it only 2048 . . ?";

        GameBoard.Start();

        ConsoleKeyInfo pressedKey;
        bool flag = true;

        while (flag)
        {  
            if (Status == GameStatus.Idle || Status == GameStatus.Win){
                pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.UpArrow)
                    Move(Direction.Up);
                else if (pressedKey.Key == ConsoleKey.DownArrow)
                    Move(Direction.Down);  
                else if (pressedKey.Key == ConsoleKey.LeftArrow)
                    Move(Direction.Left);  
                else if (pressedKey.Key == ConsoleKey.RightArrow)
                    Move(Direction.Right);  

            }else if (Status == GameStatus.Lose){
                if (GameBoard.WonTheGame){
                    Console.ForegroundColor = ConsoleColor.Green; 
                    Console.WriteLine($"You Won!\n");

                    Console.ForegroundColor = ConsoleColor.DarkCyan; 
                    Console.WriteLine($"Score - {Points}");
                    Console.ForegroundColor = ConsoleColor.Gray; 

                    Console.WriteLine();

                    flag = false;

                }else{
                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine($"You Lost!\n");

                    Console.ForegroundColor = ConsoleColor.DarkCyan; 
                    Console.WriteLine($"SCORE: {Points}");
                    Console.ForegroundColor = ConsoleColor.Gray; 

                    Console.WriteLine();

                    flag = false;
                }

            }
        }
    }

    public void Start(){
        // The method oversees the menu scene.

        Console.Clear();

        // Show welcome message.
        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.WriteLine($"Welcome To 2048!\n");
        Console.ForegroundColor = ConsoleColor.Gray; 

        Menu();

        // TODO: Each command (design pattern) use here.
        // TODO: Printing the command invoker will show the Menu.
        // TODO: Add a static array that will hold all commands.

        // Start option.

        // Exit option.

        // Leader board option.

        // CTF for teach'as
    }

    private void Menu(){
        // The method will print the Menu

    }

}