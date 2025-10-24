using System;
using Backend.ChessEngine;

public class ChessConsole
{
    private Game _game = new Game();

    public void Start()
    {
        while (true)
        {
            PrintBoard();
            Console.WriteLine($"It's {(_game.Turn == PieceColor.White ? "White" : "Black")}'s turn.");
            Console.Write("Enter your move (e.g., e2e4): ");
            string input = Console.ReadLine();

            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Invalid input. Please try again.");
                continue;
            }

            if (input.Equals("exit", StringComparison.OrdinalIgnoreCase))
            {
                break;
            }

            try
            {
                Move move = ParseMove(input);
                if (!_game.MakeMove(move))
                {
                    Console.WriteLine("Invalid move. Please try again.");
                    Thread.Sleep(200);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private void PrintBoard()
    {
        Console.Clear();
        for (int y = 7; y >= 0; y--)
        {
            Console.Write($"{y + 1} ");
            for (int x = 0; x < 8; x++)
            {
                Piece piece = _game.Board.BoardData[x, y];
                char pieceChar = GetPieceChar(piece);
                Console.Write($"[{pieceChar}]");
            }
            Console.WriteLine();
        }
        Console.WriteLine("   a  b  c  d  e  f  g  h");
    }

    private char GetPieceChar(Piece piece)
    {
        if (piece == null)
        {
            return ' ';
        }

        char pieceChar = piece.GetType().Name[0];
        if (piece.Color == PieceColor.Black)
        {
            pieceChar = char.ToLower(pieceChar);
        }
        return pieceChar;
    }

    private Move ParseMove(string moveString)
    {
        if (moveString.Length != 4)
        {
            throw new ArgumentException("Invalid move format. Use format like 'e2e4'.");
        }

        int fromX = moveString[0] - 'a';
        int fromY = int.Parse(moveString[1].ToString()) - 1;
        int toX = moveString[2] - 'a';
        int toY = int.Parse(moveString[3].ToString()) - 1;

        return new Move(new Position(fromX, fromY), new Position(toX, toY), null);
    }
}
