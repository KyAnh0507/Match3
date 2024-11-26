using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardChallengeButton : MonoBehaviour
{
    public Text amount;
    public Text target;
    public GameObject highlight;
    public GameObject tick;
    public bool isClaimed = false;
    public RewardChallenge reward;
    public int order;
    public void SetTarget(int target)
    {
        this.target.text = target.ToString();
    }

    public void SetAmount(int amount)
    {
        if (this.amount != null)
        {
            this.amount.text = "x" + amount.ToString();

        }
    }

    public void ActiveHighlight(bool b)
    {
        if (DataManager.Ins.dataSaved.statusReward[order])
        {
            tick.SetActive(true);
            highlight.SetActive(false);
        }
        else
        {
            tick.SetActive(false);
            highlight.SetActive(true);
        }
    }
}
