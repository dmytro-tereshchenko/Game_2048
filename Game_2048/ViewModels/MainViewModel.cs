using Game_2048.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace Game_2048.ViewModels
{
    internal class MainViewModel : ViewModel<MainViewModel>
    {
        private IGameField gameField;
        private IGameScoreManager gameScore;
        private ICommand moveBarRight;
        private ICommand moveBarLeft;
        private ICommand moveBarUp;
        private ICommand moveBarDown;
        private ICommand startGame;
        private INewViewFactory newViewFactory;
        public MainViewModel(IGameField gameField, IGameScoreManager gameScore, INewViewFactory newViewFactory)
        {
            this.gameField = gameField;
            this.gameScore = gameScore;
            this.newViewFactory = newViewFactory;
            moveBarRight = new DelegateCommand(MoveRight, () => !gameField.IsNotMove && !gameField.IsWinGame);
            moveBarLeft = new DelegateCommand(MoveLeft, () => !gameField.IsNotMove && !gameField.IsWinGame);
            moveBarUp = new DelegateCommand(MoveUp, () => !gameField.IsNotMove && !gameField.IsWinGame);
            moveBarDown = new DelegateCommand(MoveDown, () => !gameField.IsNotMove && !gameField.IsWinGame);
            startGame = new DelegateCommand(InitialGame);
            gameScore.ScoreChanged += GameScore_ScoreChanged;
            gameScore.HighScoreChanged += GameScore_HighScoreChanged;
            gameField.FieldChanged += GameField_FieldChanged;
            gameField.NotMove += GameField_NotMove;
            gameField.WinGame += GameField_WinGame;
        }

        private void GameField_WinGame(object sender, EventArgs e) =>
            newViewFactory.CreateNewView(gameField, "You are win!!!");

        private void GameField_NotMove(object sender, EventArgs e) =>
            newViewFactory.CreateNewView(gameField, "You are lose!!!");
        public ICommand MoveBarRight => moveBarRight;
        public ICommand MoveBarLeft => moveBarLeft;
        public ICommand MoveBarUp => moveBarUp;
        public ICommand MoveBarDown => moveBarDown;
        public ICommand StartGame => startGame;
        public string Score => gameScore.Score.ToString();
        public string HighScore => gameScore.HighScore.ToString();
        public List<string> Bar => gameField.Field.ConvertAll((int i) =>
        i.Equals(0) ? string.Empty : i.ToString());
        private void MoveRight() => gameField.MoveRight();
        private void MoveLeft() => gameField.MoveLeft();
        private void MoveUp() => gameField.MoveUp();
        private void MoveDown() => gameField.MoveDown();
        private void InitialGame()
        {
            gameField.InitialGame();
            gameField.GenerateNewBlock();
        }
        private void GameScore_ScoreChanged(object sender, EventArgs e) =>
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Score)));
        private void GameScore_HighScoreChanged(object sender, EventArgs e) =>
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(HighScore)));
        private void GameField_FieldChanged(object sender, EventArgs e) =>
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Bar)));
    }
}
