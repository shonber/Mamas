
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
        Console.WriteLine("Use the arrow keys to play\n");


        while (flag)
        {  
            if (Status == GameStatus.Idle || Status == GameStatus.Win)
                MovementManager();
            else if (Status == GameStatus.Lose){
                if (!IsGameWon())
                    GameLost();

                menu.EndMenu();

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

        bool restartGame = false, flag = true;
        ConsoleKeyInfo pressedKey = Console.ReadKey(true);

        if (IsGameWon()){
            menu.EndMenu();

            while (flag){
                pressedKey = Console.ReadKey(true);

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

            if (restartGame)
                StartGame();
        }


        switch (pressedKey.Key)
        {
            case ConsoleKey.UpArrow:
                Move(Direction.Up);
                break;

            case ConsoleKey.DownArrow:
                Move(Direction.Down);
                break;

            case ConsoleKey.LeftArrow:
                Move(Direction.Left);
                break;

            case ConsoleKey.RightArrow:
                Move(Direction.Right);
                break;

            default:
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.DarkCyan; 
                Console.Write($"SCORE: {Points}");

                Console.ForegroundColor = ConsoleColor.Blue; 
                Console.WriteLine(GameBoard);

                Console.ForegroundColor = ConsoleColor.Magenta; 
                Console.WriteLine("Use the arrow keys to play\n");
                Console.ForegroundColor = ConsoleColor.Gray; 

                break;
        }
    }

    private void GameLost(){
        // The method oversees the actions done after losing a game.

        Console.Clear();

        Console.ForegroundColor = ConsoleColor.Red; 
        Console.WriteLine("You Lost!\n");

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

    public void Start(){
        // The method oversees the menu scene.

        ConsoleKeyInfo pressedKey;
        bool runApp = true;

        ShowMainMenu();

        while (runApp){
            pressedKey = Console.ReadKey(true);

            switch(pressedKey.Key){
                case ConsoleKey.Enter:
                    // Start the game.
                    StartGame();
                    break;

                case ConsoleKey.Q:
                    // Close the game.
                    runApp = ShutDownGame();
                    break;

                case ConsoleKey.L:
                    // Show leader board.
                    ShowLeaderBoardOption();
                    break;
            }
        }
    }

    private void ShowLeaderBoardOption(){
        // The method takes care of the leader board menu.

        ConsoleKeyInfo pressedKey;
        bool showLeaderBoard = true;

        Console.Clear();

        PrintLeaderBoard();
        menu.LeaderBoardMenu();

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
    }

    private void ShowMainMenu(){
        // The method prints the main menu and configures the console a bit.

        Console.Clear();

        Console.BackgroundColor = ConsoleColor.Black;
        Console.CursorVisible = false;
        Console.Title = "Can you get 2048 . . ?";
        Console.TreatControlCAsInput = true;

        Console.ForegroundColor = ConsoleColor.Magenta; 
        menu.MainMenu();
    }

    private static bool ShutDownGame(){
        // The method is part of the shut down process.

        Console.ForegroundColor = ConsoleColor.Red; 
        Console.WriteLine("Game Closed.");
        Console.ForegroundColor = ConsoleColor.Gray; 

        return false;
    }
}