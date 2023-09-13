namespace Chess.Board;

public class Board
{
    // Logic for whose move it is
    public bool IsWhiteToMove;
    public int MoveColour => IsWhiteToMove ? 1 : 0;
}
