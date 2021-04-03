using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game_2048.Models
{
    internal class GameField
    {
        private int rows;
        private int cols;
        private GameScoreManager scores;
        private List<int> field;

        public GameField(GameScoreManager gameScoreManager, int rows, int cols)
        {
            scores = gameScoreManager;
            this.rows = rows;
            this.cols = cols;
            field = new List<int>(rows * cols);
            InitialGame();
        }

        public List<int> Field => field;
        public int Rows => rows;
        public int Cols => cols;

        public event EventHandler<EventArgs> FieldChanged;
        public event EventHandler<EventArgs> IsNotMove;
        public event EventHandler<EventArgs> WinGame;

        private void OnFieldChanged(EventArgs e)
        {
            FieldChanged?.Invoke(this, e);
        }
        private void OnIsNotMove(EventArgs e)
        {
            IsNotMove?.Invoke(this, e);
        }
        private void OnWinGame(EventArgs e)
        {
            WinGame?.Invoke(this, e);
        }

        public void InitialGame()
        {
            field.Clear();
            for(int i = 0; i < rows * cols; i++)
            {
                field.Add(0);
            }
            scores.Score = 0;
        }
        private int GetCell(int row, int col) => field[row * rows + col * cols];
        public bool MoveLeft()
        {
            bool canExecute = false;
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols - 1; j++)
                {

                }
            }
            return canExecute;
        }
    }
}
