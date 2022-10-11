using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuHandler : MonoBehaviour
{
    public TextMeshProUGUI PlayerX;
    public TextMeshProUGUI PlayerY;

    public GameObject ContinueButton;
    
    public void StartGame(){
        Debug.Log("start game clicked");
        var playerX = GetPlayerType(PlayerX.text);
        var playerY = GetPlayerType(PlayerY.text);

        MainManager.Instance.ResetGame(playerX, playerY);
        SceneManager.LoadScene(1);
    
    }


    private Player GetPlayerType(string player){


        return new Player();
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
        
        if(continueButtonExists && mainManagerIsNull){
            ContinueButton.SetActive(false);
            Debug.Log("hid the continue button");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(ContinueButton != null && MainManager.Instance.PlayerX != null && MainManager.Instance.PlayerY != null){
   
            
        
        }
    }


     IEnumerator showContinueButton(){
        yield return new WaitForSeconds(1);
    
    }
}
