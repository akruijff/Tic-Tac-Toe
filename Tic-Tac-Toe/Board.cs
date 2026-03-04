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
                if (cells[x, y] != Cell.Untaken)
                    throw new ArgumentException($"Cell ({x}, {y}) has already been taken by {cells[x, y]}");
                cells[x, y] = value;
            }
        }
    }
}
