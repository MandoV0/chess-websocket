public class Rook : Piece
{
    public Rook(PieceColor color) : base(color) { }

    public override IEnumerable<Move> GetMoves(Board board, int x, int y)
    {
        var from = new Position(x, y);
        var directions = new (int dx, int dy)[]
        {
            (0, 1),     // UP
            (0, -1),    // DOWN
            (-1, 0),    // LEFT
            (1, 0)      // RIGHT
        };

        foreach (var (dx, dy) in directions)
        {
            int nx = x + dx;
            int ny = y + dy;

            while (InBounds(nx, ny))
            {
                if (board.IsEmpty(nx, ny))
                {
                    yield return new Move(from, new Position(nx, ny), null);
                }
                else if (board.IsEnemy(nx, ny, Color))
                {
                    yield return new Move(from, new Position(nx, ny), board.BoardData[nx, ny]);
                    break; // If we caputre someone in this direction then we stop as we cant go further
                }
                else
                {
                    break; // Stop if we would intersect a friendly peace
                }

                nx += dx;
                ny += dy;
            }
        }
    }

    public override Piece Clone()
    {
        return new Rook(Color);
    }
}