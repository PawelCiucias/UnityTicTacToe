using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TicTacToe.Interfaces;
using TicTacToe.Constants;

namespace TicTacToe.Models
{
    public class PlayerAiMedium : PlayerAiEasy
    {
        public PlayerAiMedium(Material material) : base(material)
        {
        }

        public override bool ChooseMove(IGame game, SymbolEnum piece, out (int X, int Y) move)
        {
            if (game.MoveCount < 3)
                return base.ChooseMove(game, piece, out move);

            //b = 0 try to win
            //b = 1 try to block
            for (var b = 0; b < 2; b++)
            {
                var multiplier = ((game.MoveCount + b) & 1) == 0 ? 1 : -1;
                //cols
                for (var x = 0; x < 3; x++)
                {
                    var total = 0;
                    int yCandidate = -1;

                    for (var y = 0; y < 3; y++)
                    {
                        total += game.Grid[x, y];
                        if (game.Grid[x, y] == 0)
                            yCandidate = y;
                        if (y == 2 && total == (2 * multiplier)){
                            move = (x, yCandidate);
                            return true;
                        }
                    }
                }

                //rows
                for (var y = 0; y < 3; y++)
                {
                    var total = 0;
                    int xCandidate = -1;

                    for (var x = 0; x < 3; x++)
                    {
                        total += game.Grid[x, y];
                        if (game.Grid[x, y] == 0)
                            xCandidate = x;
                        if (x == 2 && total == (2 * multiplier)){
                            move = (xCandidate, y);
                            return true;
                        }
                    }
                }

                //front slash /
                var fsTotal = 0;
                (int X, int Y) fsCanddiate = (-1, -1);

                for (int x = 0, y = 2; x < 3; x++, y--)
                {
                    fsTotal += game.Grid[x, y];
                    if (game.Grid[x, y] == 0)
                        fsCanddiate = (x, y);
                    if (x == 2 && fsTotal == (2 * multiplier)){
                        move = fsCanddiate;
                        return true;
                    }
                }

                //backslash \ win
                var bsTotal = 0;
                (int X, int Y) bsCanddiate = (-1, -1);
                for (int x = 0; x < 3; x++)
                {
                    bsTotal += game.Grid[x, x];
                    if (game.Grid[x, x] == 0)
                        bsCanddiate = (x, x);
                    if (x == 2 && bsTotal == (2 * multiplier)){
                        move = bsCanddiate;
                        return true;
                    }
                }
            }

            return base.ChooseMove(game, piece, out move);
            
        }
    }
}