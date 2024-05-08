
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Game2048;

public class Board
{
    // The class oversees the data structure of the game and its functionality.

    private record GeneratedBlock(int Row, int Col, int Value);
    private record BoardSize(int Width, int Height);

    private int[,]? data;
    private bool[] fullSlots;

    private BoardSize boardSize;

    public Board(){
        boardSize = new BoardSize(4, 4);

        Data = new int[boardSize.Width, boardSize.Height];
        this.fullSlots = new bool[boardSize.Width * boardSize.Height];

        ResetBoard();
    }

    public int[,] Data {
        get { return data; }
        protected set { data = value; }
    }

    public void Start(){
        // The method will start the game and place two numbers (2 or 4) on empty slots.

        for (int i = 0; i < 2; i++)
        {
            InsertNewRandomBlock();
        }
    }

    public int Move(Direction direction){
        int score = 0, currentBlock, targetBlock, fullSlotIndex, mergeSum;

        /* The method receives direction: Enum and does the following logic:
        * Move all elements to the direction until they hit another block or the borders.
        * If two blocks have the same value and they touch, they will merge with the value being their sum.
        * If there is space after the move, another block will be generated same as Start() does.
        * if there is not space for a new block, it will check for combinations, if non - end game.
        * No matter when, the method will always return the score gathered from the move.
        */

        Console.WriteLine(direction);

        switch (direction)
        {
            case Direction.Up:
                for (int i = 0; i < boardSize.Height; i++)
                    score += MoveUp();

                InsertNewRandomBlock();

                break;

            case Direction.Down:
                for (int i = 0; i < boardSize.Height; i++)
                    score += MoveDown();
                    
                InsertNewRandomBlock();

                break;


            case Direction.Left:
                for (int i = 0; i < boardSize.Width; i++)
                    score += MoveLeft();

                InsertNewRandomBlock();

                break;


            case Direction.Right:
                for (int i = 0; i < boardSize.Width; i++)
                    score += MoveRight();

                InsertNewRandomBlock();

                break;
        }
        
        return score;
    }

    private void ResetBoard (){
        // The method iterates over the board and resets all values to null.

        for(int row = 0; row < Data.GetLength(0); row++)
            for(int col = 0; col < Data.GetLength(1); col++){
                Data[row,col] = -1;
            }
    }

    private string PrintBoard (){
        // The method iterates over the board and returns a design of the board for the console.

        var sb = new StringBuilder();
        int currentValue;

        sb.Append('\n');
        for(int col = 0; col < Data.GetLength(0); col++){
            sb.Append("+----+----+----+----+\n|");
            for(int row = 0; row < Data.GetLength(1); row++){
                currentValue = Data[col, row];

                if (currentValue == -1)
                    sb.Append($" {currentValue} |");
                else
                    sb.Append($"  {currentValue} |");
            }
            sb.Append('\n');
        }
        sb.Append("+----+----+----+----+\n");

        return sb.ToString();
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

        generated_block = GenerateBlock();
        bool emptySlot = true;

        // This slot is occupied
        if (this.fullSlots[generated_block.Col + generated_block.Row * boardSize.Width]){
            emptySlot = false;
        }

        // search for a different location.
        int counter = 1;
        int size = boardSize.Width * boardSize.Height;
        bool possibleCombinationsFlag;

        while (!emptySlot){
            if (counter == size){
                // No unoccupied slot found.
                Console.WriteLine("No space found");

                // Check if there are any combinations
                possibleCombinationsFlag = CheckPossibleCombinations();
                if (!possibleCombinationsFlag){
                    EndGame();
                }

                return;
            }

            // Unoccupied slot is found.
            generated_block = GenerateBlock();

            if (!this.fullSlots[generated_block.Col + generated_block.Row * boardSize.Width]){
                emptySlot = true;
            }

            counter++;
        }

        Data[generated_block.Row, generated_block.Col] = generated_block.Value;
        this.fullSlots[generated_block.Col + generated_block.Row * boardSize.Width] = true;
    }

    private bool CheckPossibleCombinations(){
        // The method checks if there are any possible combinations in the Board.

        if (FindCombinationsCorners())
            return true;

        if (FindCombinationsSides())
            return true;

        if (FindCombinationsCenter())
            return true;

        return false;
    }

    private bool FindCombinationsCorners(){
        // The method checks if there are any possible combinations in the corners.

        int current, nextSide, nextSide2;

        // TODO: Check for possible combinations.
        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1); col++){
                if ( (row == 0) && (col == 0) ){
                    // top-left

                    current = Data[row,col];
                    nextSide = Data[row,col + 1];
                    nextSide2 = Data[row + 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) )
                        return true;

                }else if ( (row == 0) && (col == boardSize.Width - 1) ){
                    // top-right
                    
                    current = Data[row,col];
                    nextSide = Data[row,col - 1];
                    nextSide2 = Data[row + 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) )
                        return true;

                }else if ( (row == boardSize.Height - 1) && (col == 0) ){
                    // bottom-left
                    
                    current = Data[row,col];
                    nextSide = Data[row,col + 1];
                    nextSide2 = Data[row - 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) )
                        return true;

                }else if ( (row == boardSize.Height - 1) && (col == boardSize.Width - 1) ){
                    // bottom-right
                    
                    current = Data[row,col];
                    nextSide = Data[row,col - 1];
                    nextSide2 = Data[row - 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) )
                        return true;
                }
            }
        }

        return false;
    }

    private bool FindCombinationsSides(){
        // The method checks if there are any possible combinations in the sides.

        int current, nextSide, nextSide2, nextSide3;

        // TODO: Check for possible combinations.
        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1); col++){
                if ( (row == 0) && (col != 0) && (col != boardSize.Width - 1) ){
                    // top-sides

                    current = Data[row, col];
                    nextSide = Data[row, col + 1];
                    nextSide2 = Data[row, col - 1];
                    nextSide3 = Data[row + 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
                        return true;

                }else if ( (row == boardSize.Height - 1) && (col != 0) && (col != boardSize.Width - 1) ){
                    // bottom-sides

                    current = Data[row, col];
                    nextSide = Data[row, col + 1];
                    nextSide2 = Data[row, col - 1];
                    nextSide3 = Data[row - 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
                        return true;

                }else if ( (col == 0) && (row != 0) && (row != boardSize.Height - 1) ){
                    // left-sides
                    
                    current = Data[row, col];
                    nextSide = Data[row + 1, col];
                    nextSide2 = Data[row - 1, col];
                    nextSide3 = Data[row, col + 1];
                    
                    if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
                        return true;

                }else if ( (col == boardSize.Width - 1) && (row != 0) && (row != boardSize.Height - 1) ){
                    // right-sides
                  
                    current = Data[row, col];
                    nextSide = Data[row + 1, col];
                    nextSide2 = Data[row - 1, col];
                    nextSide3 = Data[row, col - 1];
                    
                    if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
                        return true;
                }
            }
        }

        return false;
    }

    private bool FindCombinationsCenter(){
        // The method checks if there are any possible combinations in the center.

        int current, nextSide, nextSide2, nextSide3, nextSide4;

        // TODO: Check for possible combinations.
        for(int row = 0; row < Data.GetLength(0); row++){
            for(int col = 0; col < Data.GetLength(1); col++){
                if ( (row != 0) && (row != boardSize.Height - 1) && (col != 0) && (col != boardSize.Width - 1)){
                    // center

                    current = Data[row, col];
                    nextSide = Data[row - 1, col];
                    nextSide2 = Data[row, col + 1];
                    nextSide3 = Data[row, col - 1];
                    nextSide4 = Data[row + 1,col];
                    
                    if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3) || (current == nextSide4))
                        return true;
                }
            }
        }

        return false;
    }

    private int MoveUp(){
        // The method handles the block movement to the top.

        int score = 0, currentBlock, targetBlock, fullSlotIndex, mergeSum;

        for(int row = Data.GetLength(0) - 1; row >= 0; row--){
            for(int col = 0; col < Data.GetLength(1); col++){
                currentBlock = Data[row,col];

                if (currentBlock != -1){
                    // Makes sure the row is not the upper limit.
                    if (!(row == 0)){
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
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row - 1) * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;
                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row - 1, col] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row - 1) * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;
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
                    if (!(row == boardSize.Height - 1)){
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
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row + 1) * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;
                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row + 1, col] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + (row + 1) * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;
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
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col - 1 + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;
                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row, col - 1] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col - 1 + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;
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
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + 1 + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;

                            // add their sum to the score.
                            score += mergeSum;
                        }else if((targetBlock == -1) && (currentBlock != targetBlock)){
                            // Move current to target.
                            Data[row, col + 1] = currentBlock;

                            // Convert current to -1.
                            Data[row, col] = -1;

                            // update this.fullSlots - current to false and target to true.
                            fullSlotIndex = col + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = false;

                            fullSlotIndex = col + 1 + row * boardSize.Width;
                            this.fullSlots[fullSlotIndex] = true;
                        }
                    }
                }
            }
        }

        return score;
    }

    public void EndGame(){
        // The method will end the current running game.

        // TODO: End the game.
        Console.WriteLine("The game has ended.");
    }

    public override string ToString()
    {
        return PrintBoard();
    }
}