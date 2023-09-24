namespace Chess.Board;

public static class Board
{
    // Logic for whose move it is
    public static bool IsWhiteToMove;
    public static int MoveColour => IsWhiteToMove ? 1 : 0;
  
}
