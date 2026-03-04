namespace Tic_Tac_Toe
{
    public class Controller(Board board, View view)
    {
        public void Start()
        {
            view.Draw();
            bool isAbortRequested = false;
            while (!isAbortRequested)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch(key)
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
                        board[view.CursorX, view.CursorY] = Cell.Player1;
                        view.Draw();
                        break;
                }
            }
        }
    }
}