using System.Collections;
using Firebase.Analytics;
using Firebase.RemoteConfig;
using UnityEngine;
using System;
using System.Globalization;
//using Spine;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
//using UnityEditor.SceneManagement;
using System.Security.Claims;
/// <summary>
/// Document Link: https://docs.google.com/spreadsheets/d/1PUjPCuHoE5pRhD8Up4vCrQWRktgS_MFFADkgZppNdEw/edit#gid=0
/// </summary>
public class FirebaseManager : MonoBehaviour
{
    public static FirebaseManager Ins = null;

    //Check khởi tạo firebase
    private bool fireBaseReady = false;//Firebase đã Init thành công
    private bool firebaseIniting = true;//Firebase đang Init

    public bool is_remote_config_done = false;//Quá trình RemoteConfig đã xong
    public bool is_remote_config_success = false;//RemoteConfig thành công

    //#region FIREBASE SETUP
    void Awake()
    {
        if (Ins != null)
        {
            Destroy(this.gameObject);
            return;
        }
        Ins = this;
        firebaseIniting = true;
    }

    private IEnumerator Start()
    {
        CheckFireBase();
        yield return new WaitUntil(() => !firebaseIniting);
        if (fireBaseReady)
        {
            Firebase.FirebaseApp.LogLevel = Firebase.LogLevel.Debug;

            //Khởi tạo remote config
            fetch((bool is_fetch_result) => { });
        }
        else
        {
            Debug.LogError("Ko khởi tạo đc Firebase");
        }
    }

    private void CheckFireBase()
    {
        try
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                firebaseIniting = false;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    fireBaseReady = true;
                }
                else
                {
                    Debug.LogError(System.String.Format("Lỗi dependencies của Firebase: {0}", dependencyStatus));
                }
            });
        }
        catch (System.Exception ex)
        {
            firebaseIniting = false;
            Debug.LogError("Lỗi khởi tạo Firebase:" + ex.ToString());
        }
    }

    #region USER_PROPERTIES

    public void OnSetUserProperty()
    {
        StartCoroutine(ie_OnSetUserProperty());
    }
    //Hàm này gọi 2 lần:
    //1. Khi mở game
    //2. Khi win 1 level (đối với game hyper) hoặc win 1 level ở main game content (với các game mid/puzzle)
    IEnumerator ie_OnSetUserProperty()
    {
        yield return new WaitUntil(() => fireBaseReady);
        try
        {
            //Nếu là bản DevelopmentBuild hoặc UnityEditor thì ko bắn UserProperty lên
            if (!Debug.isDebugBuild && !Application.isEditor)
            {
                //===========================================================
                //retentType
                //[timeInstall]: Thời gian cài app lần đầu
                //DateTime timeInstall = DateTime.ParseExact(DataManager.instance.playerData.timeInstall.ToString(), "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                var timeInstall = DataManager.Ins.dataSaved.timeInstall;

                //===========================================================
                //[timeLastOpen] : Thời gian mở app lần cuối (hiện tại)
                //DateTime time = DateTime.ParseExact(DataManager.instance.timeLastOpen, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                var time = DataManager.Ins.dataSaved.timeLastOpen;

                //===========================================================
                //Retention của user, 0 tương ứng D0, 7 tương ứng D7
                //OnSetProperty("retent_type", (time - timeInstall).Days);
                OnSetProperty("retent_type", (time - timeInstall) / 86400);

                //===========================================================
                //Số ngày user đã chơi, khác với Retention.Nếu user cài ở D0 và 7 ngày sau mới chơi thì retention là D7 còn days_played là 2
                //[daysPlayed] : số ngày bật (giá trị này cần tính toán mỗi khi bật app)
                OnSetProperty("days_played", DataManager.Ins.dataSaved.daysPlayed);

                //===========================================================
                //Số tiền user đã pay, với game casual thì chỉ tính các mốc 2, 5, 10, 20, 50,
                //tức là nếu đã tiêu 3$ thì paying_type = 2, tiêu 6$ thì paying_type = 5, lấy mốc cận dưới gần nhất.
                //Chỉ game có IAP
                //OnSetProperty("paying_type", 0);

                //===========================================================
                //[maxLevel]: update sau cùng các event liên quan tới level, giá trị là level lớn nhất user pass cộng thêm 1,
                //(giá trị ban đầu khi cài game lần đầu và chưa pass level nào là 1)
                // OnSetProperty("level", GameManager.ins.data.level + 1);
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Firebase: (Userproperties) Error: " + e);
        }
    }

    private void OnSetProperty(string key, object value)
    {
        try
        {
            FirebaseAnalytics.SetUserProperty(key.ToString(), value.ToString());
        }
        catch (Exception e)
        {
            Debug.LogError("Lỗi UserProperty của Firebase: " + key + " _ " + e);
        }
    }
    #endregion

    #region Events
    public void level_start(string level, int attempt)
    {
        if (!Debug.isDebugBuild && !Application.isEditor)
        {
            FirebaseAnalytics.LogEvent("level_start", new Parameter[]
            {
                new Parameter("level",             level),
                new Parameter("attempt",        attempt)
            });
        }
    }

    public void level_end(string level, string result, int remaining_booster_add1, int remaining_booster_shuffle, int remaining_booster_delete, int remaining_booster_undo, int winStreak)
    {
        if (!Debug.isDebugBuild && !Application.isEditor)
        {
            FirebaseAnalytics.LogEvent("level_complete", new Parameter[]
            {
                new Parameter("level",                   level),
                new Parameter("result",                   result),
                new Parameter("remaining_booster_undo",    remaining_booster_add1),
                new Parameter("remaining_booster_shuffle",    remaining_booster_shuffle),
                new Parameter("remaining_booster_doublebox",    remaining_booster_delete),
                new Parameter("remaining_booster_magnet",    remaining_booster_undo),
                new Parameter("win_streak",              winStreak)
            });
        }
    }

    public void resource_earn(string level, string resource_name, int value)
    {
        if (!Debug.isDebugBuild && !Application.isEditor)
        {
            FirebaseAnalytics.LogEvent("resource_earn", new Parameter[]
            {
                new Parameter("level", level),
                new Parameter("resource_name", resource_name),
                new Parameter("value", value),
               });
        }
    }

    public void resource_spend(string level, string resource_name, int value)
    {
        if (!Debug.isDebugBuild && !Application.isEditor)
        {
            FirebaseAnalytics.LogEvent("resource_spend", new Parameter[]
            {
                new Parameter("level", level),
                new Parameter("resource_name", resource_name),
                new Parameter("value", value),
               });
        }
    }

    public void user_properties(string user_retention, string value_coins_earn_count, string value_coins_spend_count, string last_time_play)
    {
        if (!Debug.isDebugBuild && !Application.isEditor)
        {
            FirebaseAnalytics.LogEvent("user_properties", new Parameter[]
            {
                new Parameter("user_retention", user_retention),
                new Parameter("value_coins_earn_count", value_coins_earn_count),
                new Parameter("value_coins_spend_count", value_coins_spend_count),
                new Parameter("last_time_play", last_time_play),
               });
        }
    }

    #endregion

    #region Remote Config
    /// <summary>
    /// Setup Remote config
    /// </summary>
    /// <param name="completionHandler"></param>
    public void fetch(Action<bool> completionHandler)
    {
        try
        {
            var settings = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ConfigSettings;
            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetConfigSettingsAsync(settings);

            var fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(new TimeSpan(0));

            fetchTask.ContinueWith(task =>
            {
                is_remote_config_done = true;
                if (task.IsCanceled || task.IsFaulted)
                {
                    Debug.LogWarning("fetchTask Firebase Fail");
                    is_remote_config_success = false;
                    completionHandler(false);
                }
                else
                {
                    Debug.LogWarning("fetchTask Firebase Commplete");
                    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
                    RefrectProperties();

                    completionHandler(true);
                }
            });
        }
        catch (Exception ex)
        {
            is_remote_config_done = true;
            Debug.Log(ex.ToString());
        }
    }

    /// <summary>
    /// Dữ liệu remote config
    /// </summary>
    private void RefrectProperties()
    {
        try
        {
            /*DataManager.Instance.gameData.interBackHome = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("Interstitial_BackHome").LongValue;
            DataManager.Instance.gameData.interCapping = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstitialAds_Capping").LongValue;
            DataManager.Instance.gameData.interLose = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstitialAds_Failed").LongValue;
            DataManager.Instance.gameData.interWin = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstitialAds_Win").LongValue;
            DataManager.Instance.gameData.interCappingReward = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("Interstitial_CappingReward").LongValue;*/
            is_remote_config_success = true;
        }
        catch (Exception ex)
        {
            Debug.Log("Error RefrectProperties: " + ex.Message);
        }
    }


    #endregion
}
