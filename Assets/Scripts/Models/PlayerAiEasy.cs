using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe.Interfaces;
using TicTacToe.Constants;

namespace TicTacToe.Models
{
    public class PlayerAiEasy : Player
    {
        public PlayerAiEasy(Material material) : base(material)
        {
        }

        public override bool ChooseMove(IGame game, SymbolEnum piece, out (int X, int Y) move)
        {
            var randomX = -1;
            var randomY = -1;

            do
            {
                randomX = Random.Range(0, 3);
                randomY = Random.Range(0, 3);

                if (MainManager.Instance.isGameOver)
                {
                    move =(-1,-1);
                    return false;
                }
            }
            while (!game.isValidMove((randomX, randomY)));

            move = (randomX, randomY);
            return true;
        }
    }
}