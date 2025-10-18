public class Move
{
    public Position From { get; set; }
    public Position To { get; set; }
    public Piece CapturedPiece { get; set; }

    public Move(Position from, Position to, Piece capturedPiece)
    {
        From = from;
        To = to;
        CapturedPiece = capturedPiece;
    }
}