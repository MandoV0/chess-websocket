public class Rook : Piece
{
    public Rook(PieceColor color) : base(color) { }
    
    public override IEnumerable<Move> GetMoves(Board board, int x, int y)
    {
        throw new NotImplementedException();
    }
}