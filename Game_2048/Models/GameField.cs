using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private Random rand;

        public GameField(GameScoreManager gameScoreManager, int rows, int cols)
        {
            scores = gameScoreManager;
            this.rows = rows;
            this.cols = cols;
            field = new List<int>(rows * cols);
            rand = new Random(DateTime.Now.Millisecond);
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
        private void SetCell(int row, int col, int value) => field[row * rows + col * cols] = value;
        private void GenerateNewBlock()
        {
            int countFreeCell = GetCountFreeCell();
            if (countFreeCell > 0)
            {
                int position = rand.Next(countFreeCell + 1);
                for (int i = 0; i < field.Count; i++)
                {
                    if (field[i] == 0)
                    {
                        position--;
                        if (position == 0)
                        {
                            field[i] = rand.Next(10) == 0 ? 4 : 2;
                            OnFieldChanged(new PropertyChangedEventArgs(nameof(Field)));
                        }
                    }
                }
            }
        }
        private void CheckWin()
        {
            if (field.Contains(2048))
            {
                OnWinGame(new PropertyChangedEventArgs(nameof(Field)));
            }
        }
        private void CheckIsNotMove()
        {
            bool canNextRound = GetCountFreeCell() > 0 ? true : false;
            if (!canNextRound)
            {
                for (int i = 0; i < rows; i++)
                {
                    for (int j = 0; j < cols-1; j++)
                    {
                        if(GetCell(i, j) == GetCell(i, j + 1))
                        {
                            canNextRound = true;
                            break;
                        }
                    }
                }
                if (!canNextRound)
                {
                    for (int i = 0; i < rows - 1; i++)
                    {
                        for (int j = 0; j < cols; j++)
                        {
                            if (GetCell(i, j) == GetCell(i + 1, j))
                            {
                                canNextRound = true;
                                break;
                            }
                        }
                    }
                }
            }
            if (!canNextRound)
            {
                OnIsNotMove(new PropertyChangedEventArgs(nameof(Field)));
            }
        }
        private int GetCountFreeCell() => field.Count((int num) => num == 0);

        public bool MoveLeft()
        {
            bool canExecute = MoveNewCellLeft();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols - 1; j++)
                {
                    if (GetCell(i, j) == GetCell(i, j + 1))
                    {
                        SetCell(i, j, GetCell(i, j) * 2);
                        SetCell(i, j + 1, 0);
                        scores.Score = scores.Score + GetCell(i, j);
                        canExecute = true;
                    }
                }
            }
            if (canExecute)
            {
                MoveNewCellLeft();
                GenerateNewBlock();
                CheckWin();
                CheckIsNotMove();
            }
            return canExecute;
        }
        private bool MoveNewCellLeft()
        {
            bool canExecute = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = cols - 1; j > 0; j--)
                {
                    if (GetCell(i, j) > 0 && GetCell(i, j - 1) == 0)
                    {
                        SetCell(i, j - 1, GetCell(i, j));
                        SetCell(i, j, 0);
                        canExecute = true;
                    }
                }
            }
            return canExecute;
        }
        public bool MoveRight()
        {
            bool canExecute = MoveNewCellRight();
            for (int i = 0; i < rows; i++)
            {
                for (int j = cols - 1; j > 0; j--)
                {
                    if (GetCell(i, j) == GetCell(i, j - 1))
                    {
                        SetCell(i, j, GetCell(i, j) * 2);
                        SetCell(i, j - 1, 0);
                        scores.Score = scores.Score + GetCell(i, j);
                        canExecute = true;
                    }
                }
            }
            if (canExecute)
            {
                MoveNewCellRight();
                GenerateNewBlock();
                CheckWin();
                CheckIsNotMove();
            }
            return canExecute;
        }
        private bool MoveNewCellRight()
        {
            bool canExecute = false;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols - 1; j++)
                {
                    if (GetCell(i, j) > 0 && GetCell(i, j + 1) == 0)
                    {
                        SetCell(i, j + 1, GetCell(i, j));
                        SetCell(i, j, 0);
                        canExecute = true;
                    }
                }
            }
            return canExecute;
        }
        public bool MoveUp()
        {
            bool canExecute = MoveNewCellUp();
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (GetCell(i, j) == GetCell(i + 1, j))
                    {
                        SetCell(i, j, GetCell(i, j) * 2);
                        SetCell(i + 1, j, 0);
                        scores.Score = scores.Score + GetCell(i, j);
                        canExecute = true;
                    }
                }
            }
            if (canExecute)
            {
                MoveNewCellUp();
                GenerateNewBlock();
                CheckWin();
                CheckIsNotMove();
            }
            return canExecute;
        }
        private bool MoveNewCellUp()
        {
            bool canExecute = false;
            for (int i = rows-1; i >0; i--)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (GetCell(i, j) > 0 && GetCell(i - 1, j) == 0)
                    {
                        SetCell(i - 1, j, GetCell(i, j));
                        SetCell(i, j, 0);
                        canExecute = true;
                    }
                }
            }
            return canExecute;
        }
        public bool MoveDown()
        {
            bool canExecute = MoveNewCellDown();
            for (int i = rows - 1; i > 0; i--)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (GetCell(i, j) == GetCell(i - 1, j))
                    {
                        SetCell(i, j, GetCell(i, j) * 2);
                        SetCell(i - 1, j, 0);
                        scores.Score = scores.Score + GetCell(i, j);
                        canExecute = true;
                    }
                }
            }
            if (canExecute)
            {
                MoveNewCellDown();
                GenerateNewBlock();
                CheckWin();
                CheckIsNotMove();
            }
            return canExecute;
        }
        private bool MoveNewCellDown()
        {
            bool canExecute = false;
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (GetCell(i, j) > 0 && GetCell(i + 1, j) == 0)
                    {
                        SetCell(i + 1, j, GetCell(i, j));
                        SetCell(i, j, 0);
                        canExecute = true;
                    }
                }
            }
            return canExecute;
        }
    }
}
