using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TicTacToeGrid;

public class PlayerAiEasy : Player
{
    public PlayerAiEasy(Material material) : base(material)
    {
    }

    public override bool ChooseMove(TicTacToeGrid grid, Piece piece, out int[] coordinates)
    {
        var randomX = -1;
        var randomY = -1;

        do
        {
            randomX = Random.Range(0, 3);
            randomY = Random.Range(0, 3);
            Debug.Log($"x = {randomX} y = {randomY} isOver = {MainManager.Instance.isGameOver}");
            if(MainManager.Instance.isGameOver){
                coordinates = new [] {-1, -1};
                return false;
            }
                

        }
        while (!grid.isValidMove(randomX, randomY));

        coordinates = new [] {randomX, randomY};
        return true;
    }
}
