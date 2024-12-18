using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormHome : MonoBehaviour
{
    public Text textCoin;
    public Text textGems;
    public PopupDailyReward popupDailyReward;
    public PopupDailyChallenge popupDailyChallenge;
    public PopupSpinHome popupSpinHome;
    public PopupShop popupShop;

    // Start is called before the first frame update
    void OnEnable()
    {
        LoadTextCoin();

        //int timeLastOpen = int.Parse(DateTime.Now.Date.ToString("yyyyMMdd"));
        TimeSpan t = DateTime.Now.Date - TimeConfig.startTime;
        int timeLastOpen = t.Days;
        if (timeLastOpen - DataManager.Ins.dataSaved.timeLastOpen == 1)
        {
            DataManager.Ins.dataSaved.isClaimDailyReward = false;
            popupDailyReward.Notify(true);
            popupDailyReward.streakDay = DataManager.Ins.dataSaved.streakDays;
        }
        else if (timeLastOpen - DataManager.Ins.dataSaved.timeLastOpen > 1)
        {
            DataManager.Ins.dataSaved.isClaimDailyReward = false;
            popupDailyReward.Notify(true);
            DataManager.Ins.dataSaved.streakDays = 0;
            popupDailyReward.streakDay = 0;
        }else if (DataManager.Ins.dataSaved.isClaimDailyReward)
        {
            popupDailyReward.Notify(false);
            popupDailyReward.streakDay = DataManager.Ins.dataSaved.streakDays;
        }
        DataManager.Ins.dataSaved.timeLastOpen = timeLastOpen;
        if (DataManager.Ins.dataSaved.completeChallenge)
        {
            OpenPopupDailyChallenge();
        }
    }

    public void LoadGame()
    {
        UIManager.Ins.ChangeScene(Scene.Game);
    }

    public void LoadTextCoin()
    {
        textCoin.text = DataManager.Ins.dataSaved.coin.ToString();
        textGems.text = DataManager.Ins.dataSaved.gems.ToString();
    }
    public void OpenPopupDailyReward()
    {
        popupDailyReward.gameObject.SetActive(true);
        popupDailyReward.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupDailyReward.tf.DOScale(Vector3.one, 0.5f);
        popupDailyReward.SetupReward();
    }

    public void OpenPopupDailyChallenge()
    {
        popupDailyChallenge.gameObject.SetActive(true);
        popupDailyChallenge.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupDailyChallenge.tf.DOScale(Vector3.one, 0.5f);
    }

    public void OpenPopupSpinHome()
    {
        popupSpinHome.gameObject.SetActive(true);
        popupSpinHome.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupSpinHome.tf.DOScale(Vector3.one, 0.5f);
    }

    public void OpenPopupShop()
    {
        popupShop.gameObject.SetActive(true);
        popupShop.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupShop.tf.DOScale(Vector3.one, 0.5f);
    }
}
