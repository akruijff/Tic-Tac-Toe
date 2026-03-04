namespace Tic_Tac_Toe
{
    public class Board()
    {
        public static int WIDTH { get; } = 3;
        public static int HEIGHT { get; } = 3;
        public static int HORIZONTAL_CENTER { get; } = 1;
        public static int VERTICAL_CENTER { get; } = 1;

        private readonly Cell[,] cells = SetAll(new Cell[WIDTH, HEIGHT], Cell.Untaken);

        private static Cell[,] SetAll(Cell[,] cells, Cell value)
        {
            for (int y = 0; y < HEIGHT; ++y)
                for (int x = 0; x < WIDTH; ++x)
                    cells[x, y] = value;
            return cells;
        }

        public Cell this[int x, int y]
        {
            get => cells[x, y];
            set
            {
                if (Status() != GameStatus.Pending)
                    throw new ArgumentException("GAME OVER!");
                if (cells[x, y] != Cell.Untaken)
                    throw new ArgumentException($"Cell ({x}, {y}) has already been taken by {cells[x, y]}");
                cells[x, y] = value;
            }
        }

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
            return GameStatus.Pending;
        }

        private bool CheckColumn(int y)
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[0, y] != cells[i, y])
                    return false;
            return true;
        }

        private bool CheckRow(int x)
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[x, 0] != cells[x, i])
                    return false;
            return true;
        }

        private bool CheckDiagonal1()
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[0, 0] != cells[i, i])
                    return false;
            return true;
        }

        private bool CheckDiagonal2()
        {
            for (int i = 1; i < WIDTH; ++i)
                if (cells[0, HEIGHT - 1] != cells[i, HEIGHT - 1 - i])
                    return false;
            return true;
        }

        private static GameStatus GetStatus(Func<Cell> func) =>
            func.Invoke() switch
            {
                Cell.Player1 => GameStatus.Player1_won,
                Cell.Player2 => GameStatus.Player2_won,
                _ => GameStatus.Pending,
            };        

        public override string ToString() => Status().ToString();
    }
}
