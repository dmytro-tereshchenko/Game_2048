using System;

namespace Game_2048.Models
{
    internal interface IGameScore
    {
        int Score { get; set; }
        int HighScore { get; set; }
        event EventHandler<EventArgs> ScoreChanged;
        event EventHandler<EventArgs> HighScoreChanged;
    }
}
