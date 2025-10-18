public class Queen : Piece
{
    public Queen(PieceColor color) : base(color) { }
    
    public override IEnumerable<Move> GetMoves(Board board, int x, int y)
    {
        throw new NotImplementedException();
    }
}