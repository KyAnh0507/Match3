using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState { GAMEPLAY, FINISH}
public class GameManager : Singleton<GameManager>
{
    public GameState state;
    // Start is called before the first frame update
    void OnEnable()
    {
        DataManager.Ins.StartData();
    }

    public bool IsState(GameState gameState) => gameState == state;

    public void ChangeState(GameState gameState)
    {
        state = gameState;
    }
}
