using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsController : MonoBehaviour
{
    public bool testAdAppOpen;
    public static AdsController instance;
    Datacontroller _dataController;
   // MusicController soundController;
    public List<String> testDeviceIds = new List<string>();

    public string appId;
    public string appIdIOS;

    public string bannerIdAndroid;
    public string interIdAndroid;
    public string videoIdAndroid;
    public string nativeAdsAndroid;
  //  public string openAdsAndroid;

    public string bannerIdIOS;
    public string interIdIOS;
    public string videoIdIOS;
    public string nativeAdsIOS;
   // public string openAdsIOS;

    string bannerId;
    string interId;
    string videoId;
    string nativeId;
    string openAdsId;

    bool loadbannerDone;
    bool doneWatchAds = false;


    //private AppOpenAd ad;


    //private bool IsOpenAdAvailable
    //{
    //    get
    //    {
    //        return ad != null /*&& (System.DateTime.UtcNow - loadTimeOpenads).TotalHours < 4*/;
    //    }
    //}
    //public static bool checkLoadOpenAds = false;
    //AdRequest request;
    //public void LoadOpenAd()
    //{

    //    request = new AdRequest.Builder().Build();
    //    // Load an app open ad for portrait orientation
    //    AppOpenAd.LoadAd(openAdsId, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
    //    {
    //        if (error != null)
    //        {
    //            // Handle the error.
    //            checkLoadOpenAds = true;
    //            Debug.LogError("========== load open ads false 1:" + error.LoadAdError.GetMessage());

    //            return;
    //        }

    //        // App open ad is loaded.
    //        ad = appOpenAd;
    //        ad.OnAdDidDismissFullScreenContent += HandleCloseOpenAds;
    //        ad.OnAdFailedToPresentFullScreenContent += HandleLoadFalseOpenAds;
    //        ad.OnAdDidPresentFullScreenContent += HandleDisplayOpenAds;
    //        //  ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
    //        //ad.OnPaidEvent += HandlePaidEvent;
    //        checkLoadOpenAds = true;
    //        //    loadTimeOpenads = DateTime.UtcNow;

    //        Debug.LogError("====== load success openads:" + openAdsId);
    //    }));
    //    //   Debug.LogError("==========func load open ads:" + openAdsId);
    //}

    //public void ShowOpenAdsAfterLoading()
    //{
    //    if (_dataController.anAds)
    //        return;


    //    if (_dataController.saveData.session >= 1)
    //    {
    //        if (IsOpenAdAvailable)
    //        {

    //            //ad.OnAdDidDismissFullScreenContent += HandleCloseOpenAds;
    //            //ad.OnAdFailedToPresentFullScreenContent += HandleLoadFalseOpenAds;
    //            //ad.OnAdDidPresentFullScreenContent += HandleDisplayOpenAds;
    //            //ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
    //            //ad.OnPaidEvent += HandlePaidEvent;

    //            ad.Show();

    //            DataParam.afterShowAds = true;
    //            Debug.LogError("====== show open ads after loading");
    //        }

    //    }

    //}

    //public void ShowOpenAdsAfterChangeApp()
    //{
    //    if (!DataParam.canshowopenadsafterchangeapp)
    //        return;
    //    if (_dataController.anAds)
    //        return;

    //    if (Application.loadedLevelName == "Loading")
    //        return;
    //    //if (_dataController.saveData.session >= 1)
    //    //{
    //    if (IsOpenAdAvailable)
    //    {

    //        //ad.OnAdDidDismissFullScreenContent += HandleCloseOpenAds;
    //        //ad.OnAdFailedToPresentFullScreenContent += HandleLoadFalseOpenAds;
    //        //ad.OnAdDidPresentFullScreenContent += HandleDisplayOpenAds;
    //        //ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
    //        //ad.OnPaidEvent += HandlePaidEvent;

    //        ad.Show();

    //        DataParam.afterShowAds = true;
    //        Debug.LogError("====== show open ads after change app");
    //    }
    //    else
    //    {
    //        Debug.LogError("===== null open ads");
    //    }

    //    //}

    //}
    //private void HandleCloseOpenAds(object sender, EventArgs args)
    //{
    //    Debug.LogError("=====Closed app open ad");
    //    // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
    //    ad = null;
    //    LoadOpenAd();

    //    DataParam.beginShowInter = System.DateTime.Now;
    //    ShowBanner();
    //}

    //private void HandleLoadFalseOpenAds(object sender, AdErrorEventArgs args)
    //{
    //    Debug.LogError("========== load open ads false 2:" + args.AdError.GetMessage());
    //    // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
    //    ad = null;
    //    // LoadOpenAd();
    //}

    //private void HandleDisplayOpenAds(object sender, EventArgs args)
    //{
    //    Debug.LogError("======Displayed app open ad");
    //    HideBanner();
    //}

    //private void HandleAdDidRecordImpression(object sender, EventArgs args)
    //{
    //    Debug.LogError("======Recorded ad impression");//ghi lại số lần hiển thị
    //}

    //private void HandlePaidEvent(object sender, AdValueEventArgs args)
    //{
    //    Debug.LogFormat("======Received paid event. (currency: {0}, value: {1}",
    //            args.AdValue.CurrencyCode, args.AdValue.Value);//nhận sự kiện trả tiền
    //}

    private void Start()
    {
        if (_dataController == null)
        {
            _dataController = Datacontroller.instance;
          //  soundController = MusicController.instance;


#if UNITY_ANDROID
            appIdTemp = appId;
            bannerId = bannerIdAndroid;
            interId = interIdAndroid;
            videoId = videoIdAndroid;
            //  openAdsId = openAdsAndroid;
#elif UNITY_IOS
                appIdTemp = appIdIOS;
                bannerId = bannerIdIOS;
                interId = interIdIOS;
                videoId = videoIdIOS;
            //    openAdsId = openAdsIOS;
#endif


            //MobileAds.Initialize(initStatus =>
            //{


            //    Debug.LogError("=====zooooooo day init mobile ads");
            //});

            //   LoadOpenAd();
            if (Datacontroller.instance.anAds)
                return;
            Init();
        }
    }
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(gameObject);

        }
    }
    string appIdTemp;
    public void Init()
    {

        MaxSdk.SetSdkKey(appIdTemp);
        MaxSdk.SetUserId("USER_ID");
        MaxSdk.InitializeSdk();
        MaxSdkCallbacks.OnSdkInitializedEvent += (MaxSdkBase.SdkConfiguration sdkConfiguration) =>
        {
            // AppLovin SDK is initialized, start loading ads
            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeBannerAds();
            //  InitializeMRecAds();
        };



        //IronSource.Agent.validateIntegration();

        //IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent;
        //IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
        //IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

        //IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent;
        //IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

        //IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;

        //IronSource.Agent.init(appIdTemp);

        //RequestInter();
        //RequestVideo();
        //RequestBanner();
    }
    public void InitializeMRecAds()
    {
        MaxSdkCallbacks.OnMRecAdLoadedEvent += OnNativeAdsLoaded;
        MaxSdkCallbacks.OnMRecAdLoadFailedEvent += OnNativeAdsLoadedFalse;

        MaxSdk.CreateMRec(nativeId, MaxSdkBase.AdViewPosition.BottomCenter);
    }

    private void OnNativeAdsLoadedFalse(string arg1, int arg2)
    {
        Debug.LogError("=============== native load false");
    }

    private void OnNativeAdsLoaded(string obj)
    {
        Debug.LogError("=============== native load sucess");
    }

    public void InitializeBannerAds()
    {
        MaxSdkCallbacks.OnBannerAdLoadedEvent += BannerAdLoadedEvent;
        MaxSdkCallbacks.OnBannerAdLoadFailedEvent += BannerAdLoadedEventFalse;

#if UNITY_EDITOR

#else
            // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
            // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
            MaxSdk.CreateBanner(bannerId, MaxSdkBase.BannerPosition.BottomCenter);

            // Set background or background color for banners to be fully functional
            MaxSdk.SetBannerBackgroundColor(bannerId, colorBanner);
#endif
    }
    public Color colorBanner;
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;
        MaxSdkCallbacks.OnInterstitialDisplayedEvent += OnInterstitialDisplayEvent;
        // Load the first interstitial
        RequestInter();
    }



    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardDisplayEvent;

        // Load the first rewarded ad
        RequestVideo();
    }



    int retryAttemptVideo;
    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.

        // Reset retry attempt
        retryAttemptVideo = 0;
        EventController.AF_VIDEO_API_CALLED();

        Debug.LogError("========= video load sucess");
    }
    private void OnRewardDisplayEvent(string adUnitId)
    {
        EventController.AF_VIDEO_DISPLAYED();

    }
    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).

        //retryAttemptVideo++;
        //double retryDelay = Math.Pow(2, Math.Min(6, retryAttemptVideo));

        //Invoke("LoadRewardedAd", (float)retryDelay);

        Debug.LogError("========= video load false");
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        Time.timeScale = 1;
        //soundController.ChangeSettingMusic();
        //soundController.ChangeSettingSound();

        //   RequestVideo();
    }


    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        AppOpenAdManager.ResumeFromAds = false;
        //soundController.ChangeSettingMusic();
        //soundController.ChangeSettingSound();
        StartCoroutine(delayAction());
        // RequestVideo();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward)
    {
        doneWatchAds = true;
        // The rewarded ad displayed and the user should receive the reward.
    }


    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'

        // Reset retry attempt
        retryAttemptInter = 0;
        EventController.AF_INTERS_API_CALLED();

        Debug.LogError("========= inter load sucess");
    }
    int retryAttemptInter;
    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)

        //retryAttemptInter++;
        //double retryDelay = Math.Pow(2, Math.Min(6, retryAttemptInter));

        //Invoke("LoadInterstitial", (float)retryDelay);

        Debug.LogError("========= inter load false");
    }
    private void OnInterstitialDisplayEvent(string adUnitId)
    {
        EventController.AF_INTERS_DISPLAYED();
        EventController.SUM_INTER_ALL_GAME();
        //    EventController.AB_INTER_ID(DataParam.showInterType);

        //DataParam.countShowInter++;
        //EventController.AB_INTER(DataParam.countShowInter);
        //if (DataParam.countShowInter % 5 == 0)
        //{
        //    EventController.SHOW_INTER_APPFLYER(DataParam.countShowInter);

        //}

        //soundController.MuteAllMusic();
        //soundController.MuteAllSound();

        Time.timeScale = 0;

        Debug.LogError("=== displayed inter");

    }
    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.

        //soundController.ChangeSettingMusic();
        //soundController.ChangeSettingSound();
        Time.timeScale = 1;
        //    RequestInter();
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad.
        Time.timeScale = 1;
        //soundController.ChangeSettingMusic();
        //soundController.ChangeSettingSound();
        RequestInter();
        DataParam.beginShowInter = System.DateTime.Now;
        AppOpenAdManager.ResumeFromAds = false;
        Debug.LogError("=== bam' close inter");
    }


    public void RequestVideo()
    {
        MaxSdk.LoadRewardedAd(videoId);
    }
    public void RequestInter()
    {
        MaxSdk.LoadInterstitial(interId);
        //  IronSource.Agent.loadInterstitial();
    }

    void RequestBanner()
    {
        //    IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

    }

    //private void InterstitialAdClosedEvent()
    //{
    //    RequestInter();
    //    Time.timeScale = 1;
    //    SoundController.instance.auBG.mute = _dataController.saveData.offmusic;
    //    SoundController.instance.auResult.mute = _dataController.saveData.offsound;
    //}
    public bool bannerOK;
    private void BannerAdLoadedEvent(string adUnitId)
    {
        Debug.LogError("====load banner success ");
        bannerOK = true;
        //HideBanner();
         //ShowBanner();
    }
    private void BannerAdLoadedEventFalse(string s, int i)
    {
        Debug.LogError("====load banner false ");
        bannerOK = false;
    }

    //void InterstitialAdShowFailedEvent(IronSourceError error)
    //{
    //    Debug.Log("======load inter show Failed " + error.getDescription());
    //    Time.timeScale = 1;
    //    SoundController.instance.auBG.mute = _dataController.saveData.offmusic;
    //    SoundController.instance.auResult.mute = _dataController.saveData.offsound;
    //}

    //private void RewardedVideoAdShowFailedEvent(IronSourceError obj)
    //{
    //    Debug.LogError("=====video Show Faile");
    //    Time.timeScale = 1;
    //    SoundController.instance.auBG.mute = _dataController.saveData.offmusic;
    //    SoundController.instance.auResult.mute = _dataController.saveData.offsound;
    //}

    //private void RewardedVideoAdRewardedEvent(IronSourcePlacement obj)
    //{
    //    doneWatchAds = true;
    //}
    IEnumerator delayAction()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        if (doneWatchAds)
        {
            if (AdManager.Instance)
            {
                AdManager.Instance.HandleEarnReward();
            }
            //  acreward();
            EventController.SUM_VIDEO_SHOW_NAME(DataParam.nameEventVideo);
            Debug.LogError("=======name vide:" + DataParam.nameEventVideo);
            //if (nameEventVideo.Contains("skip"))
            //{
            //    EventController.SKIP_LEVEL_NAME(DataParam.levelHintOrSkip);
            //}
            //else if (nameEventVideo == "hint")
            //{
            //    EventController.HINT_LEVEL_NAME(DataParam.levelHintOrSkip);
            //}
            //DataParam.countShowVideo++;
            //EventController.AB_VIDEO(DataParam.countShowVideo);
            //if (DataParam.countShowVideo % 5 == 0 || DataParam.countShowVideo == 2)
            //{
            //    EventController.SHOW_VIDEO_APPFLYER(DataParam.countShowVideo);
            //}
        }
        RequestVideo();
        // Debug.LogError("====== close video");
       // acreward = null;

        doneWatchAds = false;
        Time.timeScale = 1;
    }

    private void RewardedVideoAdClosedEvent()
    {
        //soundController.ChangeSettingMusic();
        //soundController.ChangeSettingSound();
        StartCoroutine(delayAction());
    }

  //  Action acreward;

    public void ShowVideo(/*Action _ac, *//*string name*/)
    {
        if (_dataController.anAds)
        {
            if (AdManager.Instance)
            {
                AdManager.Instance.HandleEarnReward();
            }
            return;
        }

        if (/*IronSource.Agent.isRewardedVideoAvailable()*/MaxSdk.IsRewardedAdReady(videoId))
        {
            //acreward = _ac;

                doneWatchAds = false;
          //  nameEventVideo = name;
            MaxSdk.ShowRewardedAd(videoId);
            // IronSource.Agent.showRewardedVideo();
            Time.timeScale = 0;
            //soundController.MuteAllMusic();
            //soundController.MuteAllSound();

            EventController.AF_VIDEO_AD_ELIGIBLE();
            //DataParam.afterShowAds = true;
            AppOpenAdManager.ResumeFromAds = true;
            // Debug.LogError("------ video show video");
        }
        else
        {
            //   Debug.LogError("------ video chua load");
            RequestVideo();
        }
    }
    public void ShowInter()
    {
        if (_dataController.anAds)
            return;

        if (/*IronSource.Agent.isInterstitialReady()*/MaxSdk.IsInterstitialReady(interId))
        {
            //  IronSource.Agent.showInterstitial();
            MaxSdk.ShowInterstitial(interId);
            AppOpenAdManager.ResumeFromAds = true;
            // DataParam.afterShowAds = true;
            //  EventController.INTER_SHOW();
        }
        else
        {
            RequestInter();
        }
    }
    public void ShowBanner()
    {
        if (_dataController.anAds)
            return;

        if (bannerOK)
            MaxSdk.ShowBanner(bannerId);

        //if (bannerOK)
        //    IronSource.Agent.displayBanner();
        //else
        //    RequestBanner();
    }
    public void HideBanner()
    {

        if (bannerOK)
            MaxSdk.HideBanner(bannerId);
        //    IronSource.Agent.hideBanner();
    }
    public void ShowNativeAds()
    {
        //if (!Datacontroller.instance.saveData.removeAds)
        //    MaxSdk.ShowMRec(nativeId);
    }
    public void HideNativeAds()
    {
        // MaxSdk.HideMRec(nativeId);
    }

}
