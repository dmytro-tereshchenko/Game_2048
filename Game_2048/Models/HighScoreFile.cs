using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.Models
{
    internal static class HighScoreFile
    {
        public const string Path = @"..\..\HighScore.xml";

        public static class Root
        {
            public const string ElementName = "Scores";

            public static class Score
            {
                public const string ElementName = "Score";

                public static class Attributes
                {
                    public const string HighScore = "HighScore";
                }
            }
        }
    }
}
