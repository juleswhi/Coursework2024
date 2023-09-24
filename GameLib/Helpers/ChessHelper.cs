namespace Helpers;

public static class ChessHelper
{
    public static Notation GetNotation(this (int, int) coordinates) => coordinates switch
    {
        (1, int Row) => new Notation('a', Row),
        (2, int Row) => new Notation('b', Row),
        (3, int Row) => new Notation('c', Row),
        (4, int Row) => new Notation('d', Row),
        (5, int Row) => new Notation('e', Row),
        (6, int Row) => new Notation('f', Row),
        (7, int Row) => new Notation('g', Row),
        (8, int Row) => new Notation('h', Row),
    };

    public static string ImageDirectory = "../../../../FormsLib/Chess/Images/";

}
