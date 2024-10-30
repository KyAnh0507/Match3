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
    // Start is called before the first frame update
    void Awake()
    {
        textCoin.text = DataManager.Ins.dataSaved.coin.ToString();
        textGems.text = DataManager.Ins.dataSaved.gems.ToString();

        int timeLastOpen = int.Parse(DateTime.Now.Date.ToString("yyyyMMdd"));
        if (timeLastOpen - DataManager.Ins.dataSaved.timeLastOpen == 1)
        {
            DataManager.Ins.dataSaved.isClaimDailyReward = false;
            popupDailyReward.Notify(true);
            DataManager.Ins.dataSaved.streakDays++;
            popupDailyReward.streakDay = DataManager.Ins.dataSaved.streakDays;
        }
        else if (timeLastOpen - DataManager.Ins.dataSaved.timeLastOpen > 1)
        {
            DataManager.Ins.dataSaved.isClaimDailyReward = false;
            popupDailyReward.Notify(true);
            DataManager.Ins.dataSaved.streakDays = 0;
            popupDailyReward.streakDay = 0;
        }
        DataManager.Ins.dataSaved.timeLastOpen = timeLastOpen;
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
        popupDailyReward.SetupReward();
    }
}
