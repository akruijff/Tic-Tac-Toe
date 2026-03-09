namespace Tic_Tac_Toe
{
    internal class Program
    {
        private const int Sleep = 500;
        private bool isPlayer1Turn = true;
        private bool isAbortRequested = false;
        private string errorMessage = "";
        private readonly Logic logic;

        static void Main(string[] args)
        {
            new Program().StartGame();
        }

        private Program()
        {
            logic = new Logic(Logic.WIDTH, Logic.HEIGHT);
        }

        private void StartGame()
        {
            while (!isAbortRequested && logic.Status() == GameStatus.Pending)
            {
                UI.DrawFullScreen(logic.Cells(), Logic.CursorX, Logic.CursorY, errorMessage, isPlayer1Turn);
                if (isPlayer1Turn)
                    TurnHuman();
                else
                    TurnAI();
            }
            UI.DrawGameOver(logic.Cells(), Logic.CursorX, Logic.CursorY, logic.Status());
        }

        private void TurnHuman()
        {
            Request request = UI.GetInput();
            switch (request)
            {
                case Request.Exit: isAbortRequested = true; break;
                case Request.Up: --Logic.CursorY; break;
                case Request.Down: ++Logic.CursorY; break;
                case Request.Left: --Logic.CursorX; break;
                case Request.Right: ++Logic.CursorX; break;
                case Request.Mark:
                    if (logic.IsCellTaken(Logic.CursorX, Logic.CursorY))
                    {
                        logic.SetCell(Logic.CursorX, Logic.CursorY, Cell.Player1);
                        errorMessage = "";
                        isPlayer1Turn = false;
                    }
                    else
                        errorMessage = $"Cell ({Logic.CursorX}, {Logic.CursorY}) has already been taken.";
                    break;
            }
        }

        private void TurnAI()
        {
            UI.DrawFullScreen(logic.Cells(), Logic.CursorX, Logic.CursorY, errorMessage, isPlayer1Turn);
            Thread.Sleep(Sleep);

            (Logic.CursorX, Logic.CursorY) = logic.FindUntakenCellLocation();
            logic.SetCell(Logic.CursorX, Logic.CursorY, Cell.Player2);
            UI.DrawFullScreen(logic.Cells(), Logic.CursorX, Logic.CursorY, errorMessage, isPlayer1Turn);
            Thread.Sleep(Sleep);

            (Logic.CursorX, Logic.CursorY) = (Logic.CENTER_COLUMN, Logic.CENTER_ROW);
            isPlayer1Turn = true;
        }
    }
}
