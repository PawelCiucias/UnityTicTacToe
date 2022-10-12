using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{

     public Material SelectedMaterial {get; private set;}
     public virtual void MakeMove(){}

     public Player(Material material){
          this.SelectedMaterial = material;
     }

}
