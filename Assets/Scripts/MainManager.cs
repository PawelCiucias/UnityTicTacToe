using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    private int moveCount;
    public static MainManager Instance { get; private set; }
    private TicTacToeGrid grid;
    public Player PlayerX { get; private set; }
    public Player PlayerO { get; private set; }

    public GameObject oPrfab;
    public GameObject xPrefab;


    public GameObject  GameOverPanel;
    public TextMeshProUGUI text;

    public void ResetGame(Player playerX, Player playerO)
    {
        Debug.Log("rest game");
        this.moveCount = 0;
        this.grid = new TicTacToeGrid();
        this.PlayerX = playerX;
        this.PlayerO = playerO;

        Debug.Log($"Player X is {playerX.GetType()}");
        Debug.Log($"Player O is {playerO.GetType()}");
    }

    public TicTacToeGrid.Piece getPieceToMove()
    {
        return moveCount % 2 == 1 ? TicTacToeGrid.Piece.O : TicTacToeGrid.Piece.X;
    }

    public bool IsAiMove()
    {
        if (getPieceToMove() == TicTacToeGrid.Piece.X)
            return PlayerX.GetType() != typeof(Player);
        return PlayerO.GetType() != typeof(Player);
    }
    public bool ExecuteMove(int x, int y)
    {
        Debug.Log("execute move" + this.moveCount);
        if (this.grid.Board[x, y] == 0)
        {
            var pieceToMove = getPieceToMove();
            this.grid.Board[x, y] = (int)pieceToMove;

            var square = GetSquare(x, y);
            var prefab = pieceToMove == TicTacToeGrid.Piece.O ? oPrfab : xPrefab;

            if (pieceToMove == TicTacToeGrid.Piece.X)
                Array.ForEach(prefab.GetComponentsInChildren<MeshRenderer>(), m => m.material = PlayerX.SelectedMaterial);
            else
                prefab.GetComponent<MeshRenderer>().material = PlayerO.SelectedMaterial;

            Instantiate(prefab, square.gameObject.transform.position, prefab.transform.rotation);
            this.isGameOver = (++this.moveCount) > 8;
            Debug.Log("move count is now " + this.moveCount);
            return true;
        }
        return false;
    }

    GameObject GetSquare(int x, int y)
    {
        var boxes = GameObject.FindObjectsOfType<BoxClicker>();
        for (var i = 0; i < boxes.Length; i++)
            if (boxes[i].X == x && boxes[i].Y == y)
                return boxes[i].gameObject;
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
    
    bool WaitForAiToMove = false;
    public bool isGameOver {get; private set; } = false;
    void Update()
    {
        if(this.GameOverPanel != null)
            this.GameOverPanel.SetActive(this.isGameOver);
        
        if (PlayerX == null || PlayerO == null || WaitForAiToMove || isGameOver)
            return;

        if (getPieceToMove() == TicTacToeGrid.Piece.X && PlayerX.GetType() != typeof(Player))
        {
            WaitForAiToMove = true;
            int[] coordinates;
            Debug.Log("AI to move Over:" + this.isGameOver + " move count = " + this.moveCount);
            if (PlayerX.ChooseMove(this.grid, TicTacToeGrid.Piece.X, out coordinates))
                StartCoroutine("AiMove", coordinates);
        }

        else if (getPieceToMove() == TicTacToeGrid.Piece.O && PlayerO.GetType() != typeof(Player))
        {
            WaitForAiToMove = true;
            int[] coordinates;
            Debug.Log("AI to move Over:" + this.isGameOver + " move count = " + this.moveCount);
            if (PlayerO.ChooseMove(this.grid, TicTacToeGrid.Piece.O, out coordinates))
                StartCoroutine("AiMove", coordinates);
        }

    }

    IEnumerator AiMove(object coordinates)
    {
        var c = (int[])coordinates;

        yield return new WaitForSeconds(2);

        if (ExecuteMove(c[0], c[1]))
        {
            var square = GetSquare(c[0], c[1]);

            square.GetComponent<BoxClicker>().blowUpSquare(TicTacToeGrid.Piece.O);
            WaitForAiToMove = false;
        }
    }

    
    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);
        this.GameOverPanel = GameObject.FindGameObjectWithTag("GameOver_PNL");
        this.GameOverPanel.SetActive(false);
    }
}
