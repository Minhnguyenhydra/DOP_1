using System;
using GoogleMobileAds.Api;
using UnityEngine;
using System.Collections;

public class AppOpenAdLauncher : MonoBehaviour
{
    public string ID1_ANDROID, ID2_ANDROID, ID3_ANDROID;
    public string ID1_IOS, ID2_IOS, ID3_IOS;
    public static AppOpenAdLauncher instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;


            DontDestroyOnLoad(gameObject);

        }
    }
    private void Start()
    {
        MobileAds.Initialize(status => { AppOpenAdManager.Instance.LoadAd(); });
    }
    private void OnApplicationPause(bool pause)
    {
        if (!pause && AppOpenAdManager.ConfigResumeApp && !AppOpenAdManager.ResumeFromAds)
        {
            AppOpenAdManager.Instance.ShowAdIfAvailable();
            Debug.LogError("=== show change app open ads");
        }
        Debug.LogError(pause + ":" + AppOpenAdManager.ConfigResumeApp + ":" + AppOpenAdManager.ResumeFromAds);
    }
}