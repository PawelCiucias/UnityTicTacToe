using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToeGrid
{
    public enum Piece
    {
        X = -1,
        O = 1
    }
    int[,] board = new int[3, 3];

    public int[,] Board { get { return board; } }

    public bool InputValue(int x, int y, Piece p)
    {
        Func<int, bool> inRangePredicate = (int v) => v > -1 && v < 3;

        if (inRangePredicate(x) && inRangePredicate(y) && board[x, y] == 0) {
            board[x, y] = (int)p;
            return true;
        }

        return false;
    }


    // Update is called once per frame
    void Update()
    {

    }

}
