using System;
using System.Collections.Generic;
using TicTacToe.Constants;
using TicTacToe.Models;

namespace TicTacToe.Interfaces
{
    public interface IGame
    {
        int MoveCount { get; }
        int[,] Grid { get; }
        EventHandler<WinnerEventArgs> GameEndedEvent { get; set; }
        bool ExecuteMove((int X,int Y) move, out SymbolEnum? moved);
        bool isValidMove((int X,int Y) move);
        bool isMoveX();
    }
}