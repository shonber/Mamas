using System.Diagnostics;
using System.Text;

namespace Game2048;

public class Board
{
    private readonly BoardSize boardSize;
    private readonly int winningScore = 2048;

    private Stopwatch stopper;

    public Board(){
        this.boardSize = new BoardSize(4, 4);

        Data = new int[this.boardSize.Width, this.boardSize.Height];
        FullSlots = new bool[this.boardSize.Width * this.boardSize.Height];

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
    public bool[] FullSlots{ set; protected get;}

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
        FullSlots = new bool[this.boardSize.Width * this.boardSize.Height];
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
        foreach (var item in FullSlots)
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

            if (!FullSlots[generated_block.Col + generated_block.Row * this.boardSize.Width])
                foundEmptySlot = true;
        }

        Data[generated_block.Row, generated_block.Col] = generated_block.Value;
        FullSlots[generated_block.Col + generated_block.Row * this.boardSize.Width] = true;

        // check for combinations again
        NoCombinationsEndGame();
    }

    private void NoCombinationsEndGame(){
        // check for combinations if non end game.

        bool possibleCombinationsFlag = CombinationsChecker.CheckPossibleCombinations(Data, this.boardSize);
        if (!possibleCombinationsFlag)
            EndGame();
    }

    private void EndGame(){
        // The method will end the current running game - Lost.

        this.stopper.Stop();
        Status = GameStatus.Lose;
    }

    protected void WinGame(){
        // The method will alert the client that they won.

        WonTheGame = true;
        this.stopper.Stop();
        Status = GameStatus.Lose;
    }

    public int MoveUp(){
        // The method handles the block movement to the top.

        int score = 0, currentBlock, targetBlock, mergeSum;

        for(int row = Data.GetLength(0) - 1; row >= 0; row--){
            for(int col = 0; col < Data.GetLength(1); col++){
                currentBlock = Data[row,col];

                if ((currentBlock != -1) && (row != 0)){
                    targetBlock = Data[row - 1, col];

                    if ((targetBlock != -1) && (currentBlock == targetBlock)){
                        mergeSum = currentBlock + targetBlock;
                        score += mergeSum;
                        MergeUp(row, col, mergeSum);

                    }else if((targetBlock == -1) && (currentBlock != targetBlock))
                        SwitchUp(row, col, currentBlock);
                }
            }
        }

        return score;
    }

    public int MoveDown(){
        // The method handles the block movement to the bottom.

        int score = 0, currentBlock, targetBlock, mergeSum;

        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1); col++){
                currentBlock = Data[row,col];

                if ((currentBlock != -1) && (row != boardSize.Height - 1)){
                    targetBlock = Data[row + 1, col];

                    if ((targetBlock != -1) && (currentBlock == targetBlock)){
                        mergeSum = currentBlock + targetBlock;
                        score += mergeSum;
                        MergeDown(row, col, mergeSum);

                    }else if((targetBlock == -1) && (currentBlock != targetBlock))
                        SwitchDown(row, col, currentBlock);
                }
            }
        }

        return score;
    }

    public int MoveLeft(){
        // The method handles the block movement to the left.

        int score = 0, currentBlock, targetBlock, mergeSum;

        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = Data.GetLength(1) - 1; col >= 0; col--){
                currentBlock = Data[row, col];

                if ((currentBlock != -1) && (col != 0)){
                    targetBlock = Data[row, col - 1];

                    if ((targetBlock != -1) && (currentBlock == targetBlock)){
                        mergeSum = currentBlock + targetBlock;
                        score += mergeSum;
                        MergeLeft(row, col, mergeSum);

                    }else if((targetBlock == -1) && (currentBlock != targetBlock))
                        SwitchLeft(row, col, currentBlock);
                }
            }
        }

        return score;
    }

    public int MoveRight(){
        // The method handles the block movement to the right.

        int score = 0, currentBlock, targetBlock, mergeSum;

        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1) - 1; col++){
                currentBlock = Data[row, col];

                if (currentBlock != -1 && (col != Data.GetLength(1) - 1)){
                    targetBlock = Data[row, col + 1];

                    if ((targetBlock != -1) && (currentBlock == targetBlock)){
                        mergeSum = currentBlock + targetBlock;
                        score += mergeSum;
                        MergeRight(row, col, mergeSum);

                    }else if((targetBlock == -1) && (currentBlock != targetBlock))
                        SwitchRight(row, col, currentBlock);
                }
            }
        }

        return score;
    }

    private void MergeUp(int row, int col, int mergeSum){
        // The method will take care of merging up two blocks.

        Data[row - 1, col] = mergeSum;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col + (row - 1) * boardSize.Width;
        FullSlots[fullSlotIndex] = true;

        if (mergeSum == winningScore && Status != GameStatus.Win)
            WinGame();
    }

    private void SwitchUp(int row, int col, int currentBlock){
        // The method will move a block up.

        Data[row - 1, col] = currentBlock;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col + (row - 1) * boardSize.Width;
        FullSlots[fullSlotIndex] = true;
    }

    private void MergeDown(int row, int col, int mergeSum){
        // The method will take care of merging up two blocks.

        Data[row + 1, col] = mergeSum;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col + (row + 1) * boardSize.Width;
        FullSlots[fullSlotIndex] = true;

        if (mergeSum == winningScore && Status != GameStatus.Win)
            WinGame();
    }

    private void SwitchDown(int row, int col, int currentBlock){
        // The method will move a block up.

        Data[row + 1, col] = currentBlock;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col + (row + 1) * boardSize.Width;
        FullSlots[fullSlotIndex] = true;
    }

    private void MergeLeft(int row, int col, int mergeSum){
        // The method will take care of merging up two blocks.

        Data[row, col - 1] = mergeSum;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col - 1 + row * boardSize.Width;
        FullSlots[fullSlotIndex] = true;

        if (mergeSum == winningScore && Status != GameStatus.Win)
            WinGame();
    }

    private void SwitchLeft(int row, int col, int currentBlock){
        // The method will move a block up.

        Data[row, col - 1] = currentBlock;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col - 1 + row * boardSize.Width;
        FullSlots[fullSlotIndex] = true;
    }
    
    private void MergeRight(int row, int col, int mergeSum){
        // The method will take care of merging up two blocks.

        Data[row, col + 1] = mergeSum;
        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col + 1 + row * boardSize.Width;
        FullSlots[fullSlotIndex] = true;

        if (mergeSum == winningScore && Status != GameStatus.Win)
            WinGame();
    }

    private void SwitchRight(int row, int col, int currentBlock){
        // The method will move a block up.

        Data[row, col + 1] = currentBlock;

        Data[row, col] = -1;

        int fullSlotIndex = col + row * boardSize.Width;
        FullSlots[fullSlotIndex] = false;

        fullSlotIndex = col + 1 + row * boardSize.Width;
        FullSlots[fullSlotIndex] = true;
    }

    public override string ToString(){
        return PrintBoard();
    }
}