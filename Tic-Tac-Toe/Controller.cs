namespace Tic_Tac_Toe
{
    public class Controller
    {
        public void Start()
        {
            bool isAbortRequested = false;
            while (!isAbortRequested)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
                switch(key)
                {
                    case ConsoleKey.Escape:
                        isAbortRequested = true;
                        break;
                }
            }
        }
    }
}