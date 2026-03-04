namespace Tic_Tac_Toe
{
    public class Controller(View view)
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
                }
            }
        }
    }
}