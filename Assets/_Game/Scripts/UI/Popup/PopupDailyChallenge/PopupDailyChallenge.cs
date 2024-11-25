using AssetKits.ParticleImage;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupDailyChallenge : MonoBehaviour
{
    public RectTransform tf;
    public RectTransform bg1;
    public RectTransform bg2;
    public List<DayInDaylyChallenge> dayInDaylyChallenges = new List<DayInDaylyChallenge>();

    public Text textMonth;
    public DayInDaylyChallenge currentDay;

    public ParticleImage flyCoin;
    public ParticleImage flyGems;

    public GameObject buttonPlay;
    public GameObject buttonFinished;


    void OnEnable()
    {
        SetUpCalendar();
    }

    public void SetUpCalendar()
    {
        textMonth.text = DateTime.Now.ToString("MMMM, yyyy");
        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        int daysInLastMonth = 0;
        if (DateTime.Now.Month > 1)
        {
            daysInLastMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month - 1);
        }
        else
        {
            daysInLastMonth = 31;
        }
        DateTime firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        DayOfWeek dayOfWeek = firstDayOfMonth.DayOfWeek;
        int nowDay = DateTime.Now.Day;
        bool has5Week = false;
        for (int i = 0; i < dayInDaylyChallenges.Count; i++)
        {
            if(i < 7 && i < (int)dayOfWeek)
            {
                dayInDaylyChallenges[i].gameObject.SetActive(false);
                /*dayInDaylyChallenges[i].SetupDay(daysInLastMonth - ((int)dayOfWeek - i) + 1);
                dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;*/
            }
            else if (i >= (int)dayOfWeek && i < (int)dayOfWeek + daysInMonth)
            {
                dayInDaylyChallenges[i].SetupDay(i - (int)dayOfWeek + 1);
                if (i - (int)dayOfWeek + 1 > nowDay)
                {
                    dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;
                }
                
            }
            else 
            {
                if (i < 35 || daysInMonth + (int)dayOfWeek < 35)
                {
                    dayInDaylyChallenges[i].gameObject.SetActive(false);
                    /*dayInDaylyChallenges[i].SetupDay(i - (int)dayOfWeek - daysInMonth + 1);
                    dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;*/
                }
                else
                {
                    has5Week = true;
                    dayInDaylyChallenges[i].gameObject.SetActive(false);
                }
            }
        }
        if (has5Week)
        {
            bg1.anchoredPosition = new Vector2(bg1.anchoredPosition.x, -315f);
            bg1.sizeDelta = new Vector2(bg1.sizeDelta.x, 1430f);
            bg2.offsetMin = new Vector2 (bg2.offsetMin.x, -150f);
            RectTransform rt1 = buttonPlay.GetComponent<RectTransform>();
            rt1.anchoredPosition = new Vector2(rt1.anchoredPosition.x, -580f);
            RectTransform rt2 = buttonFinished.GetComponent<RectTransform>();
            rt2.anchoredPosition = new Vector2(rt2.anchoredPosition.x, -580f);
        }


        int nMonth = DateTime.Now.Year*12 + DateTime.Now.Month;

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

    public void PlayChallenge()
    {

    }

    public void SelectDay(DayInDaylyChallenge dayInDaylyChallenge)
    {
        currentDay = dayInDaylyChallenge;

    }
    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
