using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : Singleton<GameConfig>
{
    public List<ThemeGame> themeGames;
    // Start is called before the first frame update
    void Awake()
    {
        
    }
}

[System.Serializable]
public class ThemeGame
{
    public List<Sprite> sprites;
}
public enum Scene { Loading, Home, Game, GameColorPencil}


public static class TimeConfig
{
    public static DateTime startTime = new DateTime(2024, 1, 1);
}

