namespace Tic_Tac_Toe
{
    /// <summary>
    /// Manages the 3x3 game board, handles move validation, and determines the game status.
    /// </summary>
    public class Board()
    {
        /// <summary>
        /// Indicates whether it is currently Player 1's turn.
        /// </summary>
        public bool IsPlayer1Turn { get; private set; } = true;

        /// <summary>The horizontal size of the board.</summary>
        public static int WIDTH { get; } = 3;

        /// <summary>The vertical size of the board.</summary>
        public static int HEIGHT { get; } = 3;

        private int freeCells = WIDTH * HEIGHT;

        /// <summary>
        /// The horizontal index of the board's center cell.
        /// </summary>
        public static int HORIZONTAL_CENTER { get; } = 1;

        /// <summary>
        /// The vertical index of the board's center cell.
        /// </summary>
        public static int VERTICAL_CENTER { get; } = 1;

        private readonly Cell[,] cells = SetAll(new Cell[WIDTH, HEIGHT], Cell.Untaken);

        private static Cell[,] SetAll(Cell[,] cells, Cell value)
        {
            for (int y = 0; y < HEIGHT; ++y)
                for (int x = 0; x < WIDTH; ++x)
                    cells[x, y] = value;
            return cells;
        }

        /// <summary>
        /// Gets or sets the <see cref="Cell"/> state at the specified coordinates.
        /// </summary>
        /// <param name="x">The horizontal index.</param>
        /// <param name="y">The vertical index.</param>
        /// <exception cref="ArgumentException">Thrown if the game is over or the cell is already occupied.</exception>
        public Cell this[int x, int y] { get => cells[x, y]; }

        /// <summary>
        /// Evaluates the board to check if a player has won or if the game is still pending.
        /// </summary>
        /// <returns>The current <see cref="GameStatus"/>.</returns>
        internal GameStatus Status()
        {
            for (int x = 0; x < HEIGHT; ++x)
                if (CheckRow(x))
                    return GetStatus(() => cells[x, 0]);
            for (int y = 0; y < HEIGHT; ++y)
                if (CheckColumn(y))
                    return GetStatus(() => cells[0, y]);
            if (CheckDiagonal1())
                return GetStatus(() => cells[0, 0]);
            if (CheckDiagonal2())
                return GetStatus(() => cells[0, HEIGHT - 1]);
            return freeCells == 0 ? GameStatus.Draw : GameStatus.Pending;
        }

        private bool CheckColumn(int y)
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[0, y] != cells[i, y])
                    return false;
            return cells[0, y] != Cell.Untaken;
        }

        private bool CheckRow(int x)
        {
            for (int i = 1; i < HEIGHT; ++i)
                if (cells[x, 0] != cells[x, i])
                    return false;
            return cells[x, 0] != Cell.Untaken;
        }

        private bool CheckDiagonal1()
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[0, 0] != cells[i, i])
                    return false;
            return cells[0, 0] != Cell.Untaken;
        }

        private bool CheckDiagonal2()
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[0, HEIGHT - 1] != cells[i, HEIGHT - 1 - i])
                    return false;
            return cells[0, HEIGHT - 1] != Cell.Untaken;
        }

        private static GameStatus GetStatus(Func<Cell> func) =>
            func.Invoke() switch
            {
                Cell.Player1 => GameStatus.Player1_won,
                Cell.Player2 => GameStatus.Player2_won,
                _ => GameStatus.Pending,
            };

        /// <summary>
        /// Places a mark on the board at the specified coordinates for the current player.
        /// </summary>
        /// <param name="x">The horizontal index (0 to <see cref="WIDTH"/> - 1).</param>
        /// <param name="y">The vertical index (0 to <see cref="HEIGHT"/> - 1).</param>
        public void MarkCell(int x, int y)
        {
            if (Status() != GameStatus.Pending)
                throw new ArgumentException("GAME OVER!");
            if (cells[x, y] != Cell.Untaken)
                throw new ArgumentException($"Cell ({x}, {y}) has already been taken by {cells[x, y]}");
            cells[x, y] = IsPlayer1Turn ? Cell.Player1 : Cell.Player2;
            --freeCells;
            IsPlayer1Turn = !IsPlayer1Turn;
        }

        /// <returns>A string such as "Pending", "Player1_won", or "Player2_won".</returns>
        public override string ToString() => Status().ToString();
    }
}
