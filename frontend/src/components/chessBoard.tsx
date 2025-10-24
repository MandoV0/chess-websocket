import 'Chessboard.css'

export default function ChessBoard() {
    const rows = Array.from({ length: 8 }, (_, r) => r);
    const cols = Array.from({ length: 8 }, (_, c) => c);

    return (
        <div className="chessboard">
            {rows.map((row) => 
                cols.map((col) => {
                    const isDark = (row + col) % 2 === 1;
                    const coord = `${String.fromCharCode(97 + col)}${8 - row}`; // a8, b7...

                    return (
                        <div
                            key={coord}
                            className={`square ${isDark ? "dark" : "light" }`}
                            data-coord={coord}>
                        </div>
                    );
                })
            )}
        </div>
    );
}