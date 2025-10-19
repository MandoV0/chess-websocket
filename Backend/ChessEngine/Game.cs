public class Game
{
    public Board Board { get; private set; } = new Board();
    public PieceColor Turn { get; private set; } = PieceColor.White;
    public Stack<Move> MoveHistory { get; } = new Stack<Move>();

    public bool MakeMove(Move move)
    {
        var piece = Board.BoardData[move.From.X, move.From.Y];
        if (piece == null || piece.Color != Turn) return false;

        // Is move legal?
        var legalMoves = piece.GetMoves(Board, move.From.X, move.From.Y);

        // Illegal move
        if (!legalMoves.Any(m => m.To.Equals(move.To))) return false;
        

        // Switch out Piece and perform move
        move.CapturedPiece = Board.BoardData[move.To.X, move.To.Y];
        Board.BoardData[move.To.X, move.To.Y] = piece;
        Board.BoardData[move.From.X, move.From.Y] = null;
        MoveHistory.Push(move);

        Turn = Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;
        return true;
    }
}