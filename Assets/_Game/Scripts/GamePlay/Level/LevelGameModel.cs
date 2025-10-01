using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData_", menuName = "Data/LevelDataAsset", order = 1)]
public class LevelGameModel : ScriptableObject
{
    public int level;
    public LevelModel levelModel;
}


[System.Serializable]
public struct LevelModel
{
    public int soLayer;
    public Hole1Model[] holeModels;
    public IronMode[] ironModes;
    public KeyModel[] keyModels;

    public float boardIncreaseSize;
}

[System.Serializable]
public struct Hole1Model
{
    public ItemTransModel transModel;
    public int screwType;
    public bool hasScrew;
    public bool hasLock;
    public bool hasIce;
    public int iceUnlockNuber;
    public bool hasLockNumbScrew;
    public int numbLockIron;
}

[System.Serializable]
public struct IronMode
{
    public int id;
    public Hole1Model[] holeModels;
    public ItemTransModel transModel;
    public int layer;
    public int orderLayer;
    public string spriteName;
    public Vector2[] polygonColliderPoints;
    public bool hasIce;
}
[System.Serializable]
public struct KeyModel
{
    public int id;
    public ItemTransModel transModel;
    public int holeId;
}

[System.Serializable]
public struct ItemTransModel
{
    public float3 position;
    public float3 localScale;
    public float3 rotation;

    public ItemTransModel(float3 _p, float3 _scale, float3 _r)
    {
        this.position = _p;
        this.localScale = _scale;
        this.rotation = _r;
    }
}