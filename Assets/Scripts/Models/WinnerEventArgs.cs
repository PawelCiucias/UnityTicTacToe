using System;
using TicTacToe.Constants;

namespace TicTacToe.Models
{
    public class WinnerEventArgs : EventArgs
    {
        public SymbolEnum? Winner { get; }

        public WinnerEventArgs(SymbolEnum? winner)
        {
           this.Winner = winner;
        }
    }
}