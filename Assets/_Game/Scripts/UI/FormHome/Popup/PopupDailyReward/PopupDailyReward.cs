using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDailyReward : MonoBehaviour
{
    public RectTransform tf;
    public List<DailyRewardUI> dailyRewardUIs;
    public GameObject notify;
    public int streakDay;
    public RewardDataSO rewardDataSO;
    public List<Sprite> spriteLists = new List<Sprite>();

    public ParticleImage flyCoin;
    public ParticleImage flyGem;
    public GameObject buttonClaim;
    public GameObject buttonClaimed;
    // Start is called before the first frame update
    void OnEnable()
    {
        for (int i = 0; i < dailyRewardUIs.Count; i++)
        {
            dailyRewardUIs[i].SetupData(rewardDataSO.rewardDatas[i]);
        }
        flyCoin.gameObject.SetActive(false);
        flyGem.gameObject.SetActive(false);
    }

    public void Claim()
    {
        if (dailyRewardUIs[streakDay].reward.rewardType == RewardType.Coin)
        {
            flyCoin.gameObject.SetActive(true);
            flyCoin.rectTransform.position = dailyRewardUIs[streakDay].transform.position;
            flyCoin.Play();
        }

        if (dailyRewardUIs[streakDay].reward.rewardType == RewardType.Gems)
        {
            flyGem.gameObject.SetActive(true);
            flyGem.rectTransform.position = dailyRewardUIs[streakDay].transform.position;
            flyGem.Play();
        }
        dailyRewardUIs[streakDay].Claim();
        DataManager.Ins.dataSaved.streakDays++;
        streakDay++;

        buttonClaim.SetActive(false);
        buttonClaimed.SetActive(true);

        UIManager.Ins.formHome.LoadTextCoin();
    }

    public void Notify(bool b)
    {
        notify.SetActive(b);
    }
    public void SetupReward()
    {
        if (DataManager.Ins.dataSaved.isClaimDailyReward)
        {
            buttonClaim.SetActive(false);
            buttonClaimed.SetActive(true);
        }else
        {
            buttonClaim.SetActive(true);
            buttonClaimed.SetActive(false);
        }
        if (streakDay < 6)
        {
            for (int i = 0; i < streakDay; i++)
            {
                dailyRewardUIs[i].Claimed();
            }

            for (int i = streakDay; i < dailyRewardUIs.Count; i++)
            {
                dailyRewardUIs[i].NoClaim();
            }
        }
        else
        {
            for (int i = 0; i < 6; i++)
            {
                dailyRewardUIs[i].Claimed();
            }

            dailyRewardUIs[6].NoClaim();

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


