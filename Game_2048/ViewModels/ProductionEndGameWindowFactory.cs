using Game_2048.Models;
using Game_2048.Views;

namespace Game_2048.ViewModels
{
    internal class ProductionEndGameWindowFactory : INewViewFactory
    {
        public void CreateNewView(IGameField gamefield, string message = null)
        {
            EndGameView view = new EndGameView()
            {
                DataContext = new EndGameViewModel(gamefield, message)
            };
            view.Show();
        }
    }
}
