using Game_2048.Models;
using Game_2048.ViewModels;
using Game_2048.Views;
using System.Windows;

namespace Game_2048
{
    internal class GameBuilder
    {
        public virtual IGameScoreManager BuildGameScoreManager() => new GameScoreManager();
        public virtual IGameField BuildGameField(IGameScoreManager gameScoreManager, int rows, int cols) =>
            new GameField(gameScoreManager, rows, cols);
        public virtual INewViewFactory BuildNewViewFactory() => new ProductionEndGameWindowFactory();
        public virtual IViewModel BuildMainViewModel(IGameField gameField, IGameScoreManager gameScoreManager, INewViewFactory newViewFactory) =>
            new MainViewModel(gameField, gameScoreManager, newViewFactory);
        public virtual Window BuildMainView(IViewModel viewModel) => new MainView(viewModel);
    }
}
