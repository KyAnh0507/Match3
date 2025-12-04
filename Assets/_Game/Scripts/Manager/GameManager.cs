using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { GAMEPLAY, FINISH}
public class GameManager : Singleton<GameManager>
{
    public GameState state;
    private DateTime startupTime;
    // Start is called before the first frame update
    void OnEnable()
    {
        DataManager.Ins.StartData();
    }

    public DateTime Now()
    {
        return DateTime.Now;
    }

    public bool IsState(GameState gameState) => gameState == state;

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }
}
