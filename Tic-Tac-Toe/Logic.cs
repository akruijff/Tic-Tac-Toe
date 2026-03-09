namespace Tic_Tac_Toe
{
    internal class Logic(int width, int height)
    {
        public static int WIDTH { get; } = 3;
        public static int HEIGHT { get; } = 3;
        public static int TOP_ROW { get; } = 0;
        public static int CENTER_ROW { get; } = 1;
        public static int BOTTOM_ROW { get; } = 2;
        public static int LEFT_COLUMN { get; } = 0;
        public static int CENTER_COLUMN { get; } = 1;
        public static int RIGHT_COLUMN { get; } = 2;

        public int CursorX
        {
            get;
            set => field = Math.Clamp(value, LEFT_COLUMN, RIGHT_COLUMN);
        } = CENTER_COLUMN;

        public int CursorY
        {
            get;
            set => field = Math.Clamp(value, TOP_ROW, BOTTOM_ROW);
        } = CENTER_ROW;

        private readonly Cell[,] cells = SetAll(new Cell[width, height], Cell.Untaken);

        private static Cell[,] SetAll(Cell[,] cells, Cell value)
        {
            for (int y = 0; y < cells.GetLength(1); ++y)
                for (int x = 0; x < cells.GetLength(0); ++x)
                    cells[x, y] = value;
            return cells;
        }

        public void SetCell(int x, int y, Cell value)
        {
            cells[x, y] = value;
        }

        public Cell[,] Cells()
        {
            Cell[,] copy = new Cell[width, height];
            for (int y = 0; y < height; ++y)
                for (int x = 0; x < width; ++x)
                    copy[x, y] = cells[x, y];
            return copy;
        }

        public GameStatus Status()
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

            return CountUntaken() == 0 ? GameStatus.Draw : GameStatus.Pending;
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
                cells[CENTER_COLUMN, CENTER_ROW] == cells[RIGHT_COLUMN, TOP_ROW])
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

        private int CountUntaken()
        {
            int n = 0;
            for (int y = 0; y < cells.GetLength(1); ++y)
                for (int x = 0; x < cells.GetLength(0); ++x)
                    if (cells[x, y] == Cell.Untaken)
                        ++n;
            return n;
        }

        public (int x, int y) FindUntakenCellLocation()
        {
            if (CountUntaken() <= 0)
                throw new ArgumentException("At least on cell needs to be untaken");

            int x = 0, y = 0;
            bool isFound = false;
            Random random = new Random();
            while (!isFound)
            {
                x = random.Next(0, WIDTH);
                y = random.Next(0, HEIGHT);
                if (cells[x, y] == Cell.Untaken)
                    isFound = true;
            }
            return (x, y);
        }

        public bool IsCellTaken(int x, int y) => cells[x, y] == Cell.Untaken;
    }
}
