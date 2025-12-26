using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormHome : MonoBehaviour
{
    public Image background;

    public CoinUI coinUI;
    public GemUI gemUI;
    public PopupDailyReward popupDailyReward;
    public PopupDailyChallenge popupDailyChallenge;
    public PopupSpinHome popupSpinHome;
    public PopupShop popupShop;
    public PopupSetting popupSetting;
    public PopupWinstreak popupWinstreak;
    public PopupMasterPass popupMasterPass;
    public Transform buttonPlay;

    // Start is called before the first frame update
    void OnEnable()
    {
        LoadTextCoin();
        //background.sprite = GameConfig.Ins.themeGames[DataManager.Ins.dataSaved.theme].sprites[1];
        //int timeLastOpen = int.Parse(DateTime.Now.Date.ToString("yyyyMMdd"));
        TimeSpan t = DateTime.Now.Date - TimeConfig.startTime;
        int timeLastOpen = t.Days;
        if (timeLastOpen - DataManager.Ins.dataSaved.timeLastOpen == 1)
        {
            DataManager.Ins.dataSaved.isClaimDailyReward = false;
            popupDailyReward.Notify(true);
            popupDailyReward.streakDay = DataManager.Ins.dataSaved.streakDays;
            SetDataNewDay();
        }
        else if (timeLastOpen - DataManager.Ins.dataSaved.timeLastOpen > 1)
        {
            DataManager.Ins.dataSaved.isClaimDailyReward = false;
            popupDailyReward.Notify(true);
            DataManager.Ins.dataSaved.streakDays = 0;
            popupDailyReward.streakDay = 0;
            SetDataNewDay();
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

    public void SetDataNewDay()
    {
        DataManager.Ins.dataSaved.nSpinDaily = DataManager.Ins.dataSaved.maxSpinDaily;

        DataManager.Ins.dataSaved.nPlayGame = 0;
        DataManager.Ins.dataSaved.nWinGame = 0;
        DataManager.Ins.dataSaved.nUseBooster = 0;
        DataManager.Ins.dataSaved.nUseCoin = 0;
        DataManager.Ins.dataSaved.nUseGem = 0;
        DataManager.Ins.dataSaved.nWinChallenge = 0;

        for (int i = 0; i < 50; i++)
        {
            DataManager.Ins.dataSaved.taskMasterPassStatus[i] = false;
        }
    }

    public void LoadGame()
    {
        UIManager.Ins.ChangeScene(Scene.Game);
    }

    public void LoadTextCoin()
    {
        coinUI.ChangeValueImmediately(DataManager.Ins.dataSaved.coin);
        gemUI.ChangeValueImmediately(DataManager.Ins.dataSaved.gems);
    }

    public void SetOverrideCoin(bool b)
    {
        coinUI.canvas.overrideSorting = b;
    }

    public void SetOverrideGem(bool b)
    {
        gemUI.canvas.overrideSorting = b;
    }
    public void OpenPopupDailyReward()
    {
        popupDailyReward.gameObject.SetActive(true);
        popupDailyReward.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupDailyReward.tf.DOScale(Vector3.one, 0.5f);
        popupDailyReward.SetupReward();

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupDailyChallenge()
    {
        popupDailyChallenge.gameObject.SetActive(true);
        popupDailyChallenge.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupDailyChallenge.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupSpinHome()
    {
        popupSpinHome.gameObject.SetActive(true);
        popupSpinHome.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupSpinHome.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupShop()
    {
        popupShop.gameObject.SetActive(true);
        popupShop.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupShop.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupSetting()
    {
        popupSetting.gameObject.SetActive(true);
        popupSetting.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupSetting.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupWinstreak()
    {
        popupWinstreak.gameObject.SetActive(true);
        popupWinstreak.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupWinstreak.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }

    public void OpenPopupMasterPass()
    {
        popupMasterPass.gameObject.SetActive(true);
        popupMasterPass.tf.localScale = new Vector3(0.01f, 0.01f, 1f);
        popupMasterPass.tf.DOScale(Vector3.one, 0.5f);

        SoundManager.Ins.ChangeSound(SoundType.POPUP_CLICK);
    }
}
