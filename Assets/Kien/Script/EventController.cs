using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Analytics;
using AppsFlyerSDK;
using Firebase;
using Firebase.Extensions;
using System.Threading.Tasks;
using System;
using UnityEngine.SceneManagement;

[System.Serializable]
public class RemoteDefaultInfo
{
    public string key, value;
}
public class EventController : MonoBehaviour
{
    private static EventController Instance;
    Firebase.FirebaseApp app;
    static bool fireBaseInitDone;
    public RemoteDefaultInfo[] remoteDefault;
    private int sceneIndex;
    private string sceneName;
   static bool first_open;
    private void Start()
    {
        if (Instance != null)
            return;

        Instance = this;
        DontDestroyOnLoad(this);

        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == Firebase.DependencyStatus.Available)
            {
                app = FirebaseApp.DefaultInstance;
                InitRemoteConfig();
                fireBaseInitDone = true;
                //      StartCoroutine(GetIDToken());
            }
            else
            {
                UnityEngine.Debug.LogError(System.String.Format(
                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
            }
        });

        //var activeScene = SceneManager.GetActiveScene();
        //sceneIndex = activeScene.buildIndex;
        //sceneName = activeScene.name;

        //FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
        //    new Parameter(FirebaseAnalytics.ParameterLevel, sceneIndex),
        //    new Parameter(FirebaseAnalytics.ParameterLevelName, sceneName));

        //first_open = Datacontroller.instance.saveData.session == 1 ? true : false;
    }
    //private IEnumerator GetIDToken()
    //{
    //    System.Threading.Tasks.Task<string> t = Firebase.InstanceId.FirebaseInstanceId.DefaultInstance.GetTokenAsync();
    //    while (!t.IsCompleted) yield return new WaitForEndOfFrame();
    //    Debug.LogError("============ FirebaseID is " + t.Result);

    //}
    //private void OnDisable()
    //{
    //    FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart,
    //                               new Parameter(FirebaseAnalytics.ParameterLevel, sceneIndex),
    //                               new Parameter(FirebaseAnalytics.ParameterLevelName, sceneName));
    //}
    void InitRemoteConfig()
    {
        System.Collections.Generic.Dictionary<string, object> defaults =
  new System.Collections.Generic.Dictionary<string, object>();

        for (int i = 0; i < remoteDefault.Length; i++)
        {
            defaults.Add(remoteDefault[i].key, remoteDefault[i].value);
        }

        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
          .ContinueWithOnMainThread(task =>
          {
              FetchDataAsync();
          });
    }
    public Task FetchDataAsync()
    {
        System.Threading.Tasks.Task fetchTask =
        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
            TimeSpan.Zero);
        return fetchTask.ContinueWithOnMainThread(FetchComplete);

    }
    void FetchComplete(Task fetchTask)
    {
        var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
        switch (info.LastFetchStatus)
        {
            case Firebase.RemoteConfig.LastFetchStatus.Success:
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync();

                Debug.LogError("======= fetch success:" + Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(app).GetValue(remoteDefault[0].key).StringValue);
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Failure:
                switch (info.LastFetchFailureReason)
                {
                    case Firebase.RemoteConfig.FetchFailureReason.Error:
                        Debug.LogError("======= fetch error");

                        break;
                    case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                        Debug.LogError("======= fetch fail");

                        break;
                }
                break;
            case Firebase.RemoteConfig.LastFetchStatus.Pending:
                Debug.LogError("======= fetch peding");

                break;
        }
        if (fetchTask.IsCanceled)
        {

            Debug.LogError("======= fetch cancel");
        }
        else if (fetchTask.IsFaulted)
        {

            Debug.LogError("======= fetch fault");
        }
        else if (fetchTask.IsCompleted)
        {
            Debug.LogError("======= fetch complete:" + Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(app).GetValue(remoteDefault[0].key).StringValue);
        }
        DataParam.timeDelayShowAds = float.Parse(Firebase.RemoteConfig.FirebaseRemoteConfig.GetInstance(app).GetValue(remoteDefault[0].key).StringValue);
        Debug.LogError("============ time delay show ads:" + DataParam.timeDelayShowAds);
        //    DataParam.loadUIDone = true;
    }

    public static void SUM_VIDEO_SHOW_NAME(string value)
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("video_show_all_game_para", "video_showed_" + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("video_show_all_game", param);
        }
    }

    public static void SUM_INTER_ALL_GAME()
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("inter_show_all_game_para", "inter_show_all_game_value");
            Firebase.Analytics.FirebaseAnalytics.LogEvent("inter_show_all_game", param);
        }
    }
    public static void SUM_AOA_ALL_GAME()
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("aoa_show_all_game_para", "aoa_show_all_game_value");
            Firebase.Analytics.FirebaseAnalytics.LogEvent("aoa_show_all_game", param);
        }
    }

    static string nameTempParam;
    public static void PLAY_LEVEL_DELETE_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }

            // Parameter param = new Parameter("play_level_para", "play_level_" + nameTempParam + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("play_level_delete_event_" + nameTempParam + value/*, param*/);
        }
    }
    public static void PLAY_LEVEL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }

           // Parameter param = new Parameter("play_level_para", "play_level_" + nameTempParam + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("play_level_event_" + nameTempParam + value/*, param*/);
        }
    }
    public static void PLAY_LEVEL_EVENT_CHALLANGE(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }

         //   Parameter param = new Parameter("play_level_para", "play_challange_" + nameTempParam + value/* + "_" + age*/);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("play_level_event_challenge_" + nameTempParam + value/*, param*/);
        }
    }
    public static void PLAY_EVENT_DAY(int value)
    {
        if (fireBaseInitDone)
        {
            //if (value < 8)
            //{
             //   Parameter param = new Parameter("play_day_para", "play_day_" + value);
                Firebase.Analytics.FirebaseAnalytics.LogEvent("Play_event_day_" + value/*, param*/);
           // }
        }
        Debug.LogError("==== day:" + value);
    }
    public static void PLAY_EVENT_WEEK(int value)
    {
        if (fireBaseInitDone)
        {
            //if (value < 15)
            //{
             //   Parameter param = new Parameter("play_week_para", "play_week_" + value);
                Firebase.Analytics.FirebaseAnalytics.LogEvent("Play_event_week_" + value/*, param*/);
           // }
        }
        Debug.LogError("==== week:" + value);
    }
    public static void WIN_LEVEL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
         //   Parameter param = new Parameter("win_level_para", "win_level_" + nameTempParam + value /*+ "_" + age*/);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("win_level_event_" + nameTempParam + value/*, param*/);
        }
      //  AF_LEVEL_ACHIEVED();
    }
    public static void WIN_LEVEL_EVENT_CHALLENGE(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
          //  Parameter param = new Parameter("win_level_para", "win_challange_" + nameTempParam + value /*+ "_" + age*/);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("win_level_event_challenge_" + nameTempParam + value/*, param*/);
        }
        //  AF_LEVEL_ACHIEVED();
    }

    public static void LOSE_LEVEL_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }

       //     Parameter param = new Parameter("lose_level_para", "lose_level_" + nameTempParam + value /*+ "_" + age*/);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("lose_level_event_" + nameTempParam + value/*, param*/);
        }
    }
    public static void LOSE_LEVEL_EVENT_CHALLENGE(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }

         //   Parameter param = new Parameter("lose_level_para", "lose_challange_" + nameTempParam + value /*+ "_" + age*/);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("lose_level_event_challenge_" + nameTempParam + value/*, param*/);
        }
    }

    public static void REMOVE_ADS(int value)
    {
        if (fireBaseInitDone)
        {
            if (value < 10)
            {
                nameTempParam = "00";
            }
            else if (value >= 10 && value < 100)
            {
                nameTempParam = "0";
            }
            else
            {
                nameTempParam = "";
            }
            Parameter param = new Parameter("remove_ads_para", "removeads" /*"remove_ads_level_" + nameTempParam + value*/);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("remove_ads", param);
        }

        REMOVEADS_APPFLYER();
    }
    public static void FLOW_FIRST_OPEN(string value)//chua
    {
        if (!first_open)
            return;
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("flow_first_open_para", value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("flow_first_open", param);
        }
    }
    public static void GAME_PLAY(string value)//chua
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("game_play_para", value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("game_play", param);
        }
    }
    public static void MAIN_CLICK(string value)//chua
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("main_para", value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("main_click", param);
        }
    }
    public static void SHOP_EVENT(int value)
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("skin_para","skin_" + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("shop_event", param);
        }
        Debug.LogError("skin_" + value);
    }
    public static void ARENA_EVENT_CARD(int value)
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("card_para","card_" + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("arena_event", param);
        }
    }
    public static void ARENA_EVENT_ARENA(string value)
    {
        if (fireBaseInitDone)
        {
            Parameter param = new Parameter("arena_level_para", "arena_level_" + value);
            Firebase.Analytics.FirebaseAnalytics.LogEvent("arena_event", param);
        }
    }
    public static void SHOW_INTER_APPFLYER(int value)
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("inter_show_" + value, "");
        AppsFlyer.sendEvent("inter_show_" + value, paramEvent);
    }
    public static void SHOW_VIDEO_APPFLYER(int value)
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("video_show_" + value, "");
        AppsFlyer.sendEvent("video_show_" + value, paramEvent);
    }
    public static void REMOVEADS_APPFLYER()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("remove_ads", "");
        AppsFlyer.sendEvent("remove_ads", paramEvent);
    }

    public static void AF_INTERS_AD_ELIGIBLE()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_inters_ad_eligible", "");
        AppsFlyer.sendEvent("af_inters_ad_eligible", paramEvent);
    }
    public static void AF_INTERS_API_CALLED()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_inters_api_called", "");
        AppsFlyer.sendEvent("af_inters_api_called", paramEvent);
    }
    public static void AF_INTERS_DISPLAYED()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_inters_displayed", "");
        AppsFlyer.sendEvent("af_inters_displayed", paramEvent);

        Debug.LogError("==========event inter");
    }


    public static void AF_VIDEO_AD_ELIGIBLE()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_rewarded_ad_eligible", "");
        AppsFlyer.sendEvent("af_rewarded_ad_eligible", paramEvent);
    }
    public static void AF_VIDEO_API_CALLED()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_rewarded_api_called", "");
        AppsFlyer.sendEvent("af_rewarded_api_called", paramEvent);
    }
    public static void AF_VIDEO_DISPLAYED()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_rewarded_ad_displayed", "");
        AppsFlyer.sendEvent("af_rewarded_ad_displayed", paramEvent);
    }
    public static void AF_LEVEL_ACHIEVED()
    {
        System.Collections.Generic.Dictionary<string, string> paramEvent = new
System.Collections.Generic.Dictionary<string, string>();
        paramEvent.Add("af_level_achieved", "");
        AppsFlyer.sendEvent("af_level_achieved", paramEvent);
    }
}
