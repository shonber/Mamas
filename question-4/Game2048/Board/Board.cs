using System.Diagnostics;
using System.Text;

namespace Game2048;

public sealed record BoardSize(int Width, int Height);

public class Board
{
    private sealed record GeneratedBlock(int Row, int Col, int Value);

    private bool[] fullSlots;
    private readonly BoardSize boardSize;
    private readonly int winningScore = 2048;
    private Stopwatch stopper;

    public Board(){
        this.boardSize = new BoardSize(4, 4);

        Data = new int[this.boardSize.Width, this.boardSize.Height];
        this.fullSlots = new bool[this.boardSize.Width * this.boardSize.Height];

        this.stopper = new();

        ResetBoard();
    }

    public int[,] Data { get; protected set; }
    public bool WonTheGame { get; protected set; }
    public GameStatus Status { get; protected set; }
    public TimeSpan Stopper{
        get{
            return stopper.Elapsed;
        }
    }
    
    public void Start(){
        // The method will start the game and place two random numbers on two random slots.

        this.stopper.Start();

        Console.ForegroundColor = ConsoleColor.DarkCyan; 
        Console.Write("SCORE: 0");
        Console.ForegroundColor = ConsoleColor.Gray; 

        for (int i = 0; i < 2; i++)
        {
            InsertNewRandomBlock();
        }

        Console.ForegroundColor = ConsoleColor.Blue; 
        Console.WriteLine(PrintBoard());
        Console.ForegroundColor = ConsoleColor.Gray; 
    }

    public int Move(Direction direction){
        int score = 0;

        /* The method receives direction: Direction and does the following logic:
        * Move all elements to the direction until they hit another block or the borders.
        * If two blocks have the same value and they touch, they will merge with the value being their sum.
        * If there is space after the move, another block will be generated same as Start() does.
        * Check for combinations, if non - end game.
        * No matter when, the method will always return the score gathered from the move.
        */

        Status = GameStatus.Idle;

        switch (direction)
        {
            case Direction.Up:
                for (int i = 0; i < this.boardSize.Height; i++)
                    score += MoveUp();
                InsertNewRandomBlock();
                break;

            case Direction.Down:
                for (int i = 0; i < this.boardSize.Height; i++)
                    score += MoveDown();
                InsertNewRandomBlock();
                break;

            case Direction.Left:
                for (int i = 0; i < this.boardSize.Width; i++)
                    score += MoveLeft();
                InsertNewRandomBlock();
                break;

            case Direction.Right:
                for (int i = 0; i < this.boardSize.Width; i++)
                    score += MoveRight();
                InsertNewRandomBlock();
                break;
        }
        
        return score;
    }

    private void ResetBoard (){
        // The method iterates over the board and resets all values to -1.

        for(int row = 0; row < Data.GetLength(0); row++)
            for(int col = 0; col < Data.GetLength(1); col++){
                Data[row,col] = -1;
            }
    }

    public void ResetGame(){
        // Reset the game.

        ResetBoard();
        this.fullSlots = new bool[this.boardSize.Width * this.boardSize.Height];
        this.stopper = new();
        WonTheGame = false;
        Status = GameStatus.Idle;
    }

    private string PrintBoard (){
        // The method iterates over the board and returns a design of the board for the console.

        var sb = new StringBuilder();
        int currentValue;

        sb.Append('\n');
        for(int row = 0; row < Data.GetLength(0); row++){
            sb.Append("\n+------------+------------+------------+------------+\n");
            sb.Append("|            |            |            |            |\n");

            for(int col = 0; col < Data.GetLength(1); col++){
                currentValue = Data[row, col];
                BoardNumbersManager(sb, currentValue);
                
                if(col == this.boardSize.Width - 1)
                    sb.Append('|');
            }
            sb.Append("\n|            |            |            |            |");
        }

        sb.Append("\n+------------+------------+------------+------------+\n");

        return sb.ToString();
    }

    private static void BoardNumbersManager(StringBuilder sb, int currentValue){
        // The method evens the spaces for the console printed board depending on the currentValue: int and append it to the sb: StringBuilder.

        int numLength = (int) Math.Floor(Math.Log10(currentValue) + 1);

        if (currentValue == -1)
            sb.Append("|            ");
        else{
            switch(numLength){
                case 6:
                    sb.Append($"|   {currentValue}   ");
                    break;

                case 5:
                    sb.Append($"|    {currentValue}   ");
                    break;

                case 4:
                    sb.Append($"|    {currentValue}    ");
                    break;

                case 3:
                    sb.Append($"|    {currentValue}     ");
                    break;

                case 2:
                    sb.Append($"|     {currentValue}     ");
                    break;

                case 1:
                    sb.Append($"|      {currentValue}     ");
                    break;

                default:
                    sb.Append($"|    {currentValue}  ");
                    break;
            }
        }
    }

    private static GeneratedBlock GenerateBlock(){
        // The method generates a new random block for an empty slot

        Random rnd = new();

        int[] possibleValues = [2, 4];
        int row, col, value;

        row = rnd.Next(0, 4);
        col = rnd.Next(0, 4);

        value = possibleValues[rnd.Next(0, 2)];

        return new GeneratedBlock(row, col, value);
    }

    private void InsertNewRandomBlock(){
        // The method adds to the board a random generated block at a random generated location.

        GeneratedBlock generated_block;
        bool foundEmptySlot = false;

        generated_block = GenerateBlock();

        // Check if there are free slots.
        foreach (var item in this.fullSlots)
        {
            if (!item)
                foundEmptySlot = true;
        }

        if (!foundEmptySlot){
            NoCombinationsEndGame();
            return;
        }

        // Grab random free slot.
        foundEmptySlot = false;
        while (!foundEmptySlot){
            generated_block = GenerateBlock();

            if (!this.fullSlots[generated_block.Col + generated_block.Row * this.boardSize.Width])
                foundEmptySlot = true;
        }

        Data[generated_block.Row, generated_block.Col] = generated_block.Value;
        this.fullSlots[generated_block.Col + generated_block.Row * this.boardSize.Width] = true;

        // check for combinations again
        NoCombinationsEndGame();
    }

    private void NoCombinationsEndGame(){
        // check for combinations if non end game.

        bool possibleCombinationsFlag = CombinationsChecker.CheckPossibleCombinations(Data, this.boardSize);
        if (!possibleCombinationsFlag)
            EndGame();
    }

    private int MoveUp(){
        // The method handles the block movement to the top.

        int score = 0, currentBlock, targetBlock, fullSlotIndex, mergeSum;

        for(int row = Data.GetLength(0) - 1; row >= 0; row--){
            for(int col = 0; col < Data.GetLength(1); col++){
                currentBlock = Data[row,col];

                if (currentBlock != -1){
                    // Makes sure the row is not the upper limit.
                    if(row != 0){
                        // Check if the current block is the same as the above one or not - if yes, merge.
                        targetBlock = Data[row - 1, col];
                        // Checks if they are the same
                        if ((targetBlock != -1) && (currentBlock == targetBlock)){
                            // merge and become their sum and the moved place.
                            mergeSum = currentBlock + targetBlock;

                            // Move current to target with the mergeValue and change the current to -1.
                            Data[row - 1, col] = mergeSum;
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row - 1) * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;

                            // Check if the user won
                            if (mergeSum == winningScore && Status != GameStatus.Win)
                                WinGame();

                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row - 1, col] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row - 1) * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // // Check if the user won
                            // if (Status == GameStatus.Win)
                            //     WinGame();
                        }
                    }
                }
            }
        }

        return score;
    }

    private int MoveDown(){
        // The method handles the block movement to the bottom.

        int score = 0, currentBlock, targetBlock, fullSlotIndex, mergeSum;

        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1); col++){
                currentBlock = Data[row,col];

                if (currentBlock != -1){
                    // Makes sure the row is not the upper limit.
                    if (!(row == this.boardSize.Height - 1)){
                        // Check if the current block is the same as the above one or not - if yes, merge.
                        targetBlock = Data[row + 1, col];
                        // Checks if they are the same
                        if ((targetBlock != -1) && (currentBlock == targetBlock)){
                            // merge and become their sum and the moved place.
                            mergeSum = currentBlock + targetBlock;

                            // Move current to target with the mergeValue and change the current to -1.
                            Data[row + 1, col] = mergeSum;
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row + 1) * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;

                            // Check if the user won
                            if (mergeSum == winningScore && Status != GameStatus.Win)
                                WinGame();

                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row + 1, col] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row + 1) * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // // Check if the user won
                            // if (Status == GameStatus.Win)
                            //     WinGame();

                        }
                    }
                }
            }
        }

        return score;
    }

    private int MoveLeft(){
        // The method handles the block movement to the left.

        int score = 0, currentBlock, targetBlock, fullSlotIndex, mergeSum;

        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = Data.GetLength(1) - 1; col >= 0; col--){
                currentBlock = Data[row, col];

                if (currentBlock != -1){
                    // Makes sure the row is not the upper limit.
                    if (!(col == 0)){
                        // Check if the current block is the same as the above one or not - if yes, merge.
                        targetBlock = Data[row, col - 1];
                        // Checks if they are the same
                        if ((targetBlock != -1) && (currentBlock == targetBlock)){
                            // merge and become their sum and the moved place.
                            mergeSum = currentBlock + targetBlock;

                            // Move current to target with the mergeValue and change the current to -1.
                            Data[row, col - 1] = mergeSum;
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col - 1 + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;

                            // Check if the user won
                            if (mergeSum == winningScore && Status != GameStatus.Win)
                                WinGame();

                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row, col - 1] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col - 1 + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // // Check if the user won
                            // if (Status == GameStatus.Win)
                            //     WinGame();
                        }
                    }
                }
            }
        }

        return score;
    }

    private int MoveRight(){
        // The method handles the block movement to the right.

        int score = 0, currentBlock, targetBlock, fullSlotIndex, mergeSum;

        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1) - 1; col++){
                currentBlock = Data[row, col];

                if (currentBlock != -1){
                    // Makes sure the row is not the upper limit.
                    if (!(col == Data.GetLength(1) - 1)){
                        // Check if the current block is the same as the above one or not - if yes, merge.
                        targetBlock = Data[row, col + 1];
                        // Checks if they are the same
                        if ((targetBlock != -1) && (currentBlock == targetBlock)){
                            // merge and become their sum and the moved place.
                            mergeSum = currentBlock + targetBlock;

                            // Move current to target with the mergeValue and change the current to -1.
                            Data[row, col + 1] = mergeSum;
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + 1 + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;

                            // Check if the user won
                            if (mergeSum == winningScore && Status != GameStatus.Win)
                                WinGame();

                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row, col + 1] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + 1 + row * this.boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // // Check if the user won
                            // if (Status == GameStatus.Win)
                            //     WinGame();
                        }
                    }
                }
            }
        }

        return score;
    }

    private void EndGame(){
        // The method will end the current running game - Lost.

        this.stopper.Stop();
        Status = GameStatus.Lose;
    }

    private void WinGame(){
        // The method will alert the client that they won.

        Status = GameStatus.Win;
        WonTheGame = true;
    }

    public override string ToString(){
        return PrintBoard();
    }
}