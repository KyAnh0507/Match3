using AssetKits.ParticleImage;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupDailyChallenge : MonoBehaviour
{
    public RectTransform tf;
    public List<DayInDaylyChallenge> dayInDaylyChallenges = new List<DayInDaylyChallenge>();

    public ParticleImage flyCoin;
    public GameObject buttonClaim;
    public GameObject buttonClaimed;


    void OnEnable()
    {
        SetUpCalendar();
    }

    public void SetUpCalendar()
    {

    }

    public void SetUpDayInDaylyChallenge()
    {
        
    }

    public void SetupReward()
    {
        /*if (DataManager.Ins.dataSaved.isClaimDailyReward)
        {
            buttonClaim.SetActive(false);
            buttonClaimed.SetActive(true);
        }
        else
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

        }*/

    }

    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
