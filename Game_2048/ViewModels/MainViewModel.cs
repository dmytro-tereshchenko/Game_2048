using Game_2048.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.ViewModels
{
    class MainViewModel
    {
        private GameField gameField;
        private GameScoreManager gameScore;
        private Command moveField;
        public MainViewModel(GameField gameField, GameScoreManager gameScore)
        {
            this.gameField = gameField;
            this.gameScore = gameScore;
            
        }
        public string Score { get => gameScore.Score.ToString(); }
        public string HighScore { get => gameScore.HighScore.ToString(); }
        
        public string Bar_00 { get => gameField.GetCell(0, 0).ToString(); }
        public int MyProperty { get; set; }
        public List<string> Bar { get => gameField.Field.ConvertAll((int i) => (i.ToString()).Equals("0") ? string.Empty : i.ToString()); }
        private void Move()
        {

        }
    }
}
