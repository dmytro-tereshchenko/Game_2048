using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Game_2048.Models
{
    internal class GameField : IGameField
    {
        private int rows;
        private int cols;
        private IGameScoreManager scores;
        private List<int> field;
        private Random rand;
        private bool isNotMove;
        private bool isWinGame;

        public GameField(IGameScoreManager gameScoreManager, int rows, int cols)
        {
            scores = gameScoreManager;
            this.rows = rows;
            this.cols = cols;
            field = new List<int>(rows * cols);
            rand = new Random(DateTime.Now.Millisecond);
        }

        public List<int> Field => field;
        public int Rows => rows;
        public int Cols => cols;
        public bool IsNotMove => isNotMove;
        public bool IsWinGame => isWinGame;

        public event EventHandler<EventArgs> FieldChanged;
        public event EventHandler<EventArgs> NotMove;
        public event EventHandler<EventArgs> WinGame;

        private void OnFieldChanged(EventArgs e) => FieldChanged?.Invoke(this, e);
        private void OnIsNotMove(EventArgs e) => NotMove?.Invoke(this, e);
        private void OnWinGame(EventArgs e) => WinGame?.Invoke(this, e);

        public virtual void InitialGame()
        {
            field.Clear();
            for(int i = 0; i < rows * cols; i++)
            {
                field.Add(0);
            }
            scores.Score = 0;
            isNotMove = false;
            isWinGame = false;
        }
        public virtual void ContinueGame() => isWinGame = false;
        public virtual int GetCell(int row, int col) => field[row * rows + col];
        private void SetCell(int row, int col, int value) => field[row * rows + col] = value;
        public virtual void GenerateNewBlock()
        {
            int countFreeCell = GetCountFreeCell();
            if (countFreeCell > 0)
            {
                int position = rand.Next(countFreeCell) + 1;
                for (int i = 0; i < field.Count; i++)
                {
                    if (field[i] == 0)
                    {
                        position--;
                        if (position == 0)
                        {
                            field[i] = rand.Next(10) == 0 ? 4 : 2;
                            OnFieldChanged(new PropertyChangedEventArgs(nameof(Field)));
                            break;
                        }
                    }
                }
            }
        }
        private int GetCountFreeCell() => field.Count((int num) => num == 0);
        private void CheckWin()
        {
            if (field.Contains(2048))
            {
                isWinGame = true;
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
                isNotMove = true;
                OnIsNotMove(new PropertyChangedEventArgs(nameof(Field)));
            }
        }
        public virtual void MoveLeft()
        {
            bool canExecute = MoveNewCellLeft();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols - 1; j++)
                {
                    if (GetCell(i, j) == GetCell(i, j + 1) && GetCell(i, j) > 0)
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
        }
        protected virtual bool MoveNewCellLeft()
        {
            bool canExecute = false;
            int k;
            for (int i = 0; i < rows; i++)
            {
                for (int j = 1; j < cols; j++)
                {
                    k = j - 1;
                    while (k >= 0 && GetCell(i, k) == 0 && GetCell(i, k + 1) > 0)
                    {
                        SetCell(i, k, GetCell(i, k + 1));
                        SetCell(i, k + 1, 0);
                        canExecute = true;
                        k--;
                    }
                }
            }
            return canExecute;
        }
        public virtual void MoveRight()
        {
            bool canExecute = MoveNewCellRight();
            for (int i = 0; i < rows; i++)
            {
                for (int j = cols - 1; j > 0; j--)
                {
                    if (GetCell(i, j) == GetCell(i, j - 1) && GetCell(i, j) > 0)
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
        }
        protected virtual bool MoveNewCellRight()
        {
            bool canExecute = false;
            int k;
            for (int i = 0; i < rows; i++)
            {
                for (int j = cols - 2; j >= 0; j--)
                {
                    k = j + 1;
                    while (k < cols && GetCell(i, k) == 0 && GetCell(i, k - 1) > 0)
                    {
                        SetCell(i, k, GetCell(i, k - 1));
                        SetCell(i, k - 1, 0);
                        canExecute = true;
                        k++;
                    }
                }
            }
            return canExecute;
        }
        public virtual void MoveUp()
        {
            bool canExecute = MoveNewCellUp();
            for (int i = 0; i < rows - 1; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (GetCell(i, j) == GetCell(i + 1, j) && GetCell(i, j) > 0)
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
        }
        protected virtual bool MoveNewCellUp()
        {
            bool canExecute = false;
            int k;
            for (int i = 1; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    k = i - 1;
                    while (k >= 0 && GetCell(k, j) == 0 && GetCell(k + 1, j) > 0)
                    {
                        SetCell(k, j, GetCell(k + 1, j));
                        SetCell(k + 1, j, 0);
                        canExecute = true;
                        k--;
                    }
                }
            }
            return canExecute;
        }
        public virtual void MoveDown()
        {
            bool canExecute = MoveNewCellDown();
            for (int i = rows - 1; i > 0; i--)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (GetCell(i, j) == GetCell(i - 1, j) && GetCell(i, j) > 0)
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
        }
        protected virtual bool MoveNewCellDown()
        {
            bool canExecute = false;
            int k;
            for (int i = rows - 2; i >= 0; i--)
            {
                for (int j = 0; j < cols; j++)
                {
                    k = i + 1;
                    while (k < rows && GetCell(k, j) == 0 && GetCell(k - 1, j) > 0)
                    {
                        SetCell(k, j, GetCell(k - 1, j));
                        SetCell(k - 1, j, 0);
                        canExecute = true;
                        k++;
                    }
                }
            }
            return canExecute;
        }
    }
}
