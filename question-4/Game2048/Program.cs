
namespace Game2048;

public enum Direction {Up, Down, Left, Right}
public enum GameStatus {Win, Lose, Idle}

static class Program
{
    static void Main(string[] args)
    {   
        ConsoleGame game = new();
        game.Start();
    }
}