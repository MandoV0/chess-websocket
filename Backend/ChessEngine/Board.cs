namespace Backend.ChessEngine
{
    public class Board
    {
        public Piece[,] BoardData { get; private set; } = new Piece[8, 8];

        public Board()
        {
            CreateBoardStartingLayout();
        }

        private void CreateBoardStartingLayout()
        {
            // Pawns
            for (int i = 0; i < 8; i++)
            {
                BoardData[i, 1] = new Pawn(PieceColor.White);
                BoardData[i, 6] = new Pawn(PieceColor.Black);
            }

            // Rooks
            BoardData[0, 0] = new Rook(PieceColor.White);
            BoardData[7, 0] = new Rook(PieceColor.White);
            BoardData[0, 7] = new Rook(PieceColor.Black);
            BoardData[7, 7] = new Rook(PieceColor.Black);

            // Knights
            BoardData[1, 0] = new Knight(PieceColor.White);
            BoardData[6, 0] = new Knight(PieceColor.White);
            BoardData[1, 7] = new Knight(PieceColor.Black);
            BoardData[6, 7] = new Knight(PieceColor.Black);

            // Bishops
            BoardData[2, 0] = new Bishop(PieceColor.White);
            BoardData[5, 0] = new Bishop(PieceColor.White);
            BoardData[2, 7] = new Bishop(PieceColor.Black);
            BoardData[5, 7] = new Bishop(PieceColor.Black);

            // Queens
            BoardData[3, 0] = new Queen(PieceColor.White);
            BoardData[3, 7] = new Queen(PieceColor.Black);

            // Kings
            BoardData[4, 0] = new King(PieceColor.White);
            BoardData[4, 7] = new King(PieceColor.Black);
        }

        public bool IsEmpty(int x, int y) => BoardData[x, y] == null;

        /// <summary>
        /// Returns if the given Position has an enemy or not.
        /// </summary>
        /// <param name="x">Position of Enemy X</param>
        /// <param name="y">Position of Enemy Y</param>
        /// <param name="color">Color of friendly Team</param>
        /// <returns>True if [X,Y] has an enemy, else false</returns>
        public bool IsEnemy(int x, int y, PieceColor color) => BoardData[x, y] != null && BoardData[x, y].Color != color;

        /// <summary>
        /// Returns if the King of the gives Pieces Color would be in Check after this move is performed.
        /// </summary>
        /// <param name="move">Move our Piece whishes to perform X</param>
        /// <param name="piece">Piece we want to move</param>
        /// <returns>True if King of our Piece would be in Check after this move.</returns>
        public bool WouldBeInCheck(Move move, Piece piece)
        {
            var tempBoard = this.Clone();

            tempBoard.BoardData[move.To.X, move.To.Y] = piece;
            tempBoard.BoardData[move.From.X, move.From.Y] = null;

            var kingPos = tempBoard.GetKingPos(piece.Color);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var boardPiece = tempBoard.BoardData[x, y];
                    if (boardPiece == null || boardPiece.Color == piece.Color) continue;

                    var moves = boardPiece.GetMoves(tempBoard, x, y);
                    if (moves.Any(m => m.To.Equals(kingPos)))
                        return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns wether King of given color is currently in Check
        /// <summary>
        public bool IsKingInCheck(PieceColor KingColor)
        {
            var kingPos = GetKingPos(KingColor);

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = BoardData[x, y];
                    if (piece == null || piece.Color == KingColor) continue;

                    // Can Enemy move to Kings position?
                    var moves = piece.GetMoves(this, x, y);
                    if (moves.Any(m => m.To.Equals(kingPos))) return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if color still have any legal moves left.
        /// <summary>
        public bool HasAnyLegalMoves(PieceColor color)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = BoardData[x, y];
                    if (piece == null || piece.Color != color)
                        continue;

                    var moves = piece.GetMoves(this, x, y);

                    foreach (var move in moves)
                    {
                        if (!WouldBeInCheck(move, piece))
                            return true;
                    }
                }
            }

            return false;
        }


        public Position GetKingPos(PieceColor color)
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = BoardData[x, y];
                    if (piece is King && piece.Color == color)
                    {
                        return new Position(x, y);
                    }
                }
            }

            throw new Exception("King not found"); // King not found. Should not happen.
        }

        public Board Clone()
        {
            var newBoard = new Board();

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    var piece = BoardData[x, y];
                    if (piece != null) newBoard.BoardData[x, y] = piece.Clone();
                }
            }

            return newBoard;
        }
    }
}
