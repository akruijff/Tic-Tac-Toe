namespace Tic_Tac_Toe
{
    public class Controller(Board board, View view)
    {
        private bool isAbortRequested = false;
        private const int Sleep = 500;

        public void Start()
        {
            view.Draw();
            while (!isAbortRequested)
            {
                if (view.IsPlayer1Turn)
                    Player1();
                else
                    Player2();
            }
        }

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