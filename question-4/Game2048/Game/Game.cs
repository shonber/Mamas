
using System.Text;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Xml;

namespace Game2048;

public class Game
{
    private Board gameBoard;
    private GameStatus status;
    private int points;

    private static readonly string leaderBoardPathName = "../leaderboard.xml";


    private Dictionary<DateTime, string[]> leaderBoard;

    public Game() {
        this.gameBoard = new();
        this.status = GameStatus.Idle;
        this.points = 0;

        this.leaderBoard = new();

        LoadLeaderBoard();
    }

    public Board GameBoard{
        get{
            return gameBoard;
        }
        private set{
            gameBoard = value;
        }
    }

    public GameStatus Status{
        get{
            return status;
        }
        protected set{
            status = value;
        }
    }

    public int Points{
        get{
            return points;
        }
        protected set{
            points = value;
        }
    }

    protected Dictionary<DateTime, string[]> LeaderBoard{
        get {
            return leaderBoard;
        }
    }

    protected void AddToLeaderBoard(){
        // The method will add the current play to the leader board.

        this.leaderBoard.Add(DateTime.Now, [Points.ToString(), GameBoard.Stopper.ToString()]);
        SaveLeaderBoardToXml();

    }

    protected void SaveLeaderBoardToXml(){
        // The method will save a dictionary to a XML file.
        
        DataContractSerializer serializer = new(this.leaderBoard.GetType());

        using var writer = new FileStream(leaderBoardPathName, FileMode.Create, FileAccess.Write);
        serializer.WriteObject(writer, this.leaderBoard);
    }

    protected void ReadLeaderBoardFromXml(){
        // The method will read a dictionary from a XML file.
        
        DataContractSerializer serializer = new(this.leaderBoard.GetType());

        using var reader = new FileStream(leaderBoardPathName, FileMode.Open, FileAccess.Read);
        if (reader.Length <= 0)
            return;
            
        Dictionary<DateTime, string[]> loadedLeaderBoard = (Dictionary<DateTime, string[]>) serializer.ReadObject(reader);
        this.leaderBoard = loadedLeaderBoard;

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

        int index = 0;
        foreach(KeyValuePair<DateTime, string[]> entry in LeaderBoard.Reverse())
        {
            retVal.Append($"{index} - {entry.Key}\n");
            retVal.Append($"\t score: {entry.Value[0]}\n");
            retVal.Append($"\t Time: {entry.Value[1]}\n\n");

            index ++;
        }

        Console.WriteLine(retVal); 
    }

    protected void ResetGame(){
        GameBoard.ResetGame();
        Points = 0;
        Status = GameStatus.Idle;
    }

    protected virtual void StartGame(){        
        GameBoard.Start();

        string input = "";
        bool flag = true;

        while (flag)
        {  
            if (Status == GameStatus.Idle || Status == GameStatus.Win){
                Console.Write("up / down / left / right >>> ");
                input = Console.ReadLine();
                if (input == "u")
                    Move(Direction.Up);
                else if (input == "d")
                    Move(Direction.Down);  
                else if (input == "l")
                    Move(Direction.Left);  
                else if (input == "r")
                    Move(Direction.Right);  

                Console.Write($"SCORE: {Points}");
                Console.WriteLine(GameBoard);

                CheckStatus();
            }else if (Status == GameStatus.Lose){
                Console.Clear();

                string current_status = GameBoard.WonTheGame ? "Won" : "Lost";

                Console.WriteLine($"You {current_status} the game with a score of - {this.points} - time: {GameBoard.Stopper}");
                flag = false;
            }
        }
    }

    protected void LoadLeaderBoard(){
        // The method will load a leader board from a file.


        if (File.Exists(leaderBoardPathName)) {
            // Load to the dictionary
            ReadLeaderBoardFromXml();
        }
        else {
            // Create the file
           using FileStream fs = File.Create(leaderBoardPathName);
        }
    }

    public void CheckStatus(){
        // The method checks the current status of the game.
        Status = GameBoard.Status;
    }

    public void Move(Direction direction){
       // The method oversees the movement logic in the game.

        Console.Clear();

        // Move accordantly
        this.points += GameBoard.Move(direction);

        // Print current board and check status.
        Console.ForegroundColor = ConsoleColor.DarkCyan; 
        if (GameBoard.WonTheGame){
            Console.Write($"SCORE: {Points}");
            Console.ForegroundColor = ConsoleColor.Green; 
            Console.Write($"(WON)");
        }
        else
            Console.Write($"SCORE: {Points}");

        Console.ForegroundColor = ConsoleColor.Gray; 

        Console.ForegroundColor = ConsoleColor.Blue; 
        Console.WriteLine(GameBoard);
        Console.ForegroundColor = ConsoleColor.Gray; 

        CheckStatus();
    }

    public override string ToString(){
        return GameBoard.ToString();
    }
}