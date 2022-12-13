using System;
using System.Collections;
using GoogleMobileAds.Api;
using UnityEngine;

public class AppOpenAdManager
{




    private string ID_TIER_1 = "TIER_1_HERE";
    private string ID_TIER_2 = "TIER_2_HERE";
    private string ID_TIER_3 = "TIER_3_HERE";


    private static AppOpenAdManager instance;

    private AppOpenAd ad;

    private DateTime loadTime;

    private bool isShowingAd = false;

    private bool showFirstOpen = false;

    public static bool ConfigOpenApp = true;
    public static bool ConfigResumeApp = true;

    public static bool ResumeFromAds = false;

    public static AppOpenAdManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new AppOpenAdManager();
            }

            return instance;
        }
    }

    private bool IsAdAvailable => ad != null && (System.DateTime.UtcNow - loadTime).TotalHours < 4;

    private int tierIndex = 1;

    public void LoadAd()
    {
        if (Datacontroller.instance.anAds)
            return;
        // if (IAP_AD_REMOVED)
        //     return;

#if UNITY_ANDROID
        ID_TIER_1 = AppOpenAdLauncher.instance.ID1_ANDROID;
        ID_TIER_2 = AppOpenAdLauncher.instance.ID2_ANDROID;
        ID_TIER_3 = AppOpenAdLauncher.instance.ID3_ANDROID;
#else
        ID_TIER_1 =AppOpenAdLauncher.instance. ID1_IOS;
        ID_TIER_2 = AppOpenAdLauncher.instance.ID2_IOS;
        ID_TIER_3 =AppOpenAdLauncher.instance. ID3_IOS;
#endif

        LoadAOA();
    }

    public void LoadAOA()
    {
        string id = ID_TIER_1;
        if (tierIndex == 2)
            id = ID_TIER_2;
        else if (tierIndex == 3)
            id = ID_TIER_3;

        Debug.Log("====Start request Open App Ads Tier " + tierIndex);

        AdRequest request = new AdRequest.Builder().Build();

        if (AdsController.instance.testAdAppOpen)
        {
            RequestConfiguration configuration = new RequestConfiguration.Builder().SetTestDeviceIds(AdsController.instance.testDeviceIds).build();
            MobileAds.SetRequestConfiguration(configuration);
            Debug.LogError("===config:" + configuration);
        }

        AppOpenAd.LoadAd(id, ScreenOrientation.Portrait, request, ((appOpenAd, error) =>
        {
            if (error != null)
            {
                // Handle the error.
                Debug.LogErrorFormat("======Failed to load the ad. (reason: {0}), tier {1}", error.LoadAdError.GetMessage(), tierIndex);
                tierIndex++;
                if (tierIndex <= 3)
                    LoadAOA();
                else
                    tierIndex = 1;
                return;
            }

            // App open ad is loaded.
            ad = appOpenAd;
            tierIndex = 1;
            loadTime = DateTime.UtcNow;
            //Debug.LogError("=======load open ads:" + showFirstOpen + ":" + ConfigOpenApp);
            //if (!showFirstOpen && ConfigOpenApp)
            //{
            //    ShowAdIfAvailable();
            //    showFirstOpen = true;
            //}
        }));
    }

    public void ShowAdIfAvailable()
    {
#if !UNITY_EDITOR
           Debug.LogError("======available:" + IsAdAvailable + ":" + isShowingAd + ":" + ad + ":" + (System.DateTime.UtcNow - loadTime).TotalHours);
        if (Datacontroller.instance.anAds)
            return;
        if (!IsAdAvailable || isShowingAd || Datacontroller.instance.saveData.removeAds)
        {
            return;
        }

        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;

        ad.Show();

     //   AdsController.instance.HideBanner();

        Debug.LogError("===== show open ads");
#endif

    }

    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args)
    {
        Debug.Log("===== Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
      //  AdsController.instance.ShowBanner();
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args)
    {
        Debug.LogFormat("====Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        LoadAd();
    //    AdsController.instance.ShowBanner();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args)
    {
        Debug.LogError("===Displayed app open ad");
        isShowingAd = true;
        EventController.SUM_AOA_ALL_GAME();
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args)
    {
        Debug.Log("===Recorded ad impression");
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args)
    {
        Debug.LogFormat("=====Received paid event. (currency: {0}, value: {1}",
            args.AdValue.CurrencyCode, args.AdValue.Value);
    }
}