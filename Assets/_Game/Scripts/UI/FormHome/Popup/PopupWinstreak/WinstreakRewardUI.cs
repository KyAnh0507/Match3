using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class WinstreakRewardUI : MonoBehaviour
{
    public Image image;
    public TMP_Text amount;
    public GameObject block;
    public GameObject claimed;
    public WinstreakRewardData reward;
    public void SetupData(WinstreakRewardData winstreakRewardData)
    {
        reward = winstreakRewardData;
        image.sprite = winstreakRewardData.sprite;
        if (winstreakRewardData.rewards.Count == 1)
        {
            amount.text = "X" + winstreakRewardData.rewards[0].amount;
        }
    }
    public void Claim()
    {
        Claimed();
        DataManager.Ins.dataSaved.isClaimDailyReward = true;
        if (reward.rewards.Count == 1)
        {
            switch (reward.rewards[0].rewardType)
            {
                case RewardType.Coin:
                    DataManager.Ins.ChangeCoin(reward.rewards[0].amount);
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.transform.position = image.transform.position;
                    UIManager.Ins.SetActiveBlock(true);
                    UIManager.Ins.formHome.SetOverrideCoin(true);
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.Play();
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.onLastParticleFinish.AddListener(() =>
                    {
                        UIManager.Ins.formHome.SetOverrideCoin(false);
                        UIManager.Ins.SetActiveBlock(false);
                        Claimed();
                    });

                    break;
                case RewardType.Gems:
                    DataManager.Ins.ChangeGem(reward.rewards[0].amount);
                    UIManager.Ins.formHome.popupWinstreak.flyGem.transform.position = image.transform.position;
                    UIManager.Ins.SetActiveBlock(true);
                    UIManager.Ins.formHome.SetOverrideGem(false);
                    UIManager.Ins.formHome.popupWinstreak.flyGem.Play();
                    UIManager.Ins.formHome.popupWinstreak.flyGem.onLastParticleFinish.AddListener(() =>
                    {
                        UIManager.Ins.formHome.SetOverrideGem(true);
                        UIManager.Ins.SetActiveBlock(false);
                        Claimed();
                    });
                    break;
            }
        }
    }

    public void Claimed()
    {
        claimed.gameObject.SetActive(true);
    }

    public void Block()
    {
        block.gameObject.SetActive(false);
    }
}
