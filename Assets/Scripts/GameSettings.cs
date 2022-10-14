using System.Collections;
using System.Collections.Generic;
using TicTacToe.Interfaces;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings Instance { get; private set; }
    public IPlayer PlayerX { get; private set; }
    public IPlayer PlayerO { get; private set; }

    public void Init(IPlayer playerX, IPlayer playerO)
    {
        this.PlayerX = playerX;
        this.PlayerO = playerO;
    }

    private void Awake()
    {
        if (GameSettings.Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
