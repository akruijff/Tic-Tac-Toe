namespace Tic_Tac_Toe
{
    /// <summary>
    /// Manages the 3x3 game board, handles move validation, and determines the game status.
    /// </summary>
    public class Logic()
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

        public static int TOP_ROW { get; } = 0;
        public static int CENTER_ROW { get; } = 1;
        public static int BOTTOM_ROW { get; } = 2;
        public static int LEFT_COLUMN { get; } = 0;
        public static int CENTER_COLUMN { get; } = 1;
        public static int RIGHT_COLUMN { get; } = 2;

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
            for (int x = 0; x < WIDTH; ++x)
            {
                GameStatus value = CheckColumn(x);
                if (value != GameStatus.Pending)
                    return value;
            }

            for (int y = 0; y < HEIGHT; ++y)
            {
                GameStatus value = CheckRow(y);
                if (value != GameStatus.Pending)
                    return value;
            }

            GameStatus value1 = CheckDiagonal1();
            if (value1 != GameStatus.Pending)
                return value1;

            GameStatus value2 = CheckDiagonal2();
            if (value2 != GameStatus.Pending)
                return value2;

            return freeCells == 0 ? GameStatus.Draw : GameStatus.Pending;
        }

        private GameStatus CheckColumn(int x)
        {
            if (cells[x, TOP_ROW] == cells[x, CENTER_ROW] && 
                cells[x, CENTER_ROW] == cells[x, BOTTOM_ROW])
                return CheckWinStatus(x, TOP_ROW);            
            return GameStatus.Pending;
        }

        private GameStatus CheckRow(int y)
        {
            if (cells[LEFT_COLUMN, y] == cells[CENTER_COLUMN, y] && 
                cells[CENTER_COLUMN, y] == cells[RIGHT_COLUMN, y])
                return CheckWinStatus(LEFT_COLUMN, y);
            return GameStatus.Pending;
        }

        private GameStatus CheckDiagonal1()
        {
            if (cells[LEFT_COLUMN, TOP_ROW] == cells[CENTER_COLUMN, CENTER_ROW] && 
                cells[CENTER_COLUMN, CENTER_ROW] == cells[RIGHT_COLUMN, BOTTOM_ROW])
                return CheckWinStatus(LEFT_COLUMN, TOP_ROW);
            return GameStatus.Pending;
        }

        private GameStatus CheckDiagonal2()
        {
            if (cells[LEFT_COLUMN, BOTTOM_ROW] == cells[CENTER_COLUMN, CENTER_ROW] && 
                cells[CENTER_COLUMN, CENTER_ROW] == cells[RIGHT_COLUMN, BOTTOM_ROW])
                return CheckWinStatus(LEFT_COLUMN, BOTTOM_ROW);
            return GameStatus.Pending;
        }

        private GameStatus CheckWinStatus(int x, int y)
        {
            if (cells[x, y] == Cell.Player1)
                return GameStatus.Player1_won;
            else if (cells[x, y] == Cell.Player2)
                return GameStatus.Player2_won;
            return GameStatus.Pending;
        }

        /// <summary>
        /// Check if a cell on the board at the specified coordiantes is free.
        /// </summary>
        /// <param name="x">The horizontal index (0 to <see cref="WIDTH"/> - 1).</param>
        /// <param name="y">The vertical index (0 to <see cref="HEIGHT"/> - 1).</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool IsFree(int x, int y) => cells[x, y] == Cell.Untaken;

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
