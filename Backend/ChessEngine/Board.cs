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
}