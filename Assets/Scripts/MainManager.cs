using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

using TicTacToe.Constants;
using TicTacToe.Interfaces;
using TicTacToe.Models;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{

    private IGame currentGame;
    public GameObject Reset_PNL;

    public bool isGameOver {get;  private set;}


    public void NewGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);  
    }
    public void ResetGame()
    {
        Reset_PNL.SetActive(false);
        if(this.currentGame != null)
            this.currentGame.GameEndedEvent -= GameOverEvent;

        this.currentGame = new Game();
        this.currentGame.GameEndedEvent += GameOverEvent;
        this.isGameOver = false;
   
    }

    private void GameOverEvent (object sender, WinnerEventArgs args){
        Reset_PNL.SetActive(true);
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
        var gs = GameSettings.Instance;
        if (currentGame.isMoveX())
            return gs.PlayerX.GetType() != typeof(Player);
        return gs.PlayerO.GetType() != typeof(Player);
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

  
    void Start(){
        ResetGame();
    }

    // Update is called once per frame
    
    bool WaitForAiToMove = false;

    void Update()
    {
        var gs = GameSettings.Instance;
        if (gs.PlayerX == null || gs.PlayerO == null || WaitForAiToMove)
            return;

        if (IsAiMove())
        {
            WaitForAiToMove = true;
            (int X, int Y) move;
            if(currentGame.isMoveX() && gs.PlayerX.ChooseMove(this.currentGame, SymbolEnum.X, out move))
                StartCoroutine("AiMove", (move, SymbolEnum.X));
            else if(!currentGame.isMoveX() && gs.PlayerO.ChooseMove(this.currentGame, SymbolEnum.O, out move))
                StartCoroutine("AiMove", (move, SymbolEnum.O));
            else
                WaitForAiToMove = false;
                
        }
    }

    IEnumerator AiMove(object data)
    {
        var d = (((int X,int Y) move, SymbolEnum symbol)) data;

        yield return new WaitForSeconds(1);

        if (ExecuteMove((d.move.X, d.move.Y)) != null)
        {
            var square = GetSquare(d.move.X, d.move.Y);

            square.GetComponent<BoxClicker>().blowUpSquare(d.symbol);
            WaitForAiToMove = false;
        }
    }

    
 
}
