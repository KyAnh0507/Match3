using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DataManager : Singleton<DataManager>
{
    public bool isLoaded = false;

    [Header("Data References")]
    public Data dataSaved;
    private const string DATA_SAVED = "DataSaved";
    private Data dataBackup;

    public UnityAction<int> OnCoinChanged;
    public UnityAction<int> OnGemChanged;

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

    public void ChangeCoin(int amount)
    {
        dataSaved.coin += amount;

        if (amount < 0)
        {
            dataSaved.nUseCoin -= amount;
        }

        if (dataSaved.coin < 0)
        {
            dataSaved.coin = 0;
            Debug.LogError("Error: Diamond amount < 0");
        }

        OnCoinChanged?.Invoke(dataSaved.coin);
    }

    public void ChangeGem(int amount)
    {
        dataSaved.gems += amount;

        if (amount < 0)
        {
            dataSaved.nUseGem -= amount;
        }

        if (dataSaved.gems < 0)
        {
            dataSaved.gems = 0;
            Debug.LogError("Error: Diamond amount < 0");
        }

        OnGemChanged?.Invoke(dataSaved.gems);
    }

    [System.Serializable]
    public class Data
    {
        [Header("Basic Infor")]
        public bool isNew = true;
        public int timeInstall;
        public int timeLastOpen;
        public int daysPlayed; // So ngay user co choi game
        public int totalDays;  // So ngay user da cai game
        public int totalSession;


        public int level;
        public int indexLevel;

        public int coin;
        public int gems;

        [Header("Theme")]
        public int theme;
        public List<bool> statusTheme;

        [Header("DailyReward")]
        public int streakDays;
        public bool isClaimDailyReward;

        [Header("DailyChallenge")]
        public int currentMonth;
        public List<bool> statusDays;
        public List<bool> statusReward;
        public List<bool> statusUnlockReward;
        public bool completeChallenge;

        [Header("SpinHome")]
        public bool isClaimSpinHome;
        public int nSpinDaily;
        public int maxSpinDaily;

        [Header("Game Color Pencil")]
        public int indexLevelColorPencil;
        public int indexCurrentDay;


        [Header("Booster")]
        public int boosterSuffer;
        public int boosterBomb;
        public int boosterAdd1;
        public int boosterUndo;

        [Header("Winstreak")]
        public int maxWinstreak;
        public int currentWinstreak;
        public List<bool> statusWinstreak;

        [Header("Master Pass")]
        public int nPlayGame;
        public int nWinGame;
        public int nUseBooster;
        public int nUseCoin;
        public int nUseGem;
        public int nWinChallenge;
        public List<bool> taskMasterPassStatus;
        public int lvMasterPass;
        public int progress;
        public int maxLvMasterPass;
        public List<bool> rewardMasterPassStatus1;
        public List<bool> rewardMasterPassStatus2;
        public bool unlockedMasterPass;
        public int cycleIndexMasterPass;
        public int ticketUnlockMasterPass;

        [Header("Sound")]
        public bool isMusicOn;
        public bool isSoundOn;
        public bool isVibrate;

        [Header("Firebase")]
        public int cpStartLevel;
        public int cpEndLevel;
        public int timeRetry;
        public int attenpt;
        public int remainboosteradd1;
        public int remainboosterdelete;
        public int remainboostershuffle;
        public int remainboosterundo;

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
            coin = 2000;
            gems = 2000;

            //Theme
            theme = 0;
            statusTheme = new List<bool>();
            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                {
                    statusTheme.Add(true);
                }
                else
                {
                    statusTheme.Add(false);
                }
            }

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
            completeChallenge = false;
            indexLevelColorPencil = 0;

            // Spin
            isClaimSpinHome = false;
            nSpinDaily = 5;
            maxSpinDaily = 5;

            // Booster
            boosterSuffer = 0;
            boosterBomb = 0;
            boosterAdd1 = 0;
            boosterUndo = 0;

            isMusicOn = false;
            isSoundOn = false;
            isVibrate = false;

            //Winstreak
            maxWinstreak = 0;
            currentWinstreak = 0;
            statusWinstreak = new List<bool>();
            for(int i = 0; i < 50; i++)
            {
                statusWinstreak.Add(false);
            }

            //Master Pass
            nPlayGame = 0;
            nWinGame = 0;
            nUseBooster = 0;
            nUseCoin = 0;
            nUseGem = 0;
            nWinChallenge = 0;

            taskMasterPassStatus = new List<bool>();
            for (int i = 0; i < 50; i++)
            {
                taskMasterPassStatus.Add(false);
            }
            lvMasterPass = 1;
            progress = 0;
            maxLvMasterPass = 30;

            rewardMasterPassStatus1 = new List<bool>();
            for (int i = 0; i < 50; i++)
            {
                rewardMasterPassStatus1.Add(false);
            }
            rewardMasterPassStatus2 = new List<bool>();
            for (int i = 0; i < 50; i++)
            {
                rewardMasterPassStatus2.Add(false);
            }
            unlockedMasterPass = false;
            cycleIndexMasterPass = 0;
            ticketUnlockMasterPass = 0;
            // Firebase
            cpStartLevel = -1;
            cpEndLevel = -1;
            timeRetry = 0;
            attenpt = 0;
            remainboosteradd1 = 0;
            remainboosterdelete = 0;
            remainboostershuffle = 0;
            remainboosterundo = 0;

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
        }
    }

}