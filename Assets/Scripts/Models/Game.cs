using System;
using System.Collections;
using System.Collections.Generic;
using TicTacToe.Constants;
using TicTacToe.Interfaces;
using UnityEngine;

namespace TicTacToe.Models
{

    public class Game : IGame
    {
        protected int[,] grid;

        
        int moveCount = 0;
        public int[,] Board { get { return grid; } }

        public EventHandler<WinnerEventArgs> GameEndedEvent { get; set; }

        public int MoveCount => moveCount;

        public int[,] Grid => grid;

        public Game()
        {
            this.grid = new int[3, 3];
            moveCount = 0;
        }

        public bool isValidMove((int X,int Y) move)
        {
            Func<int, bool> inRangePredicate = (int v) => v > -1 && v < 3;

            return inRangePredicate(move.X) && inRangePredicate(move.Y) && grid[move.X, move.Y] == 0;
        }

        public bool ExecuteMove((int X,int Y) move, out SymbolEnum? moved)
        {
            if(grid[move.X, move.Y] == 0){
                moved = moveCount++ % 2 == 0 ? SymbolEnum.X : SymbolEnum.O;
                
                grid[move.X, move.Y] = (int)moved;
                
                SymbolEnum? winner;
                if(isGameOver(out winner)){
                    this.GameEndedEvent.Invoke(this, new WinnerEventArgs(winner));
                }
                
                return true;
            }
            moved = null;
            return false;
        }

        private bool isGameOver(out SymbolEnum? winner)
        {
            winner = moveCount % 2 == 1 ? SymbolEnum.X : SymbolEnum.O;
            Debug.Log("check if is game over");
            //test rows
            for (var x = 0; x < 3; x++) {
                var total = 0;
                for (var y = 0; y < 3; y++)
                    if (Math.Abs(total += grid[x, y]) == 3)
                        return true;
                    
            }

            //test cols
            for (var y = 0; y < 3; y++) {
                var total = 0;
                for (var x = 0; x < 3; x++)
                    if (Math.Abs(total += grid[x, y]) == 3)
                        return true;
            }

            //test / diagnal
            var forwardSlashTotal = grid[0, 2] + grid[1, 1] + grid[2, 0];
            if (forwardSlashTotal == 3)
                return true;
            else if (forwardSlashTotal == -3)
                return true;

            //test \ diagnal
            var backSlashTotal = grid[0, 0] + grid[1, 1] + grid[2, 2];
            if (backSlashTotal == 3)
                return true;
            else if (backSlashTotal == -3)
                return true;



            for(var x = 0; x < 3; x++)
                for(var y = 0; y < 3; y++)
                    if(grid[x,y] == 0)
                        return false;
                
            winner = null;
            return true;
        }

        public bool isMoveX()
        {
            return this.moveCount % 2 == 0;
        }
    }
}