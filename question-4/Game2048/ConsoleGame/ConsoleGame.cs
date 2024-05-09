using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game2048;

public class ConsoleGame : Game
{
    public ConsoleGame() : base(){
    }

    public override void StartGame(){
        // Overriding the inherited method.
 
        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;
        Console.Title = "Is it only 2048 . . ?";

        Console.Clear();
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

}