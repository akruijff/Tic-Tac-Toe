namespace Tic_Tac_Toe
{
    /// <summary>
    /// Coordinates the game flow between the <see cref="Logic"/> logic and the <see cref="View"/> display.
    /// </summary>
    /// <param name="board">The game board instance containing the grid and win logic.</param>
    /// <param name="view">The display instance for rendering the UI.</param>
    public class UI(Logic board)
    {
        /// <summary>
        /// Gets or sets the horizontal cursor position, clamped between 0 and the board's <see cref="Logic.WIDTH"/> - 1.
        /// </summary>
        public int CursorX
        {
            get;
            set => field = Math.Clamp(value, 0, Logic.WIDTH - 1);
        } = 1;

        /// <summary>
        /// Gets or sets the vertical cursor position, clamped between 0 and the board's <see cref="Logic.HEIGHT"/> - 1.
        /// </summary>
        public int CursorY
        {
            get;
            set => field = Math.Clamp(value, 0, Logic.HEIGHT - 1);
        } = 1;

        /// <summary>
        /// Gets or sets an error message to be displayed below the board (e.g., for invalid moves).
        /// </summary>
        public string ErrorMessage { get; internal set; } = "";

        private const int SPACING_X_FACTOR = 2;
        private const int OFFSET_X = 2;
        private const int OFFSET_Y = 3;

        /// <summary>
        /// Indicates if the user has requested to quit the game.
        /// </summary>
        private bool isAbortRequested = false;

        /// <summary>
        /// The delay in milliseconds used for AI "thinking" animations.
        /// </summary>
        private const int Sleep = 500;

        /// <summary>
        /// Starts the main game loop, alternating between the human player and the AI.
        /// </summary>
        /// <remarks>
        /// The loop continues until a winner is found, the board is full, or the user presses Escape.
        /// </remarks>
        public void Start()
        {
            Draw();
            DrawFooter();
            while (!isAbortRequested && board.Status() == GameStatus.Pending)
            {
                if (board.IsPlayer1Turn)
                    Player1();
                else
                    Player2();
            }
            if (board.Status() != GameStatus.Pending)
            {
                Draw();
                DrawGameOver();
            }
        }

        /// <summary>
        /// Handles human input for Player 1, including movement and cell marking.
        /// </summary>
        private void Player1()
        {
            ConsoleKey key = Console.ReadKey(true).Key;
            switch (key)
            {
                case ConsoleKey.Escape:
                    isAbortRequested = true;
                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    Goto(CursorX, CursorY - 1);
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    Goto(CursorX, CursorY + 1);
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    Goto(CursorX - 1, CursorY);
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    Goto(CursorX + 1, CursorY);
                    break;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    try
                    {
                        board.MarkCell(CursorX, CursorY);
                        ErrorMessage = "";
                    }
                    catch(ArgumentException e)
                    {
                        ErrorMessage = e.Message;
                    }
                    Draw();
                    DrawFooter();
                    break;
            }
        }

        /// <summary>
        /// Executes a simple random AI move for Player 2.
        /// </summary>
        /// <remarks>
        /// This method simulates "thinking" by updating the <see cref="View.CursorX"/> 
        /// and <see cref="View.CursorY"/> before marking the cell.
        /// </remarks>
        private void Player2()
        {
            Thread.Sleep(Sleep);
            (int x, int y) = FreeLocation();
            Goto(x, y);

            Thread.Sleep(Sleep);
            board.MarkCell(x, y);
            Draw(CursorX, CursorY);

            Thread.Sleep(Sleep);
            Goto(Logic.HORIZONTAL_CENTER, Logic.VERTICAL_CENTER);
        }

        private (int x, int y) FreeLocation()
        {
            Random random = new Random();
            int x = 0, y = 0;
            bool isFound = false;
            while (!isFound)
            {
                x = random.Next(0, Logic.WIDTH);
                y = random.Next(0, Logic.HEIGHT);
                if (board[x, y] == Cell.Untaken)
                    isFound = true;
            }
            return (x, y);
        }

        private void Goto(int x, int y)
        {
            int oldX = CursorX, oldY = CursorY;
            Draw(CursorX = x, CursorY = y);
            Draw(oldX, oldY);
        }

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
            for (int y = 0; y < Logic.HEIGHT; ++y)
            {
                Console.Write("|");
                for (int x = 0; x < Logic.WIDTH; ++x)
                {
                    Console.Write(" ");
                    DrawCell(x, y);
                }
                Console.WriteLine(" |");
            }
            Console.WriteLine("+-------+");
            Console.WriteLine("");
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
