using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupWinstreak : MonoBehaviour
{
    public RectTransform tf;
    public List<WinstreakRewardUI> winstreakRewardUIs = new List<WinstreakRewardUI>();

    public WinstreakRewardDataSO rewardDataSO;

    public ParticleImage flyCoin;
    public ParticleImage flyGem;
    public WinstreakRewardUI winstreakRewardUIPrefab;
    public WinstreakRewardUI winstreakRewardUIPrefab2;
    public Transform content;
    public RectTransform rtcontent;
    // Start is called before the first frame update
    void OnEnable()
    {
        if (winstreakRewardUIs.Count == 0)
        {
            for (int i = 0; i < rewardDataSO.rewardDatas.Count; i++)
            {
                if (i == rewardDataSO.rewardDatas.Count - 1)
                {
                    WinstreakRewardUI winstreakRewardUI2 = Instantiate(winstreakRewardUIPrefab2, content);
                    winstreakRewardUIs.Add(winstreakRewardUI2);
                    continue;
                }
                WinstreakRewardUI winstreakRewardUI = Instantiate(winstreakRewardUIPrefab, content);
                winstreakRewardUIs.Add(winstreakRewardUI);
            }
        }
        
        for (int i = 0; i < winstreakRewardUIs.Count; i++)
        {
            winstreakRewardUIs[i].SetupData(rewardDataSO.rewardDatas[i], i + 1);
        }
        if (DataManager.Ins.dataSaved.maxWinstreak > 1)
        {
            rtcontent.anchoredPosition = new Vector2(0f, 0f);
            rtcontent.DOAnchorPosY((DataManager.Ins.dataSaved.maxWinstreak - 2) * 330, DataManager.Ins.dataSaved.maxWinstreak * 0.38f);
        }
        flyCoin.gameObject.SetActive(false);
        flyGem.gameObject.SetActive(false);
    }


    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
