using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.IO;
using System.ComponentModel;

namespace Game_2048.Models
{
    internal sealed class GameScoreManager
    {
        private GameScore scores;

        public GameScoreManager()
        {
            scores = new GameScore(LoadHighScore());
            scores.ScoreChanged += Scores_ScoreChanged;
            scores.HighScoreChanged += Scores_HighScoreChanged;
        }

        private void Scores_HighScoreChanged(object sender, EventArgs e)
        {
            OnHighScoreChanged(new PropertyChangedEventArgs(nameof(HighScore)));
        }

        private void Scores_ScoreChanged(object sender, EventArgs e)
        {
            OnScoreChanged(new PropertyChangedEventArgs(nameof(Score)));
        }

        private int LoadHighScore()
        {
            if (File.Exists(HighScoreFile.Path))
            {
                XDocument document = XDocument.Load(HighScoreFile.Path);

                XElement scoreElement = document
                    .Element(HighScoreFile.Root.ElementName)
                    .Elements(HighScoreFile.Root.Score.ElementName).First<XElement>();

                XAttribute highScoreAttribute = scoreElement.Attribute(HighScoreFile.Root.Score.Attributes.HighScore);
                int hightScore = int.Parse(highScoreAttribute.Value);
            }
            else 
            { 
                throw new FileNotFoundException($"Not found file {HighScoreFile.Path}");
            }
            return 0;
        }
        private void SaveHighScore(int highScore)
        {
            XDocument document = XDocument.Load(HighScoreFile.Path);

            XElement scoreElement = document
                .Element(HighScoreFile.Root.ElementName)
                .Elements(HighScoreFile.Root.Score.ElementName).First<XElement>();

            XAttribute highScoreAttribute = scoreElement.Attribute(HighScoreFile.Root.Score.Attributes.HighScore);            int hightScore = int.Parse(highScoreAttribute.Value);
            highScoreAttribute.Value = highScore.ToString();
            document.Save(HighScoreFile.Path);
        }

        public int Score
        {
            get => scores.Score;
            set
            {
                if (!scores.Score.Equals(value) && value >= 0)
                {
                    scores.Score = value;
                    if(value>scores.HighScore)
                    {
                        scores.HighScore = value;
                    }
                    OnScoreChanged(EventArgs.Empty);
                }
            }
        }
        public int HighScore
        {
            get => scores.HighScore;
            set
            {
                if (!scores.HighScore.Equals(value) && value >= 0)
                {
                    if (value > scores.HighScore)
                    {
                        SaveHighScore(value);
                    }
                    scores.HighScore = value;
                    OnHighScoreChanged(EventArgs.Empty);
                }
            }
        }

        public event EventHandler<EventArgs> ScoreChanged;
        public event EventHandler<EventArgs> HighScoreChanged;

        private void OnScoreChanged(EventArgs e)
        {
            ScoreChanged?.Invoke(this, e);
        }
        private void OnHighScoreChanged(EventArgs e)
        {
            HighScoreChanged?.Invoke(this, e);
        }
    }
}
