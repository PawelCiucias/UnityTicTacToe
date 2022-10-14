using System;
using TicTacToe.Constants;

namespace TicTacToe.Interfaces
{
    public interface IMove : IEquatable<IMove>
    {
        (int X, int Y) Coordinates { get; set; }
    }
}