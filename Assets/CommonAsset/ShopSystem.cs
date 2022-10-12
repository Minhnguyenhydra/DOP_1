using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace DarkcupGames {
    public enum CurrencyType {
        Gold, Diamond
    }

    [System.Serializable]
    public class ShopData {
        public int price;
        public CurrencyType currencyType = CurrencyType.Gold;
        public UnityEvent buyComplete;
    }

    public class ShopSystem : MonoBehaviour {
        public List<ShopData> shopDatas;
        public List<UnityEvent> watchAdCompletes;

        public void Buy(int id) {
            if (id >= shopDatas.Count) return;

            ShopData data = shopDatas[id];

            switch (data.currencyType) {
                case CurrencyType.Gold:
                    if (GameSystem.userdata.gold < data.price) {
                        Debug.Log("You have not enough money!!");
                        return;
                    }
                    GameSystem.userdata.gold -= data.price;
                    GameSystem.SaveUserDataToLocal();
                    data.buyComplete.Invoke();
                    break;
                case CurrencyType.Diamond:
                    if (GameSystem.userdata.diamond < data.price) {
                        Debug.Log("You have not enough money!!");
                        return;
                    }
                    GameSystem.userdata.diamond -= data.price;
                    GameSystem.SaveUserDataToLocal(); 
                    data.buyComplete.Invoke();
                    break;
            }
        }

        public void WatchAds(int id) {
            if (id >= watchAdCompletes.Count) return;

            watchAdCompletes[id].Invoke();
        }
    }
}
