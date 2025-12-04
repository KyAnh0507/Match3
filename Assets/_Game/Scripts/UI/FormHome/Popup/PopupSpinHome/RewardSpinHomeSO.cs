using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardSpinHomeSO", menuName = "ScriptableObjects/RewardSpinHomeScriptableObject", order = 1)]
public class RewardSpinHomeSO : ScriptableObject
{
    public List<RewardSpinHomeData> datas;
}

[System.Serializable]
public class RewardSpinHomeData
{
    public RewardType Type;
    public Sprite image;
    public int amount;
}
