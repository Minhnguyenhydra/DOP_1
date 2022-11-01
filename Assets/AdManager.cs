using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class AdManager : MonoBehaviour
{
    public List<UnityEvent> adEvents;

    int watchAdsId;

    public void HandleEarnReward() {
        //switch (watchAdRewardType) {
        //    case WatchAdRewardType.AddBonusCoin:
        //        Gameplay.Instance.AddCoin((int)Constants.WATCH_ADS_COIN);
        //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //        break;
        //    case WatchAdRewardType.RevivePlayer:
        //        ReviveManager.Instance.Revive();
        //        break;
        //}
        if (watchAdsId < adEvents.Count) {
            UnityEvent e = adEvents[watchAdsId];
            e?.Invoke();
        }
        GoogleAdMobController.Instance.RequestAndLoadRewardedAd();
    }

    public void WatchAds(int id) {
        watchAdsId = id;
        GoogleAdMobController.Instance.ShowRewardedAd();
    }

    //public void WatchAdsToRevivePlayer() {
    //    watchAdRewardType = WatchAdRewardType.RevivePlayer;
    //    GoogleAdMobController.Instance.ShowRewardedAd();
    //}

    //public void WatchAdsToReceiveBonusGold() {
    //    watchAdRewardType = WatchAdRewardType.AddBonusCoin;
    //    GoogleAdMobController.Instance.ShowRewardedAd();
    //}
}
