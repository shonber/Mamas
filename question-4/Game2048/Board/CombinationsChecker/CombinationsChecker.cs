
namespace Game2048;

public static class CombinationsChecker{
    public static bool CheckPossibleCombinations(int[,] data, BoardSize boardSize){
        // The method checks if there are any possible combinations in the Board.

        if (FindCombinationsCorners(data, boardSize))
            return true;

        if (FindCombinationsSides(data, boardSize))
            return true;

        if (FindCombinationsCenter(data, boardSize))
            return true;

        return false;
    }

    private static bool FindCombinationsCorners(int[,] data, BoardSize boardSize){
        // The method checks if there are any possible combinations in the corners.

        for(int row = 0; row < data.GetLength(0); row++){
            for(int col = 0; col < data.GetLength(1); col++){
                if ( (row == 0) && (col == 0) && VerifyCornerTopLeft(data, row, col))
                    return true;

                else if ( (row == 0) && (col == boardSize.Width - 1) && VerifyCornerTopRight(data, row, col))
                    return true;

                else if ( (row == boardSize.Height - 1) && (col == 0) && VerifyCornerBottomLeft(data, row, col))
                    return true;
                    
                else if ( (row == boardSize.Height - 1) && (col == boardSize.Width - 1) && VerifyCornerBottomRight(data, row, col))
                    return true;
                
            }
        }

        return false;
    }
    
    private static bool VerifyCornerTopLeft(int[,] data, int row, int col){
        // The method verifies if the top-left corner has valid combinations.

        int current = data[row, col], nextSide = data[row, col + 1], nextSide2 = data[row + 1, col];
        if ( (current == nextSide) || (current == nextSide2) )
            return true;
        return false;
    }

    private static bool VerifyCornerTopRight(int[,] data, int row, int col){
        // The method verifies if the top-right corner has valid combinations.

        int current = data[row, col], nextSide = data[row, col - 1], nextSide2 = data[row + 1, col];
        if ( (current == nextSide) || (current == nextSide2) )
            return true;
        return false;
    }

    private static bool VerifyCornerBottomLeft(int[,] data, int row, int col){
        // The method verifies if the bottom-left corner has valid combinations.

        int current = data[row, col], nextSide = data[row, col + 1], nextSide2 = data[row - 1, col];
        if ( (current == nextSide) || (current == nextSide2) )
            return true;
        return false;
    }

    private static bool VerifyCornerBottomRight(int[,] data, int row, int col){
        // The method verifies if the bottom-right corner has valid combinations.

        int current = data[row, col], nextSide = data[row, col - 1], nextSide2 = data[row - 1, col];
        if ( (current == nextSide) || (current == nextSide2) )
            return true;
        return false;
    }
    
    private static bool FindCombinationsSides(int[,] data, BoardSize boardSize){
        // The method checks if there are any possible combinations in the sides.

        for(int row = 0; row < data.GetLength(0); row++){
            for(int col = 0; col < data.GetLength(1); col++){
                if ( (row == 0) && (col != 0) && (col != boardSize.Width - 1) && VerifySideTop(data, row, col)){
                    return true;

                }else if ( (row == boardSize.Height - 1) && (col != 0) && (col != boardSize.Width - 1)  && VerifySideBottom(data, row, col)){
                    return true;

                }else if ( (col == 0) && (row != 0) && (row != boardSize.Height - 1)  && VerifySideLeft(data, row, col)){
                    return true;

                }else if ( (col == boardSize.Width - 1) && (row != 0) && (row != boardSize.Height - 1) && VerifySideRight(data, row, col) ){
                    return true;
                }
            }
        }

        return false;
    }

    private static bool VerifySideTop(int[,] data, int row, int col){
        // The method verifies if the side-top corner has valid combinations.

        int current = data[row, col], nextSide = data[row, col + 1], nextSide2 = data[row, col - 1], nextSide3 = data[row + 1, col];
        if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
            return true;
        return false;
    }

    private static bool VerifySideBottom(int[,] data, int row, int col){
        // The method verifies if the side-bottom corner has valid combinations.

        int current = data[row ,col], nextSide = data[row, col + 1], nextSide2 = data[row, col - 1], nextSide3 = data[row - 1,col];
        if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
            return true;
        return false;
    }

    private static bool VerifySideLeft(int[,] data, int row, int col){
        // The method verifies if the side-left corner has valid combinations.

        int current = data[row ,col], nextSide = data[row + 1, col], nextSide2 = data[row - 1, col], nextSide3 = data[row, col + 1];
        if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
            return true;
        return false;
    }

    private static bool VerifySideRight(int[,] data, int row, int col){
        // The method verifies if the side-right corner has valid combinations.

        int current = data[row ,col], nextSide = data[row + 1, col], nextSide2 = data[row - 1, col], nextSide3 = data[row, col - 1];
        if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3))
            return true;
        return false;
    }

    private static bool FindCombinationsCenter(int[,] data, BoardSize boardSize){
        // The method checks if there are any possible combinations in the center.

        for(int row = 0; row < data.GetLength(0); row++){
            for(int col = 0; col < data.GetLength(1); col++){
                if ( (row != 0) && (row != boardSize.Height - 1) && (col != 0) && (col != boardSize.Width - 1) && VerifyCenter(data, row, col)){
                    return true;
                }
            }
        }

        return false;
    }

    private static bool VerifyCenter(int[,] data, int row, int col){
        // The method verifies if the center has valid combinations.

        int current = data[row ,col], nextSide = data[row - 1, col], nextSide2 = data[row, col + 1], nextSide3 = data[row, col - 1] , nextSide4 = data[row + 1, col];
        if ( (current == nextSide) || (current == nextSide2) || (current == nextSide3) || (current == nextSide4))
            return true;
        return false;
    }

}