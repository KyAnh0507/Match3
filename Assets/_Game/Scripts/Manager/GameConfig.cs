using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameConfig : Singleton<GameConfig>
{
    public List<ThemeGame> themeGames;

    private void OnEnable()
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

