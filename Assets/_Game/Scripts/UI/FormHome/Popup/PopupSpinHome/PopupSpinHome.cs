using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupSpinHome : MonoBehaviour
{
    public RectTransform tf;
    public RectTransform spin;
    public List<UIRewardSpinHome> uiRewardSpinHomes;
    public RewardSpinHomeSO rewardSpinHomeSO;

    public int n;
    public GameObject noClaim;
    public GameObject claimed;
    private void OnEnable()
    {
        spin.rotation = Quaternion.Euler(new Vector3(0, 0, 30f));
        SetupSpin(DataManager.Ins.dataSaved.isClaimSpinHome);

        for (int i = 0; i < uiRewardSpinHomes.Count; i++)
        {
            uiRewardSpinHomes[i].SetupReward(rewardSpinHomeSO.datas[i]);
        }
    }

    public void SetupSpin(bool b)
    {
        noClaim.SetActive(!b);
        claimed.SetActive(b);


    }

    public void ClaimSpin()
    {
        int angle = 360*n + Random.Range(0, 360);

        spin.DORotate(new Vector3(n, 0, angle), 5f, RotateMode.FastBeyond360).SetEase(Ease.OutQuad).OnComplete(() =>
        {
            ClaimReward(uiRewardSpinHomes[(angle % 360) / 60]);
        });


    }

    public void ClaimReward(UIRewardSpinHome reward)
    {
        switch(reward.reward.Type)
        {
            case RewardType.Coin:
                DataManager.Ins.dataSaved.coin += reward.reward.amount;
                break;
            case RewardType.Gems:
                DataManager.Ins.dataSaved.gems += reward.reward.amount;
                break;
            case RewardType.Add1Tile:
                DataManager.Ins.dataSaved.boosterAdd1 += reward.reward.amount;
                break;
            case RewardType.Undo:
                DataManager.Ins.dataSaved.boosterUndo += reward.reward.amount;
                break;
            case RewardType.Shuffle:
                DataManager.Ins.dataSaved.boosterSuffer += reward.reward.amount;
                break;
        }
    }

    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
