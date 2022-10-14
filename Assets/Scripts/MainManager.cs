using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using TicTacToe.Constants;
using TicTacToe.Interfaces;
using TicTacToe.Models;

public class MainManager : MonoBehaviour
{
    public static MainManager Instance { get; private set; }
    
    public IPlayer PlayerX { get; private set; }
    public IPlayer PlayerO { get; private set; }

    private IGame currentGame;

    public bool isGameOver {get;  private set;}

    public void ResetGame(IPlayer playerX, IPlayer playerO)
    {
        this.PlayerX = playerX;
        this.PlayerO = playerO;

        if(this.currentGame != null)
            this.currentGame.GameEndedEvent -= GameOverEvent;

        this.currentGame = new Game();
        this.currentGame.GameEndedEvent += GameOverEvent;
        this.isGameOver = false;
        

        Debug.Log($"Player X is {playerX.GetType()}");
        Debug.Log($"Player O is {playerO.GetType()}");
    }

    private void GameOverEvent (object sender, WinnerEventArgs args){
        var gameOver = GameObject.FindGameObjectWithTag("GameOver_TXT").GetComponent<TextMeshProUGUI>();
        gameOver.text = "GAME OVER!!";
        this.isGameOver = true;
        var gameResult = GameObject.FindGameObjectWithTag("GameResult_TXT").GetComponent<TextMeshProUGUI>();
        
        if(args.Winner.HasValue)
            gameResult.text = $"{args.Winner.Value} is the winner!!!";
        else
            gameResult.text = $"It's a tie game";

    }
    public bool IsAiMove()
    {
        if (currentGame.isMoveX())
            return PlayerX.GetType() != typeof(Player);
        return PlayerO.GetType() != typeof(Player);
    }

    public SymbolEnum? ExecuteMove((int X,int Y) move)
    {
        SymbolEnum? moved;
        if(!isGameOver && currentGame.ExecuteMove(move, out moved)){
            Debug.Log("executed move " + moved);
            return moved;
        }
        return null;
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
        if (MainManager.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    
    bool WaitForAiToMove = false;

    void Update()
    {
        if (PlayerX == null || PlayerO == null || WaitForAiToMove)
            return;

        if (IsAiMove())
        {
            WaitForAiToMove = true;
            (int X, int Y) move;
            if(currentGame.isMoveX() && PlayerX.ChooseMove(this.currentGame, SymbolEnum.X, out move))
                StartCoroutine("AiMove", (move, SymbolEnum.X));
            else if(!currentGame.isMoveX() && PlayerO.ChooseMove(this.currentGame, SymbolEnum.O, out move))
                StartCoroutine("AiMove", (move, SymbolEnum.O));
            else
                WaitForAiToMove = false;
                
        }
    }

    IEnumerator AiMove(object data)
    {
        var d = (((int X,int Y) move, SymbolEnum symbol)) data;

        yield return new WaitForSeconds(2);

        if (ExecuteMove((d.move.X, d.move.Y)) != null)
        {
            var square = GetSquare(d.move.X, d.move.Y);

            square.GetComponent<BoxClicker>().blowUpSquare(d.symbol);
            WaitForAiToMove = false;
        }
    }

    
 
}
