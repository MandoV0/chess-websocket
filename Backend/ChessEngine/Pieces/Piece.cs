public enum PieceColor { White, Black }

public struct Position
{
    public int X { get; set; }
    public int Y { get; set; }

    public Position(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public abstract class Piece
{
    public PieceColor Color { get; private set; }

    protected Piece(PieceColor color)
    {
        Color = color;
    }

    /// <summary>
    /// To check if a move stays inside the board.
    /// </summary>
    /// <param name="x">Desired Position</param>
    /// <param name="y">Desired Position</param>
    /// <returns>True if the desired postion is inside the board, false otherwise</returns>
    public bool InBounds(int x, int y) => x >= 0 && x < 8 && y >= 0 && y < 8;

    public abstract IEnumerable<Move> GetMoves(Board board, int x, int y);

    public abstract Piece Clone();
}