using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    public int moveCount {get; private set;}
    public static MainManager Instance { get; private set; }
    private TicTacToeGrid grid;
    public Player PlayerX {get; private set;}
    public Player PlayerY {get; private set;}

    public GameObject oPrfab;
    public GameObject xPrefab;

    public void ResetGame(Player playerX, Player playerY){
        Debug.Log("rest game");
        this.moveCount = 0;
        this.grid = new TicTacToeGrid();
        this.PlayerX = playerX;
        this.PlayerY = playerY;
    }

    public bool ExecuteMove(int x, int y){
        if(this.grid.Board[ x, y] == 0){
            var piece = moveCount++ % 2 == 1 ? TicTacToeGrid.Piece.O : TicTacToeGrid.Piece.X;
            this.grid.Board[x,y] = (int)piece;
           
                    
            var square = GetSquare(x,y);
            
            var prefab = piece == TicTacToeGrid.Piece.O  ? oPrfab : xPrefab;
            if(moveCount % 2 == 1)
            {       
                Array.ForEach(prefab.GetComponentsInChildren<MeshRenderer>(), m => m.material = PlayerX.SelectedMaterial);
            }
            else
            {
            prefab.GetComponent<MeshRenderer>().material =  PlayerY.SelectedMaterial;    
            }
     
            Instantiate(prefab, square.gameObject.transform.position, prefab.transform.rotation);

            Debug.Log(this.grid.Board);
            return true;
        }
        return false;
    }

    GameObject GetSquare(int x, int y){
        var boxes = GameObject.FindObjectsOfType<BoxClicker>();
        for(var i = 0; i< boxes.Length; i++)
            if(boxes[i].X == x && boxes[i].Y == y){
                boxes[i].playerColor = Color.cyan;
                return boxes[i].gameObject;
            }
                

        return null;
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
    void Start()
    {
        Debug.Log("start main manager");

     
    }

    // Update is called once per frame
    void Update()
    {

    }
}
