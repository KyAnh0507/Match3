using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardDataSO", menuName = "ScriptableObjects/RewardDataScriptableObject", order = 1)]
public class RewardDataSO : ScriptableObject
{
    public List<RewardData> rewardDatas;
}

[System.Serializable]
public class RewardData
{
    public bool isClaim;
    public RewardType rewardType;
    public int amount;
}

