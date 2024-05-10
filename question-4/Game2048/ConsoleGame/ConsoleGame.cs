
using System.Runtime.InteropServices;

namespace Game2048;

public class ConsoleGame : Game
{
    private readonly Menu menu;
    
    public ConsoleGame() : base(){
        menu = new();
    }

    protected override void StartGame(){
        // Overriding the inherited method.

        bool flag = true, restartGame = false;

        Console.Clear();
        GameBoard.Start();

        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.WriteLine($"Use the arrow keys to play\n");


        while (flag)
        {  
            if (Status == GameStatus.Idle || Status == GameStatus.Win)
                MovementManager();
            else if (Status == GameStatus.Lose){
                if (!IsGameWon())
                    GameLost();

                menu.EndMenu();
                EndGameManager();

                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                switch(pressedKey.Key){
                    case ConsoleKey.Enter:
                        // Start the game.
                        flag = SaveRetryGame();
                        restartGame = !flag;
                        break;

                    case ConsoleKey.S:
                        // Save and go back.
                        flag = SaveGoBackOption();
                        break;
                }
            }
        }

        if (restartGame)
            StartGame();

    }

    private void MovementManager(){
        // The method will take care of the code regarding the movement.

        ConsoleKeyInfo pressedKey = Console.ReadKey(true);

        if (pressedKey.Key == ConsoleKey.UpArrow)
            Move(Direction.Up);
        else if (pressedKey.Key == ConsoleKey.DownArrow)
            Move(Direction.Down);  
        else if (pressedKey.Key == ConsoleKey.LeftArrow)
            Move(Direction.Left);  
        else if (pressedKey.Key == ConsoleKey.RightArrow)
            Move(Direction.Right);  

    }

    private bool IsGameWon(){
        // The method checks if the client reached the winning goal.

        if (GameBoard.WonTheGame){
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine($"You Won!\n");

            Console.ForegroundColor = ConsoleColor.DarkCyan; 
            Console.WriteLine($"Score - {Points}");
            Console.WriteLine($"Time: {GameBoard.Stopper}");
            Console.ForegroundColor = ConsoleColor.Gray; 

            Console.WriteLine();

            return true;
        }

        return false;
    }

    private void GameLost(){
        // The method oversees the actions done after losing a game.

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red; 
        Console.WriteLine($"You Lost!\n");

        Console.ForegroundColor = ConsoleColor.DarkCyan; 
        Console.WriteLine($"Score: {Points}");
        Console.WriteLine($"Time: {GameBoard.Stopper}");
        Console.ForegroundColor = ConsoleColor.Gray; 

        Console.WriteLine();
    }

    private bool SaveGoBackOption(){
        // The method will save the current game and go back to main menu.

        AddToLeaderBoard();
        ResetGame();
        Console.Clear();
        menu.MainMenu();

        return false;
        
    }

    private bool SaveRetryGame(){
        // The method will save the current game and start a new one.

        AddToLeaderBoard();
        ResetGame();

        return false;
    }

    private void EndGameManager(){
        // The method oversees the end game actions.

        
    }

    public void Start(){
        // The method oversees the menu scene.

        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;
        Console.Title = "Is it only 2048 . . ?";
        Console.TreatControlCAsInput = true;

        Console.ForegroundColor = ConsoleColor.Magenta; 
        menu.MainMenu();

        ConsoleKeyInfo pressedKey;

        bool runApp = true;

        while (runApp){
            pressedKey = Console.ReadKey(true);

            switch(pressedKey.Key){
                case ConsoleKey.Enter:
                    // Start the game.

                    StartGame();
                    
                    break;

                case ConsoleKey.Q:
                    // Close the game.

                    runApp = false;

                    Console.ForegroundColor = ConsoleColor.Red; 
                    Console.WriteLine("Game Closed.");
                    Console.ForegroundColor = ConsoleColor.Gray; 

                    break;

                case ConsoleKey.L:
                    // Show leader board.
                   
                    Console.Clear();
                    PrintLeaderBoard();
                    menu.LeaderBoardMenu();

                    bool showLeaderBoard = true;
                    while (showLeaderBoard)
                    {
                        pressedKey = Console.ReadKey(true);

                        if(pressedKey.Key == ConsoleKey.Q){
                            // Open main menu.
                            Console.Clear();
                            menu.MainMenu();
                            showLeaderBoard = false;
                        }
                    }

                    break;

                case ConsoleKey.Escape:
                    // Show challenge.

                    System.Console.WriteLine("Challenge started.");
                    break;
            }
        }
    }
}