using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "ColorDatas", menuName = "ScriptableObjects/ColorDatas", order = 1)]
public class ColorDatas : ScriptableObject
{
    public List<ColorData> colorData;

    public Color GetColor(ColorType colorType)
    {
        return colorData.Single(q => q.colorType == colorType).color;
    }
}

[System.Serializable]
public class ColorData
{
    public ColorType colorType;
    public Color color;
}

public enum ColorType
{
    Orange = 0,
    Green = 1,
    Pink = 2,
    Blue = 3,
    Yellow = 4,
    Purple = 5,
    Red = 6,
    DarkBlue = 7,
    Gray = 8,
    While = 9,
}
