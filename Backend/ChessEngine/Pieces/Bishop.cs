namespace Backend.ChessEngine
{
    public class Bishop : Piece
    {
        public Bishop(PieceColor color) : base(color) { }

        public override IEnumerable<Move> GetMoves(Board board, int x, int y)
        {
            var from = new Position(x, y);
            var directions = new (int dx, int dy)[]
            {
                (1, 1),    // up-right
                (1, -1),   // down-right
                (-1, 1),   // up-left
                (-1, -1)   // down-left
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
            return new Bishop(Color);
        }
    }
}