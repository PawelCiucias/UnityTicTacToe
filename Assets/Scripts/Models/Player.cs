using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe.Interfaces;
using TicTacToe.Constants;

namespace TicTacToe.Models
{
    public class Player : IPlayer
    {
        public Material SelectedMaterial { get; private set; }
        public virtual bool ChooseMove(IGame game, SymbolEnum piece,  out (int X, int Y) move)
        {
            move = (-1,-1);
            return false;
        }

        public Player(Material material)
        {
            this.SelectedMaterial = material;
        }

    }
}
