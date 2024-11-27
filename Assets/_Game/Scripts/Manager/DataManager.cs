using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public bool isLoaded = false;

    [Header("Data References")]
    public Data dataSaved;
    private const string DATA_SAVED = "DataSaved";
    private Data dataBackup;

    public void StartData()
    {
        LoadData(); // GameManager invoke this method
    }

    private void OnApplicationPause(bool pause) { SaveData(); }
    private void OnApplicationQuit()
    {
        SaveData();
    }

    public void LoadData()
    {
        try
        {
            isLoaded = true;

            if (PlayerPrefs.HasKey(DATA_SAVED)) dataSaved = JsonUtility.FromJson<Data>(PlayerPrefs.GetString(DATA_SAVED));

            if (dataSaved.isNew)
            {
                dataSaved = new Data();

                dataSaved.isNew = false;

            }
            else
            {
                dataSaved.totalSession++;
                /*int timeNow = (int)DateTime.Now.Subtract(GameConst.ORIGINAL_TIME).TotalDays;
                // New day
                if (timeNow - dataSaved.timeLastOpen > 0)
                {
                    dataSaved.daysPlayed++;
                    dataSaved.totalDays = timeNow - dataSaved.timeInstall;
                }
                dataSaved.timeLastOpen = timeNow;*/

            }

            // SaveData();
        }
        catch (Exception ex)
        {
            Debug.LogError("Load Data Error:" + ex);
        }
    }


    public void SaveData()
    {
        try
        {
            //if (!isLoaded) return;

            if (dataSaved == null)
            {
                if (dataBackup != null)
                {
                    dataSaved = dataBackup;
                }
                else
                {
                    dataSaved = new Data();
                    Debug.LogError("dataSaved null, backup fail. Reset data");
                }

            }


            dataBackup = dataSaved;

            PlayerPrefs.SetString(DATA_SAVED, JsonUtility.ToJson(dataSaved));
            PlayerPrefs.Save();
        }
        catch (Exception ex)
        {
            Debug.LogError("Save Data Error:" + ex);
        }
    }

    [System.Serializable]
    public class Data
    {
        [Header("Basic Infor")]
        public bool isNew = true;
        public int timeInstall;
        public int timeLastOpen;
        public int streakDays;
        public int daysPlayed; // So ngay user co choi game
        public int totalDays;  // So ngay user da cai game
        public int totalSession;
        public bool isClaimDailyReward;

        public int level;
        public int indexLevel;

        public int coin;
        public int gems;

        [Header("DailyChallenge")]
        public int currentMonth;

        public List<bool> statusDays;
        public List<bool> statusReward;
        public List<bool> statusUnlockReward;

        [Header("Game Color Pencil")]
        public int indexLevelColorPencil;
        public int indexCurrentDay;


        [Header("Booster")]
        public int boosterSuffer;
        public int boosterBomb;
        public int boosterAdd1;

        public bool isMusicOn;
        public bool isSoundOn;
        public bool isVibrate;

        [Header("Firebase")]
        public int cpStartLevel;
        public int cpEndLevel;
        public int timeRetry;

        [Header("Cheat")]
        public bool isCheatHard;

        [Header("Remove Ads")]
        public bool isNoAds;
        public bool isNoPack;

        [Header("Super Hard Level")]
        public bool isSuperHard;
        public int superHardLevel;
        public List<int> superHardCardStatus;
        public List<int> superHardBest;
        public bool isSuperHardNoti;

        [Header("Level Progress")]
        public int currentLevelProgress;
        public int currentLevelStage;

        [Header("JourneyProgress")]
        public int currentJourneyProgress;
        public int currentJourneyStage;

        [Header("Rate")]
        public bool isRate;

        [Header("AOA")]
        public bool isFirstAOA;

        public Data()
        {
            isNew = true;
            //timeInstall = (int)DateTime.Now.Subtract(GameConst.ORIGINAL_TIME).TotalDays;
            timeLastOpen = timeInstall;
            daysPlayed = 1;
            totalDays = 0;
            totalSession = 0;

            level = 0;
            indexLevel = 0;
            coin = 0;
            gems = 0;

            // DailyChalenge
            currentMonth = 0;
            statusDays = new List<bool>();
            for (int i = 0; i < 42; i++)
            {
                statusDays.Add(false);
            }

            statusReward = new List<bool>();
            for (int i = 0; i < 5; i++)
            {
                statusReward.Add(false);
            }

            statusUnlockReward = new List<bool>();
            for (int i = 0; i < 5; i++)
            {
                statusUnlockReward.Add(false);
            }

            indexLevelColorPencil = 0;

            // Booster
            boosterSuffer = 0;
            boosterBomb = 0;
            boosterAdd1 = 0;

            isMusicOn = true;
            isSoundOn = true;
            isVibrate = true;

            // Firebase
            cpStartLevel = -1;
            cpEndLevel = -1;
            timeRetry = 0;

            // Cheat
            isCheatHard = false;

            //remove ads
            isNoAds = false;
            isNoPack = false;

            // Super Hard
            isSuperHard = false;
            superHardLevel = 0;
            superHardCardStatus = new List<int>();
            superHardBest = new List<int>();
            isSuperHardNoti = false;

            // Level Progress
            currentLevelProgress = 0;
            currentLevelStage = 0;

            // Journey Progress
            currentJourneyProgress = 0;
            currentLevelStage = 0;

            // Rate
            isRate = false;

            // AOA
            isFirstAOA = false;
        }
    }

}