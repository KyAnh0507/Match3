using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : Singleton<GameConfig>
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public enum Scene { Loading, Home, Game, GameColorPencil}


public static class TimeConfig
{
    public static DateTime startTime = new DateTime(2024, 1, 1);
}

