public class Pawn : Piece
{
    public Pawn(PieceColor color) : base(color) { }

    // TODO: En passant and promotion
    public override IEnumerable<Move> GetMoves(Board board, int x, int y)
    {
        var from = new Position(x, y);
        int direction = (Color == PieceColor.White) ? 1 : -1;

        if (InBounds(x, y + direction) && board.IsEmpty(x, y + direction)) // 1 Step forward
        {
            yield return new Move(from, new Position(x, y + direction), null);

            if ((Color == PieceColor.White && y == 1) || (Color == PieceColor.Black && y == 6)) // Two steps forward at the start
            {
                if (InBounds(x, y + 2 * direction) && board.IsEmpty(x, y + 2 * direction))
                    yield return new Move(from, new Position(x, y + 2 * direction), null);
            }
        }

        // Capture moves (If a piece is right (1) or left (-1) of our pawn) Diagonal
        foreach (int dx in new[] { -1, 1 })
        {
            int nx = x + dx;
            int ny = y + direction;
            if (InBounds(nx, ny) && board.IsEnemy(nx, ny, Color))
                yield return new Move(from, new Position(nx, ny), board.BoardData[nx, ny]);
        }
    }

}