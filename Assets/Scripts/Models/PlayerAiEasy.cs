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
            Debug.Log($"{piece} choosing move");
            var randomX = -1;
            var randomY = -1;
            var attempts = 0;
            
            do
            {
                randomX = Random.Range(0, 3);
                randomY = Random.Range(0, 3);
                move = (randomX, randomY);
                if(attempts++ > 100){
                    return false;
                }
                    
            }
            while (!game.isValidMove((randomX, randomY)));

         
            return true;
        }
    }
}