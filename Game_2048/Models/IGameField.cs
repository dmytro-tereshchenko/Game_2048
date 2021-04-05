using System;
using System.Collections.Generic;

namespace Game_2048.Models
{
    internal interface IGameField
    {
        List<int> Field { get; }
        int Rows { get; }
        int Cols { get; }
        bool IsNotMove { get; }
        bool IsWinGame { get; }
        event EventHandler<EventArgs> FieldChanged;
        event EventHandler<EventArgs> NotMove;
        event EventHandler<EventArgs> WinGame;
        void InitialGame();
        void ContinueGame();
        int GetCell(int row, int col);
        void GenerateNewBlock();
        void MoveLeft();
        void MoveRight();
        void MoveUp();
        void MoveDown();
    }
}
