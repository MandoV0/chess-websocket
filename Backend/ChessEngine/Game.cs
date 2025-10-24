namespace Backend.ChessEngine
{
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
            if (!legalMoves.Any(m => m.To.Equals(move.To)))
            {
                Console.WriteLine("This move is illegal!");
                return false;
            }

            // Does this move bring Pieces own king in check?
            if (Board.WouldBeInCheck(move, piece))
            {
                Console.WriteLine("Your king would be in check!");
                return false;
            }

            // Switch out Piece and perform move
            move.CapturedPiece = Board.BoardData[move.To.X, move.To.Y];
            Board.BoardData[move.To.X, move.To.Y] = piece;
            Board.BoardData[move.From.X, move.From.Y] = null;
            MoveHistory.Push(move);

            // Switch Turn
            Turn = Turn == PieceColor.White ? PieceColor.Black : PieceColor.White;

            // Checkmate, Stalemate
            // TODO: End on true returns here or move this Part of the Code into a seperate function.
            if (Board.IsKingInCheck(piece.Color))
            {
                if (!Board.HasAnyLegalMoves(piece.Color))
                {
                    Console.WriteLine($"CHECKMATE! {(Turn == PieceColor.White ? "Black" : "White")} wins!");
                    return true;
                }
                else
                {
                    Console.WriteLine($"CHECK on {Turn}!");
                }
            }
            else
            {
                // King is not in check but no legal moves so we have a  stalemate
                if (!Board.HasAnyLegalMoves(Turn))
                {
                    Console.WriteLine("STALEMATE! Draw.");
                    return true;
                }
            }

            return true;
        }
    }
}
