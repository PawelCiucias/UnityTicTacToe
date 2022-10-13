using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using static TicTacToeGrid;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI PlayerX;
    public TextMeshProUGUI PlayerY;

    public Button BlueXButton;
    public Button PinkXButton;
    public Button PurpleXButton;
    public Button YellowXButton;

    public Button BlueYButton;
    public Button PinkYButton;
    public Button PurpleYButton;
    public Button YellowYButton;

    private List<Button> yButtons;
    private List<Button> xButtons;
    Material playerMaterialX;
    Material playerMaterialY;
    public GameObject ContinueButton;
    public Button NewGameButton;

    public void StartGame()
    {
        Debug.Log("start game clicked");
        var playerX = GetPlayerType(PlayerX.text, Piece.X);
        var playerY = GetPlayerType(PlayerY.text, Piece.O);

        MainManager.Instance.ResetGame(playerX, playerY);
        SceneManager.LoadScene(1);

    }


    private Player GetPlayerType(string player, Piece p)
    {
        var m = p == Piece.X ? playerMaterialX : playerMaterialY;

        switch(player.ToLower()){
            case "ai easy":
                return new PlayerAiEasy(m);
        }
        return new Player(m);
    }

    // Start is called before the first frame update
    void Start()
    {
        xButtons = new List<Button>(new[] { BlueXButton, PinkXButton, PurpleXButton, YellowXButton });
        yButtons = new List<Button>(new[] { BlueYButton, PinkYButton, PurpleYButton, YellowYButton });

    }
    Action<Button> dimButton = (b) =>
    {
        var c = b.GetComponent<Image>().color;
        b.GetComponent<Image>().color = new Color(c.r, c.g, c.b, .1f);
    };

    Action<Button> disableButton = (b) =>
    {
        var c = b.GetComponent<Image>().color;
        b.enabled = false;
        b.GetComponent<Image>().color = new Color(c.r, c.g, c.b, .05f);
    };

    Action<Button> enableButton = (b) =>
    {
        var c = b.GetComponent<Image>().color;
        b.enabled = true;
        b.GetComponent<Image>().color = new Color(c.r, c.g, c.b, 1f);
    };

    
    public void SetPlayerXColour_Clicked(Material material)
    {
        var color = material.color;
        playerMaterialX = material;

   
        xButtons.ForEach(dimButton);
        
        switch (ColorUtility.ToHtmlStringRGB(material.color))
        {
            
            case "00E4FF":
                enableButton(BlueXButton);
                
               // disableButton(BlueYButton);
                break;
            case "F100EC":
                enableButton(PinkXButton);
             //   disableButton(PinkYButton);
                Debug.Log("pink");
                break;
            case "6300FF":
                enableButton(PurpleXButton);
             //   disableButton(PurpleYButton);
                Debug.Log("purple");
                break;
            case "FFDE00":
                enableButton(YellowXButton);
             //   disableButton(YellowYButton);

                Debug.Log("yellow");
                break;
        }


    }

    public void SetPlayerYColour_Clicked(Material material)
    {
        playerMaterialY = material;
        var color = material.color;
        
      //  xButtons.ForEach(enableButton);
        yButtons.ForEach(dimButton);
        
        switch (ColorUtility.ToHtmlStringRGB(material.color))
        {
            case "00E4FF":
                enableButton(BlueYButton);
             //   disableButton(BlueXButton);
                break;
            case "F100EC":
                enableButton(PinkYButton);
             //   disableButton(PinkXButton);
                Debug.Log("pink");
                break;
            case "6300FF":
                enableButton(PurpleYButton);
            //    disableButton(PurpleXButton);
                Debug.Log("purple");
                break;
            case "FFDE00":
                enableButton(YellowYButton);
            //    disableButton(YellowXButton);

                Debug.Log("yellow");
                break;
        }
    }

    public void ExitGame()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Awake()
    {
        var continueButtonExists = ContinueButton != null;
        var mainManagerIsNull = MainManager.Instance == null;

        if (continueButtonExists && mainManagerIsNull)
            ContinueButton.SetActive(false);
    }


    // Update is called once per frame
    void Update()
    {
        if(this.NewGameButton.enabled = (this.playerMaterialX != null && this.playerMaterialY != null))
            enableButton(this.NewGameButton);
        else
            disableButton(this.NewGameButton);
        
    }
}
