using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TicTacToeGrid;

public class Player
{

     public Material SelectedMaterial {get; private set;}
     public virtual bool ChooseMove(TicTacToeGrid grid, Piece piece, out  int[] cooridinates){
          
          cooridinates = new [] {-1,-1};
          return false;
      }

     public Player(Material material){
          this.SelectedMaterial = material;
     }

}
