public class Bishop : Piece
{
    public Bishop(PieceColor color) : base(color) { }
    
    public override IEnumerable<Move> GetMoves(Board board, int x, int y)
    {
        throw new NotImplementedException();
    }
}