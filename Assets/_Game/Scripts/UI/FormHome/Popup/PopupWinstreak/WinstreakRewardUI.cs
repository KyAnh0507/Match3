using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.UI;

public class WinstreakRewardUI : MonoBehaviour
{
    public TMP_Text index;
    public Image image;
    public TMP_Text amount;
    public GameObject claimed;
    public WinstreakRewardData reward;
    public Button button;
    public Image fill1;
    public Image fill2;

    public RectTransform icon;
    public void SetupData(WinstreakRewardData winstreakRewardData, bool b1, bool b2, int i)
    {
        if (winstreakRewardData.target < DataManager.Ins.dataSaved.maxWinstreak && !b1 && !b2)
        {
            fill1.fillAmount = 1;
            fill2.fillAmount = 1;
        } else if (winstreakRewardData.target < DataManager.Ins.dataSaved.maxWinstreak && b1)
        {
            fill1.fillAmount = 1;
            fill2.fillAmount = 0;
            DOVirtual.Float(0, 1, 0.35f, value =>
            {
                fill2.fillAmount = value;
            });
        }
        else if (winstreakRewardData.target <= DataManager.Ins.dataSaved.maxWinstreak && b2)
        {
            fill1.fillAmount = 0;
            fill2.fillAmount = 0;

            DOVirtual.DelayedCall(0.35f, () =>
            {
                DOVirtual.Float(0, 1, 0.15f, value =>
                {
                    fill1.fillAmount = value;
                });
            });
        }
        if (DataManager.Ins.dataSaved.statusWinstreak[i] || winstreakRewardData.target > DataManager.Ins.dataSaved.maxWinstreak)
        {
            button.enabled = false;
        }
        else
        {
            button.enabled = true;
        }
        if (DataManager.Ins.dataSaved.statusWinstreak[i])
        {
            Claimed();
        }
        index.text = winstreakRewardData.target + "";
        reward = winstreakRewardData;
        image.sprite = winstreakRewardData.sprite;
        if (winstreakRewardData.rewards.Count == 1)
        {
            amount.text = "X" + winstreakRewardData.rewards[0].amount;
        }
        image.SetNativeSize();
        if (winstreakRewardData.rewards[0].rewardType == RewardType.Gems)
        {
            image.transform.localScale = Vector3.one * 0.5f;
        }
        else if (winstreakRewardData.rewards[0].rewardType != RewardType.Coin)
        {
            image.transform.localScale = Vector3.one;
        }
    }
    public void Claim()
    {
        Claimed();
        if (reward.rewards.Count == 1)
        {
            switch (reward.rewards[0].rewardType)
            {
                case RewardType.Coin:
                    DataManager.Ins.ChangeCoin(reward.rewards[0].amount);
                    UIManager.Ins.formHome.popupWinstreak.CollectCoinReward(image.transform.position);

                    break;
                case RewardType.Gems:
                    DataManager.Ins.ChangeGem(reward.rewards[0].amount);
                    UIManager.Ins.formHome.popupWinstreak.CollectGemReward(image.transform.position);
                    break;
                case RewardType.Add1Tile:
                    DataManager.Ins.dataSaved.boosterAdd1 += reward.rewards[0].amount;
                    UIManager.Ins.formHome.popupWinstreak.CollectAdd1Reward(image.transform.position);
                    break;
                case RewardType.DeleteIron:
                    DataManager.Ins.dataSaved.boosterBomb += reward.rewards[0].amount;
                    UIManager.Ins.formHome.popupWinstreak.CollectDeleteReward(image.transform.position);
                    break;
                case RewardType.Shuffle:
                    DataManager.Ins.dataSaved.boosterSuffer += reward.rewards[0].amount;
                    UIManager.Ins.formHome.popupWinstreak.CollectShuffleReward(image.transform.position);
                    break;
                case RewardType.Undo:
                    DataManager.Ins.dataSaved.boosterUndo += reward.rewards[0].amount;
                    UIManager.Ins.formHome.popupWinstreak.CollectUndoReward(image.transform.position);
                    break;
            }
        }
        DataManager.Ins.dataSaved.statusWinstreak[int.Parse(index.text)] = true;
    }

    public void Claimed()
    {
        claimed.gameObject.SetActive(true);
    }
}
