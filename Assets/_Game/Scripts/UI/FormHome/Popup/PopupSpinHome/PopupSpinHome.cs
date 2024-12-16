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

        spin.DORotate(new Vector3(n, 0, angle), 5f).SetEase(Ease.OutQuad);


    }
}
