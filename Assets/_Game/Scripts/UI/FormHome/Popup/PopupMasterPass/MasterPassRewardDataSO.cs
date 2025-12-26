using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MasterPassRewardDataScriptableObject", menuName = "ScriptableObjects/MasterPassRewardDataScriptableObject", order = 1)]
public class MasterPassRewardDataSO : ScriptableObject
{
    public List<MasterPassRewardData> rewardDatas;
}

[System.Serializable]
public class MasterPassRewardData
{
    public Sprite sprite;
    public RewardData rewardData;
}