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
    public void SetupData(WinstreakRewardData winstreakRewardData, int i)
    {
        if (i < DataManager.Ins.dataSaved.maxWinstreak)
        {
            fill1.fillAmount = 0;
            fill2.fillAmount = 0;

            DOVirtual.DelayedCall(i * 0.38f, () =>
            {
                DOVirtual.Float(0, 1, 0.08f, value =>
                {
                    fill1.fillAmount = value;
                });
            });
            if (fill2 != null)
            {
                DOVirtual.DelayedCall(i * 0.38f + 0.03f, () =>
                {
                    DOVirtual.Float(0, 1, 0.3f, value =>
                    {
                        fill2.fillAmount = value;
                    });
                });
            }
        }else if (i == DataManager.Ins.dataSaved.maxWinstreak)
        {
            fill1.fillAmount = 0;
            fill2.fillAmount = 0;

            DOVirtual.DelayedCall(i * 0.38f, () =>
            {
                DOVirtual.Float(0, 1, 0.08f, value =>
                {
                    fill1.fillAmount = value;
                });
            });
        }
        if (DataManager.Ins.dataSaved.statusWinstreak[i] || i > DataManager.Ins.dataSaved.maxWinstreak)
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
        index.text = i + "";
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
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.gameObject.SetActive(true);
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.attractorTarget = UIManager.Ins.formHome.coinUI.IconTf;
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.Play();
                    UIManager.Ins.SetActiveBlock(true);
                    UIManager.Ins.formHome.SetOverrideCoin(true);
                    UIManager.Ins.formHome.popupWinstreak.flyCoin.onLastParticleFinish.AddListener(() =>
                    {
                        UIManager.Ins.formHome.SetOverrideCoin(false);
                        UIManager.Ins.SetActiveBlock(false);
                        Claimed();
                        UIManager.Ins.formHome.popupWinstreak.flyCoin.gameObject.SetActive(false);
                    });

                    break;
                case RewardType.Gems:
                    DataManager.Ins.ChangeGem(reward.rewards[0].amount);
                    UIManager.Ins.formHome.popupWinstreak.flyGem.transform.position = image.transform.position;
                    UIManager.Ins.formHome.popupWinstreak.flyGem.gameObject.SetActive(true);
                    UIManager.Ins.formHome.popupWinstreak.flyGem.attractorTarget = UIManager.Ins.formHome.gemUI.IconTf;
                    UIManager.Ins.formHome.popupWinstreak.flyGem.Play();
                    UIManager.Ins.SetActiveBlock(true);
                    UIManager.Ins.formHome.SetOverrideGem(true);
                    UIManager.Ins.formHome.popupWinstreak.flyGem.onLastParticleFinish.AddListener(() =>
                    {
                        UIManager.Ins.formHome.SetOverrideGem(false);
                        UIManager.Ins.SetActiveBlock(false);
                        Claimed();
                        UIManager.Ins.formHome.popupWinstreak.flyGem.gameObject.SetActive(false);

                    });
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
