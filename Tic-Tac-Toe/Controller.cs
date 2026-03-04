namespace Tic_Tac_Toe
{
    /// <summary>
    /// Coordinates the game flow between the <see cref="Board"/> logic and the <see cref="View"/> display.
    /// </summary>
    /// <param name="board">The game board instance containing the grid and win logic.</param>
    /// <param name="view">The display instance for rendering the UI.</param>
    public class Controller(Board board, View view)
    {
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
            view.Draw();
            while (!isAbortRequested && board.Status() == GameStatus.Pending)
            {
                if (view.IsPlayer1Turn)
                    Player1();
                else
                    Player2();
            }
            if (board.Status() != GameStatus.Pending)
            {
                Console.WriteLine();
                Console.WriteLine("GAME OVER");
                Console.WriteLine();
                string s = board.Status() == GameStatus.Player1_won ? "1" : "2";
                Console.WriteLine($"Player {s} won!");
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
                    --view.CursorY;
                    view.Draw();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    ++view.CursorY;
                    view.Draw();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    --view.CursorX;
                    view.Draw();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    ++view.CursorX;
                    view.Draw();
                    break;
                case ConsoleKey.Enter:
                case ConsoleKey.Spacebar:
                    try
                    {
                        board[view.CursorX, view.CursorY] = Cell.Player1;
                        view.IsPlayer1Turn = false;
                        view.ErrorMessage = "";
                    }
                    catch(ArgumentException e)
                    {
                        view.ErrorMessage = e.Message;
                    }
                    view.Draw();
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
            Random random = new Random();
            int x = 0, y = 0;
            bool isFound = false;
            while(!isFound)
            {
                x = random.Next(0, Board.WIDTH);
                y = random.Next(0, Board.HEIGHT);
                if (board[x, y] == Cell.Untaken)
                    isFound = true;
            }

            Thread.Sleep(Sleep);
            view.CursorX = x;
            view.CursorY = y;
            view.Draw();

            Thread.Sleep(Sleep);
            board[x, y] = Cell.Player2;
            view.Draw();

            Thread.Sleep(Sleep);
            view.CursorX = Board.HORIZONTAL_CENTER;
            view.CursorY = Board.VERTICAL_CENTER;
            view.IsPlayer1Turn = true;
            view.Draw();
        }
    }
}