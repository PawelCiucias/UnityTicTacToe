using System;
using UnityEngine;
using TicTacToe.Constants;

namespace TicTacToe.Interfaces 
{
    public interface IPlayer{
        Material SelectedMaterial { get; }
        bool ChooseMove(IGame game, SymbolEnum piece, out (int X, int Y) move);
    }
}