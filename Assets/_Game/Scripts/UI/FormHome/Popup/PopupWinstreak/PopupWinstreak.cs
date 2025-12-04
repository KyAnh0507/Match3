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
    public RectTransform icon;

    public Transform flyAdd1;
    public Transform flyDelete;
    public Transform flyShuffle;
    public Transform flyUndo;
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
                    icon = winstreakRewardUI2.icon;
                    winstreakRewardUIs.Add(winstreakRewardUI2);
                    continue;
                }
                WinstreakRewardUI winstreakRewardUI = Instantiate(winstreakRewardUIPrefab, content);
                winstreakRewardUIs.Add(winstreakRewardUI);
            }
        }

        int n = -1;
        for (int i = 0; i < winstreakRewardUIs.Count; i++)
        {
            if (rewardDataSO.rewardDatas[i].target <= DataManager.Ins.dataSaved.maxWinstreak)
            {
                n++;
            }
            else
            {
                continue;
            }
        }
        
        for (int i = 0; i < winstreakRewardUIs.Count; i++)
        {
            if (i == n - 1)
            {
                winstreakRewardUIs[i].SetupData(rewardDataSO.rewardDatas[i], true, false, i);
            }else if (i == n)
            {
                winstreakRewardUIs[i].SetupData(rewardDataSO.rewardDatas[i], false, true, i);
            }else
            {
                winstreakRewardUIs[i].SetupData(rewardDataSO.rewardDatas[i], false, false, i);
            }
            
        }
        if (DataManager.Ins.dataSaved.maxWinstreak > 1)
        {
            rtcontent.anchoredPosition = new Vector2(0f, 0f);
            rtcontent.DOAnchorPosY(Mathf.Max((n - 1), 0) * 330, 0.5f);
            icon.anchoredPosition = new Vector2(icon.anchoredPosition.x, 2970f);
            icon.DOAnchorPosY(2970 - Mathf.Max(n, 0) * 330f, 0.5f).SetEase(Ease.InQuad);
        }
        flyCoin.gameObject.SetActive(false);
        flyGem.gameObject.SetActive(false);

    }

    public void CollectCoinReward(Vector3 pos)
    {
        flyCoin.gameObject.SetActive(true);
        flyCoin.transform.position = pos;
        flyCoin.attractorTarget = UIManager.Ins.formHome.coinUI.IconTf;
        flyCoin.Play();
        UIManager.Ins.SetActiveBlock(true);
        UIManager.Ins.formHome.SetOverrideCoin(true);
        flyCoin.onLastParticleFinish.AddListener(() =>
        {
            UIManager.Ins.formHome.SetOverrideCoin(false);
            UIManager.Ins.SetActiveBlock(false);
            flyCoin.gameObject.SetActive(false);

        });
    }

    public void CollectGemReward(Vector3 pos)
    {
        flyGem.gameObject.SetActive(true);
        flyGem.transform.position = pos;
        flyGem.attractorTarget = UIManager.Ins.formHome.gemUI.IconTf;
        flyGem.Play();
        UIManager.Ins.SetActiveBlock(true);
        UIManager.Ins.formHome.SetOverrideGem(true);
        flyGem.onLastParticleFinish.AddListener(() =>
        {
            UIManager.Ins.formHome.SetOverrideGem(false);
            UIManager.Ins.SetActiveBlock(false);
            flyGem.gameObject.SetActive(false);
        });
    }

    public void CollectAdd1Reward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyAdd1.gameObject.SetActive(true);
        flyAdd1.transform.localScale = Vector3.one;
        flyAdd1.position = pos;
        DOVirtual.DelayedCall(1.2f, () =>
        {
            flyAdd1.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyAdd1.gameObject.SetActive(false);
            });
        });
    }

    public void CollectDeleteReward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyDelete.gameObject.SetActive(true);
        flyDelete.transform.localScale = Vector3.one;
        flyDelete.position = pos;
        DOVirtual.DelayedCall(1.2f, () =>
        {
            flyDelete.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyDelete.gameObject.SetActive(false);
            });
        });
    }

    public void CollectShuffleReward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyShuffle.gameObject.SetActive(true);
        flyShuffle.transform.localScale = Vector3.one;
        flyShuffle.position = pos;
        DOVirtual.DelayedCall(1.2f, () =>
        {
            flyShuffle.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyShuffle.gameObject.SetActive(false);
            });
        });
    }
    public void CollectUndoReward(Vector3 pos)
    {
        UIManager.Ins.SetActiveBlock(true);

        flyUndo.gameObject.SetActive(true);
        flyUndo.transform.localScale = Vector3.one;
        flyUndo.position = pos;
        DOVirtual.DelayedCall(1.2f, () =>
        {
            flyUndo.DOScale(Vector3.one * 0.3f, 0.3f).OnComplete(() =>
            {
                UIManager.Ins.SetActiveBlock(false);
                flyUndo.gameObject.SetActive(false);
            });
        });
    }

    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
