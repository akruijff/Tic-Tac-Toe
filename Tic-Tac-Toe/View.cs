namespace Tic_Tac_Toe
{
    public class View(Board board)
    {
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
        /// Renders the game interface, including the header, the board grid, and all cell
        /// contents.
        /// </summary>
        /// <remarks>
        /// This method clears the console and draws the entire static structure. 
        /// Use this for the initial render or when a complete refresh is required.
        /// For incremental updates, use the specialized Draw(x, y) or DrawFooter() methods.
        /// </remarks>
        public void Draw()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Tic Tac Toe");
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
        }

        /// <summary>
        /// Renders the contextual game information, including error messages, 
        /// current player turn, and control instructions at the bottom of the interface.
        /// </summary>
        /// <remarks>
        /// This method acts as the "status bar" of the game. It uses <see cref="IsPlayer1Turn"/> 
        /// to prompt the correct user and displays <see cref="ErrorMessage"/> if a move is invalid.
        /// </remarks>
        public void DrawFooter()
        {
            if (ErrorMessage.Length != 0)
                Console.WriteLine(ErrorMessage);
            else if (board.IsPlayer1Turn)
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

        internal void DrawGameOver()
        {
            Console.WriteLine("GAME OVER");
            Console.WriteLine();
            string s = board.Status() switch
            {
                GameStatus.Pending => "You're aborted the game.",
                GameStatus.Player1_won => "Player 1 won!",
                GameStatus.Player2_won => "Player 2 won!",
                GameStatus.Draw => "It was a draw!",
                _ => throw new NotImplementedException()
            };
            Console.WriteLine(s);
        }
    }
}
