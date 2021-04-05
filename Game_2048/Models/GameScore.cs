using System;

namespace Game_2048.Models
{
    internal class GameScore : IGameScore
    {
        private int highScore;
        private int score;

        public GameScore() : this(0) { }

        public GameScore(int highScore)
        {
            this.highScore = highScore;
            this.score = 0;
        }

        public int Score
        {
            get => score;
            set
            {
                if (!score.Equals(value) && value >= 0)
                {
                    score = value;
                    OnScoreChanged(EventArgs.Empty);
                }
            }
        }
        public int HighScore
        {
            get => highScore;
            set
            {
                if (!highScore.Equals(value) && value >= 0)
                {
                    highScore = value;
                    OnHighScoreChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> ScoreChanged;
        public event EventHandler<EventArgs> HighScoreChanged;

        private void OnScoreChanged(EventArgs e) => ScoreChanged?.Invoke(this, e);

        private void OnHighScoreChanged(EventArgs e) => HighScoreChanged?.Invoke(this, e);
    }
}
