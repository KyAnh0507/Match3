using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRewardSpinHome : MonoBehaviour
{
    public Image image;
    public Text text;
    
    public void SetupReward(RewardSpinHomeData reward)
    {
        image.sprite = reward.image;
        text.text = "x" + reward.amount;
    }
}
