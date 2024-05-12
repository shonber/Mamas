using System.Text;

namespace Game2048;

public abstract class Game
{
    protected Dictionary<DateTime, string[]> LeaderBoard{ get; set; }
    protected Board GameBoard{ get; set; }
    protected GameStatus Status{ get; set; }
    protected int Points{ get; set; }

    protected Game() {
        GameBoard = new();
        
        Status = GameStatus.Idle;
        Points = 0;
        LeaderBoard = [];

        LeaderBoard = LeaderBoardFileManager.LoadLeaderBoard(LeaderBoard);
    }

    // The method will be overridden with the start functionality for the 2048 game.
    protected abstract void StartGame();

    protected void AddToLeaderBoard(){
        // The method will add the current play to the leader board.

        LeaderBoard.Add(DateTime.Now, [Points.ToString(), GameBoard.Stopper.ToString()]);
        LeaderBoardFileManager.SaveLeaderBoardToXml(LeaderBoard);

    }

    protected void PrintLeaderBoard(){
        // The method prints the leader board.

        StringBuilder retVal = new();

        // Show welcome message.
        Console.Clear(); 
        Console.ForegroundColor = ConsoleColor.Magenta; 
        Console.ForegroundColor = ConsoleColor.DarkBlue; 

        retVal.Append("Leader Board\n");
        retVal.Append("____________\n\n");

        int index = 1;
        foreach(KeyValuePair<DateTime, string[]> entry in LeaderBoard)
        {
            retVal.Append($"{index} - {entry.Key}\n");
            retVal.Append($"\t score: {entry.Value[0]}\n");
            retVal.Append($"\t Time: {entry.Value[1]}\n\n");

            index ++;
        }

        Console.WriteLine(retVal); 
    }

    protected void ResetGame(){
        // The method resets the game data.

        GameBoard.ResetGame();
        Points = 0;
        Status = GameStatus.Idle;
    }

    protected void Move(Direction direction){
       // The method oversees the movement logic in the game.

        if (Status == GameStatus.Lose)
            return;
            
        Console.Clear();

        Points += GameBoard.Move(direction);

        // Print current board and check status.
        Console.ForegroundColor = ConsoleColor.DarkCyan; 
        Console.Write($"SCORE: {Points}");

        Console.ForegroundColor = ConsoleColor.Blue; 
        Console.WriteLine(GameBoard);
        Console.ForegroundColor = ConsoleColor.Gray; 

        Status = GameBoard.Status;
    }

    protected bool IsGameWon(){
        // The method checks if the client reached the winning goal.

        if (GameBoard.WonTheGame){
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Green; 
            Console.WriteLine("You Won!\n");

            Console.ForegroundColor = ConsoleColor.DarkCyan; 
            Console.WriteLine($"Score - {Points}");
            Console.WriteLine($"Time: {GameBoard.Stopper}");
            Console.ForegroundColor = ConsoleColor.Gray; 

            Console.WriteLine();

            return true;
        }

        return false;
    }
}