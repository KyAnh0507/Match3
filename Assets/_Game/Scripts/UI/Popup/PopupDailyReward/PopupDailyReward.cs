using AssetKits.ParticleImage;
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
    }

    public void Claim()
    {
        flyCoin.gameObject.SetActive(true);
        flyCoin.rectTransform.position = dailyRewardUIs[streakDay].transform.position;
        flyCoin.Play();
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
        this.gameObject.SetActive(false);
    }
}


