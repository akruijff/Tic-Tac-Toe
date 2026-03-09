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
                UI.DrawFullScreen(logic.Cells(), logic.CursorX, logic.CursorY, errorMessage, isPlayer1Turn);
                if (isPlayer1Turn)
                    TurnHuman();
                else
                    TurnAI();
            }
            UI.DrawGameOver(logic.Cells(), logic.CursorX, logic.CursorY, logic.Status());
        }

        private void TurnHuman()
        {
            Request request = UI.GetInput();
            switch (request)
            {
                case Request.Exit: isAbortRequested = true; break;
                case Request.Up: --logic.CursorY; break;
                case Request.Down: ++logic.CursorY; break;
                case Request.Left: --logic.CursorX; break;
                case Request.Right: ++logic.CursorX; break;
                case Request.Mark:
                    if (logic.IsCellTaken(logic.CursorX, logic.CursorY))
                    {
                        logic.SetCell(logic.CursorX, logic.CursorY, Cell.Player1);
                        errorMessage = "";
                        isPlayer1Turn = false;
                    }
                    else
                        errorMessage = $"Cell ({logic.CursorX}, {logic.CursorY}) has already been taken.";
                    break;
            }
        }

        private void TurnAI()
        {
            UI.DrawFullScreen(logic.Cells(), logic.CursorX, logic.CursorY, errorMessage, isPlayer1Turn);
            Thread.Sleep(Sleep);

            (logic.CursorX, logic.CursorY) = logic.FindUntakenCellLocation();
            logic.SetCell(logic.CursorX, logic.CursorY, Cell.Player2);
            UI.DrawFullScreen(logic.Cells(), logic.CursorX, logic.CursorY, errorMessage, isPlayer1Turn);
            Thread.Sleep(Sleep);

            (logic.CursorX, logic.CursorY) = (Logic.CENTER_COLUMN, Logic.CENTER_ROW);
            isPlayer1Turn = true;
        }
    }
}
