
namespace Game2048;

public enum Direction {Up, Down, Left, Right}

class Program
{

    // Win - Reached a block of 2048, Lose - no more space and no combinations, Idle - waiting for a move.
    enum GameStatus {Win, Lose, Idle}

    static void Main(string[] args)
    {   
        Board game = new();

        int score = 0;

        game.Start();
        Console.WriteLine(game);

        string input = "";
        while (true)
        {
            Console.Write("up / down / left / right >>> ");
            input = Console.ReadLine();
            if (input == "u")
                score += game.Move(Direction.Up);
            else if (input == "d")
                score += game.Move(Direction.Down);  
            else if (input == "l")
                score += game.Move(Direction.Left);  
            else if (input == "r")
                score += game.Move(Direction.Right);  

            Console.WriteLine(game);
            Console.WriteLine($"The score is: {score}");
        }


        // game.Move(Direction.Down);
        // game.Move(Direction.Left);
        // game.Move(Direction.Right);
    }
}