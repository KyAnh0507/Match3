using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSpinHome : MonoBehaviour
{
    public Image image;
    public TMP_Text text;
    public RewardSpinHomeData reward;
    public void SetupReward(RewardSpinHomeData reward)
    {
        this.reward = reward;
        image.sprite = reward.image;
        text.text = "" + reward.amount;
    }
}
