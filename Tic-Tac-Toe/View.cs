namespace Tic_Tac_Toe
{
    public class View(Board board)
    {
        /// <summary>
        /// Indicates whether it is currently Player 1's turn.
        /// </summary>
        public bool IsPlayer1Turn = true;

        /// <summary>
        /// Gets or sets the horizontal cursor position, clamped between 0 and the board's <see cref="Board.WIDTH"/> - 1.
        /// </summary>
        public int CursorX 
        { 
            get;
            set => field = Math.Clamp(value, 0, Board.WIDTH - 1);
        } = 1;

        /// <summary>
        /// Gets or sets the vertical cursor position, clamped between 0 and the board's <see cref="Board.HEIGHT"/> - 1.
        /// </summary>
        public int CursorY
        {
            get;
            set => field = Math.Clamp(value, 0, Board.HEIGHT - 1);
        } = 1;

        /// <summary>
        /// Gets or sets an error message to be displayed below the board (e.g., for invalid moves).
        /// </summary>
        public string ErrorMessage { get; internal set; } = "";

        private const int SPACING_X_FACTOR = 2;
        private const int OFFSET_X = 2;
        private const int OFFSET_Y = 3;

        /// <summary>
        /// Draws the board and cursor to the console.
        /// </summary>
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Welkom to Tic Tac Toe");
            Console.WriteLine("");
            Console.WriteLine("+-------+");
            for (int y = 0; y < Board.HEIGHT; ++y)
            {
                Console.Write("|");
                for (int x = 0; x < Board.WIDTH; ++x)
                {
                    Console.Write(" ");
                    DrawCell(x, y);
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("+-------+");
            Console.WriteLine("");
            if (ErrorMessage.Length != 0)
                Console.WriteLine(ErrorMessage);
            else if (IsPlayer1Turn)
                Console.WriteLine("Your move");
            else
                Console.WriteLine("Player 2 turn");
            Console.WriteLine("");
            Console.WriteLine("Esc = exit | Array keys or AWSD = move | Enter or space = mark cell");
        }

        /// <summary>
        /// Updates a single cell on the console display by calculating its absolute position.
        /// </summary>
        /// <param name="x">The horizontal board coordinate.</param>
        /// <param name="y">The vertical board coordinate.</param>
        /// <remarks>
        /// This method uses <see cref="Console.SetCursorPosition"/> to prevent full screen flickering 
        /// during cursor movement.
        /// </remarks>
        public void Draw(int x, int y)
        {
            int cx = SPACING_X_FACTOR * x + OFFSET_X, cy = y + OFFSET_Y;
            Console.SetCursorPosition(cx, cy);
            DrawCell(x, y);
        }

        private void DrawCell(int x, int y)
        {
            if (x == CursorX && y == CursorY)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.White;
            }
            switch (board[x, y])
            {
                case Cell.Player1:
                    Console.Write("X");
                    break;
                case Cell.Player2:
                    Console.Write("O");
                    break;
                default:
                    Console.Write("#");
                    break;
            }
            if (x == CursorX && y == CursorY)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }
    }
}
