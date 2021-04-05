using Game_2048.Models;
using System.Windows;
using System.Windows.Input;

namespace Game_2048.ViewModels
{
    internal class EndGameViewModel : ViewModel<EndGameViewModel>
    {
        private IGameField gameField;
        private string message;
        private ICommand newGame;
        private ICommand continueGame;
        private ICommand finishGame;
        public EndGameViewModel(IGameField gameField, string message)
        {
            this.gameField = gameField;
            this.message = message;
            newGame = new DialogCommand(OnNewGame);
            continueGame = new DialogCommand(OnContinueGame, () => !gameField.IsNotMove);
            finishGame = new DialogCommand(CloseWindow);
        }
        public string Message => message;
        public ICommand NewGame => newGame;
        public ICommand ContinueGame => continueGame;
        public ICommand FinishGame => finishGame;
        private void CloseWindow(object window)
        {
            if (window is Window)
            {
                (window as Window).Close();
            }
        }
        private void OnNewGame (object window)
        {
            gameField.InitialGame();
            CloseWindow(window);
            gameField.GenerateNewBlock();
        }
        private void OnContinueGame(object window)
        {
            gameField.ContinueGame();
            CloseWindow(window);
        }
    }
}
