using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WinstreakRewardDataSO", menuName = "ScriptableObjects/WinstreakRewardDataScriptableObject", order = 1)]
public class WinstreakRewardDataSO : ScriptableObject
{
    public List<WinstreakRewardData> rewardDatas;
}

[System.Serializable]
public class WinstreakRewardData
{
    public int target;
    public Sprite sprite;
    public List<RewardData> rewards;
}

