using System.Windows;

namespace Game_2048
{
    internal class DirectorGameBuilder
    {
        public virtual Window Construct(GameBuilder builder)
        {
            var gameScoreManager = builder.BuildGameScoreManager();
            var gameField = builder.BuildGameField(gameScoreManager, 4, 4);
            var endGameWindowFactory = builder.BuildNewViewFactory();
            var viewModel = builder.BuildMainViewModel(gameField, gameScoreManager, endGameWindowFactory);
            var view = builder.BuildMainView(viewModel);
            return view;
        }
    }
}
