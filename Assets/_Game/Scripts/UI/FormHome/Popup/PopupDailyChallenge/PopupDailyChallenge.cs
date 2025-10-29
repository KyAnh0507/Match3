using AssetKits.ParticleImage;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;
using UnityEngine.UI;

public class PopupDailyChallenge : MonoBehaviour
{
    public RectTransform tf;
    public RectTransform bg1;
    public RectTransform bg2;
    public List<DayInDaylyChallenge> dayInDaylyChallenges = new List<DayInDaylyChallenge>();
    public List<RewardChallengeButton> rewardChallengeButtons = new List<RewardChallengeButton>();

    public Text textMonth;
    public DayInDaylyChallenge currentDay;

    public ParticleImage flyCoin;
    public ParticleImage flyGems;

    public Sprite imageSelect;
    public Sprite imageDay;
    public Sprite imageFinished;

    public GameObject fillProgress;
    public GameObject buttonPlay;
    public GameObject buttonFinished;


    public RewardChallengeSO rewardChallengeSO;

    void OnEnable()
    {
        if (DataManager.Ins.dataSaved.completeChallenge)
        {
            DataManager.Ins.dataSaved.completeChallenge = false;
            SetUpCalendarCompleteChallenge();
        }
        else
        {
            SetUpCalendar();
        }
        SetupReward();
    }

    public void SetUpCalendar()
    {
        textMonth.text = DateTime.Now.ToString("MMMM, yyyy");
        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        if (daysInMonth > DataManager.Ins.dataSaved.currentMonth)
        {
            DataManager.Ins.dataSaved.currentMonth = daysInMonth;
            for (int i = 0; i < 42; i++)
            {
                DataManager.Ins.dataSaved.statusDays[i] = false;
            }
        }

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
            dayInDaylyChallenges[i].order = i;
            if(i < 7 && i < (int)dayOfWeek)
            {
                dayInDaylyChallenges[i].gameObject.SetActive(false);
                /*dayInDaylyChallenges[i].SetupDay(daysInLastMonth - ((int)dayOfWeek - i) + 1);
                dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;*/
            }
            else if (i >= (int)dayOfWeek && i < (int)dayOfWeek + daysInMonth)
            {
                dayInDaylyChallenges[i].SetupDay(i - (int)dayOfWeek + 1);
                if (DataManager.Ins.dataSaved.statusDays[i])
                {
                    dayInDaylyChallenges[i].image.sprite = imageFinished;
                }
                if (i - (int)dayOfWeek + 1 == nowDay)
                {
                    currentDay = dayInDaylyChallenges[i];
                    if (!DataManager.Ins.dataSaved.statusDays[i])
                    {
                        dayInDaylyChallenges[i].Notify(true);
                    }
                    currentDay = dayInDaylyChallenges[i];
                    currentDay.image.sprite = imageSelect;
                    CheckFinishChallenge(currentDay);
                }
                if (i - (int)dayOfWeek + 1 > nowDay)
                {
                    dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;
                }
                
            }
            else 
            {
                if (i < 35 || daysInMonth + (int)dayOfWeek >= 36)
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

        CheckFinishChallenge(currentDay);
        int nMonth = DateTime.Now.Year*12 + DateTime.Now.Month;

    }

    public void SetUpCalendarCompleteChallenge()
    {
        textMonth.text = DateTime.Now.ToString("MMMM, yyyy");
        int daysInMonth = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
        if (daysInMonth > DataManager.Ins.dataSaved.currentMonth)
        {
            DataManager.Ins.dataSaved.currentMonth = daysInMonth;
            for (int i = 0; i < 42; i++)
            {
                DataManager.Ins.dataSaved.statusDays[i] = false;
            }
        }

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
            dayInDaylyChallenges[i].order = i;
            if (i < 7 && i < (int)dayOfWeek)
            {
                dayInDaylyChallenges[i].gameObject.SetActive(false);
                /*dayInDaylyChallenges[i].SetupDay(daysInLastMonth - ((int)dayOfWeek - i) + 1);
                dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;*/
            }
            else if (i >= (int)dayOfWeek && i < (int)dayOfWeek + daysInMonth)
            {
                dayInDaylyChallenges[i].SetupDay(i - (int)dayOfWeek + 1);
                if (DataManager.Ins.dataSaved.statusDays[i])
                {
                    dayInDaylyChallenges[i].image.sprite = imageFinished;
                }
                if (i == DataManager.Ins.dataSaved.indexCurrentDay)
                {
                    currentDay = dayInDaylyChallenges[i];
                    currentDay.image.sprite = imageSelect;
                    CheckFinishChallenge(currentDay);
                }
                if (i - (int)dayOfWeek + 1 == nowDay)
                {
                    if (!DataManager.Ins.dataSaved.statusDays[i])
                    {
                        dayInDaylyChallenges[i].Notify(true);
                    }
                }
                if (i - (int)dayOfWeek + 1 > nowDay)
                {
                    dayInDaylyChallenges[i].GetComponent<Button>().interactable = false;
                }

            }
            else
            {
                if (i < 35 || daysInMonth + (int)dayOfWeek >= 36)
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
            bg2.offsetMin = new Vector2(bg2.offsetMin.x, -150f);
            RectTransform rt1 = buttonPlay.GetComponent<RectTransform>();
            rt1.anchoredPosition = new Vector2(rt1.anchoredPosition.x, -580f);
            RectTransform rt2 = buttonFinished.GetComponent<RectTransform>();
            rt2.anchoredPosition = new Vector2(rt2.anchoredPosition.x, -580f);
        }

        CheckFinishChallenge(currentDay);
        int nMonth = DateTime.Now.Year * 12 + DateTime.Now.Month;

    }


    public void SetupReward()
    {
        int count = 0;
        for (int i = 0; i < rewardChallengeButtons.Count; i++)
        {
            rewardChallengeButtons[i].target.text = rewardChallengeSO.rewardDatas[i].numberTarget.ToString();
            rewardChallengeButtons[i].reward = rewardChallengeSO.rewardDatas[i];
            rewardChallengeButtons[i].order = i;
        }
        for (int i = 0; i < DataManager.Ins.dataSaved.statusDays.Count; i++)
        {
            if (DataManager.Ins.dataSaved.statusDays[i])
            {
                count++;
            }
        }
        fillProgress.transform.localScale = new Vector3(0, 1f, 1f);
        if (count < int.Parse(rewardChallengeButtons[0].target.text))
        {
            fillProgress.transform.DOScale(new Vector3(count*0.2f, 1f, 1f), 0.5f);
        }else if (count < int.Parse(rewardChallengeButtons[1].target.text))
        {
            fillProgress.transform.DOScale(new Vector3((float)(count - int.Parse(rewardChallengeButtons[0].target.text)) / (int.Parse(rewardChallengeButtons[1].target.text) - int.Parse(rewardChallengeButtons[0].target.text)) * 0.2f + 0.2f, 1f, 1f), 0.5f);
            DataManager.Ins.dataSaved.statusUnlockReward[0] = true;
            rewardChallengeButtons[0].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[0]);
        }
        else if (count < int.Parse(rewardChallengeButtons[2].target.text))
        {
            fillProgress.transform.DOScale(new Vector3((float)(count - int.Parse(rewardChallengeButtons[1].target.text)) / (int.Parse(rewardChallengeButtons[2].target.text) - int.Parse(rewardChallengeButtons[1].target.text)) * 0.2f + 0.4f, 1f, 1f), 0.5f);
            DataManager.Ins.dataSaved.statusUnlockReward[0] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[1] = true;

            rewardChallengeButtons[0].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[0]);
            rewardChallengeButtons[1].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[1]);
        }
        else if (count < int.Parse(rewardChallengeButtons[3].target.text))
        {
            fillProgress.transform.DOScale(new Vector3((float)(count - int.Parse(rewardChallengeButtons[2].target.text)) / (int.Parse(rewardChallengeButtons[3].target.text) - int.Parse(rewardChallengeButtons[2].target.text)) * 0.2f + 0.6f, 1f, 1f), 0.5f);
            DataManager.Ins.dataSaved.statusUnlockReward[0] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[1] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[2] = true;
            rewardChallengeButtons[0].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[0]);
            rewardChallengeButtons[1].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[1]);
            rewardChallengeButtons[2].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[2]);
        }
        else if (count < int.Parse(rewardChallengeButtons[4].target.text))
        {
            fillProgress.transform.DOScale(new Vector3((float)(count - int.Parse(rewardChallengeButtons[3].target.text)) / (int.Parse(rewardChallengeButtons[4].target.text) - int.Parse(rewardChallengeButtons[3].target.text)) * 0.2f + 0.8f, 1f, 1f), 0.5f);
            DataManager.Ins.dataSaved.statusUnlockReward[0] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[1] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[2] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[3] = true;
            rewardChallengeButtons[0].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[0]);
            rewardChallengeButtons[1].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[1]);
            rewardChallengeButtons[2].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[2]);
            rewardChallengeButtons[3].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[3]);
        }
        else
        {
            fillProgress.transform.DOScale(Vector3.one, 0.5f);
            DataManager.Ins.dataSaved.statusUnlockReward[0] = true; 
            DataManager.Ins.dataSaved.statusUnlockReward[1] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[2] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[3] = true;
            DataManager.Ins.dataSaved.statusUnlockReward[4] = true;
            rewardChallengeButtons[0].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[0]);
            rewardChallengeButtons[1].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[1]);
            rewardChallengeButtons[2].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[2]);
            rewardChallengeButtons[3].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[3]);
            rewardChallengeButtons[4].ActiveHighlight(!DataManager.Ins.dataSaved.statusReward[4]);
        }
    }

    public void ReceiveReward(RewardChallengeButton rewardChallenge)
    {
        if (rewardChallenge.highlight.activeSelf)
        {
            DataManager.Ins.dataSaved.statusReward[rewardChallenge.order] = true;
            rewardChallenge.ActiveHighlight(false);
            switch (rewardChallenge.reward.rewardType)
            {
                case RewardType.Coin:
                    DataManager.Ins.ChangeCoin(rewardChallenge.reward.amount1);
                    flyCoin.gameObject.SetActive(true);
                    flyCoin.rectTransform.position = rewardChallenge.transform.position;
                    flyCoin.Play();
                    break;
                case RewardType.Gems:
                    DataManager.Ins.ChangeGem(rewardChallenge.reward.amount1);
                    flyGems.gameObject.SetActive(true);
                    flyGems.rectTransform.position = rewardChallenge.transform.position;
                    flyGems.Play();
                    flyGems.LayoutComplete();
                    break;
                case RewardType.CoinAndGems:
                    DataManager.Ins.ChangeCoin(rewardChallenge.reward.amount1);
                    DataManager.Ins.ChangeGem(rewardChallenge.reward.amount1);
                    flyCoin.gameObject.SetActive(true);
                    flyCoin.rectTransform.position = rewardChallenge.transform.position;
                    flyCoin.Play();
                    flyGems.gameObject.SetActive(true);
                    flyGems.rectTransform.position = rewardChallenge.transform.position;
                    flyGems.Play();
                    break;
            }
            UIManager.Ins.LoadTextCoin();
        }
    }

    public void CheckFinishChallenge(DayInDaylyChallenge dayInDaylyChallenge)
    {
        if (DataManager.Ins.dataSaved.statusDays[dayInDaylyChallenge.order])
        {
            buttonPlay.SetActive(false);
            buttonFinished.SetActive(true);
        } 
        else
        {
            buttonPlay.SetActive(true);
            buttonFinished.SetActive(false);
        }
    }
    public void PlayChallenge()
    {
        DataManager.Ins.dataSaved.indexLevelColorPencil = currentDay.indexLevelColorPencil;
        DataManager.Ins.dataSaved.indexCurrentDay = currentDay.order;
        Close();
        DOVirtual.DelayedCall(0.5f, () =>
        {

        }).OnComplete(() =>
        {
            UIManager.Ins.ChangeScene(Scene.GameColorPencil);

        });
    }

    public void SelectDay(DayInDaylyChallenge dayInDaylyChallenge)
    {
        if (DataManager.Ins.dataSaved.statusDays[currentDay.order])
        {
            currentDay.image.sprite = imageFinished;
        }
        else
        {
            currentDay.image.sprite = imageDay;
        }
        currentDay = dayInDaylyChallenge;
        currentDay.image.sprite = imageSelect;
        CheckFinishChallenge(currentDay);
    }
    public void Close()
    {
        tf.DOScale(new Vector3(0.01f, 0.01f, 1f), 0.5f).OnComplete(() =>
        {
            this.gameObject.SetActive(false);
        });
    }
}
