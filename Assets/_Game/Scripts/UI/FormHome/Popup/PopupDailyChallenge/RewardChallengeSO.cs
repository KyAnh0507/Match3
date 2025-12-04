using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RewardChallengeSO", menuName = "ScriptableObjects/RewardChallengeScriptableObject", order = 1)]
public class RewardChallengeSO : ScriptableObject
{
    public List<RewardChallenge> rewardDatas;
}

[System.Serializable]
public class RewardChallenge
{
    public RewardType rewardType;
    public int amount1;
    public int amount2;
    public int numberTarget;
}