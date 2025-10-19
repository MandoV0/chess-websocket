public class King : Piece
{
    public King(PieceColor color) : base(color) { }

    public override IEnumerable<Move> GetMoves(Board board, int x, int y)
    {
        var from = new Position(x, y);
        var directions = new (int dx, int dy)[]
        {
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0),
            (1, 1),
            (1, -1),
            (-1, 1),
            (-1, -1)
        };

        foreach (var (dx, dy) in directions)
        {
            int nx = from.X + dx;
            int ny = from.Y + dy;

            if (!InBounds(nx, ny)) continue;
            else if (board.IsEmpty(nx, ny)) yield return new Move(from, new Position(nx, ny), null);
            else if (board.IsEnemy(nx, ny, Color)) yield return new Move(from, new Position(nx, ny), board.BoardData[nx, ny]);
        }
    }
    
    public override Piece Clone()
    {
        return new King(Color);
    }
}