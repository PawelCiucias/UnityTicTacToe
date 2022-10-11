using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    private int moveCount;
    public static MainManager Instance { get; private set; }
    private TicTacToeGrid grid;
    public Player PlayerX {get; private set;}
    public Player PlayerY {get; private set;}
    

    public void ResetGame(Player playerX, Player playerY){
        Debug.Log("rest game");
        this.moveCount = 0;
        this.grid = new TicTacToeGrid();
        this.PlayerX = playerX;
        this.PlayerY = playerY;
    }

    private void Awake()
    {
        Debug.Log("create main manager");
        if (MainManager.Instance == null)
        {
            Instance = this;

            //DontDestorOnLoad marks the MainManager GameObject attached to this script 
            //not to be destroyed when the scene changes.
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
