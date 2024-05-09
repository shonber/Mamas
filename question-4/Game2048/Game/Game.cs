using System.Runtime.CompilerServices;

namespace Game2048;

public class Game
{
    private Board gameBoard;
    private GameStatus status;
    private int points;

    public Game() {
        this.gameBoard = new();
        this.status = GameStatus.Idle;
        this.points = 0;
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

    public virtual void StartGame(){
        this.gameBoard.Start();

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
                Console.WriteLine(this.gameBoard);

                CheckStatus();
            }else if (Status == GameStatus.Lose){
                string current_status = this.gameBoard.WonTheGame ? "Won" : "Lost";

                Console.WriteLine($"You {current_status} the game with a score of - {this.points}");
                flag = false;
            }
        }
    }

    public void CheckStatus(){
        // The method checks the current status of the game.
        Status = this.gameBoard.Status;
    }

    public void Move(Direction direction){
       // The method oversees the movement logic in the game.

        Console.Clear();

        // Move accordantly
        this.points += this.gameBoard.Move(direction);

        // Print current board and check status.
        Console.ForegroundColor = ConsoleColor.DarkCyan; 
        if (this.gameBoard.WonTheGame){
            Console.Write($"SCORE: {Points} ");
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
        return this.gameBoard.ToString();
    }
}