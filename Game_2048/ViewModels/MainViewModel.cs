using Game_2048.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private GameField gameField;
        private GameScoreManager gameScore;
        private Command moveBarRight;
        private Command moveBarLeft;
        private Command moveBarUp;
        private Command moveBarDown;
        private Command startGame;
        public MainViewModel(GameField gameField, GameScoreManager gameScore)
        {
            this.gameField = gameField;
            this.gameScore = gameScore;
            moveBarRight = new DelegateCommand(MoveRight);
            moveBarLeft = new DelegateCommand(MoveLeft);
            moveBarUp = new DelegateCommand(MoveUp);
            moveBarDown = new DelegateCommand(MoveDown);
            startGame = new DelegateCommand(InitialGame);
            gameScore.ScoreChanged += GameScore_ScoreChanged;
            gameScore.HighScoreChanged += GameScore_HighScoreChanged;
            gameField.FieldChanged += GameField_FieldChanged;
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        public Command MoveBarRight => moveBarRight;
        public Command MoveBarLeft => moveBarLeft;
        public Command MoveBarUp => moveBarUp;
        public Command MoveBarDown => moveBarDown;
        public Command StartGame => startGame;
        public string Score { get => gameScore.Score.ToString(); }
        public string HighScore { get => gameScore.HighScore.ToString(); }
        public List<string> Bar { get => gameField.Field.ConvertAll((int i) => (i.ToString()).Equals("0") ? string.Empty : i.ToString()); }
        private void MoveRight()
        {
            gameField.MoveRight();
        }
        private void MoveLeft()
        {
            gameField.MoveLeft();
        }
        private void MoveUp()
        {
            gameField.MoveUp();
        }
        private void MoveDown()
        {
            gameField.MoveDown();
        }
        private void InitialGame()
        {
            gameField.InitialGame();
            gameField.GenerateNewBlock();
        }
        private void GameScore_ScoreChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Score)));
        }
        private void GameScore_HighScoreChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(HighScore)));
        }
        private void GameField_FieldChanged(object sender, EventArgs e)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(nameof(Bar)));
        }
        protected void SetProperty<T>(ref T oldValue, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!oldValue?.Equals(newValue) ?? newValue != null)
            {
                oldValue = newValue;

                OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
