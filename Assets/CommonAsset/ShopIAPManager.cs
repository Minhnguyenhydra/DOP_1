using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;
using System;

namespace DarkcupGames {
    public class ShopIAPManager : MonoBehaviour {
        public PopupConfirm popupConfirm;
        public TMP_InputField inputSKU;
        public TMP_InputField inputInit;

        MyIAPManager iap;

        private void Start()
        {
            Init();
        }

        public void Init() {
            iap = new MyIAPManager();
            //iap.popupConfirm = popupConfirm;
            //iap.Init(inputInit.text);
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            builder.AddProduct("id_suriken_1", ProductType.Consumable);
            builder.AddProduct("id_suriken_2", ProductType.Consumable);
            builder.AddProduct("id_suriken_3", ProductType.Consumable);

            for (int i = 1; i <= 5; i++) {
                string sku = "sub_" + i + "_week";
                builder.AddProduct(sku, ProductType.Subscription);
            }

            for (int i = 1; i <= 5; i++) {
                string sku = "sub_" + i + "_month";
                builder.AddProduct(sku, ProductType.Subscription);
            }

            //builder.AddProduct("sub_1_week", ProductType.Subscription);
            //builder.AddProduct("sub2week", ProductType.Subscription);
            //builder.AddProduct("sub3week", ProductType.Subscription);
            //builder.AddProduct("sub4week", ProductType.Subscription);
            //builder.AddProduct("sub5week", ProductType.Subscription);
            //builder.AddProduct("sub6week", ProductType.Subscription);
            //builder.AddProduct("sub7week", ProductType.Subscription);
            //builder.AddProduct("sub8week", ProductType.Subscription);
            //builder.AddProduct("sub9week", ProductType.Subscription);
            //builder.AddProduct("sub10week", ProductType.Subscription);

            //builder.AddProduct("sub1month", ProductType.Subscription);
            //builder.AddProduct("sub2month", ProductType.Subscription);
            //builder.AddProduct("sub3month", ProductType.Subscription);
            //builder.AddProduct("sub4month", ProductType.Subscription);
            //builder.AddProduct("sub5month", ProductType.Subscription);
            //builder.AddProduct("sub6month", ProductType.Subscription);
            //builder.AddProduct("sub7month", ProductType.Subscription);
            //builder.AddProduct("sub8month", ProductType.Subscription);
            //builder.AddProduct("sub9month", ProductType.Subscription);
            //builder.AddProduct("sub10month", ProductType.Subscription);

            UnityPurchasing.Initialize(iap, builder);

            //iap.ShowAllProduct();
        }

        public void BuyProduct(string productId, Action onComplete) {
            MyIAPManager.currentBuySKU = productId;
            iap.OnPurchaseClicked(productId);
            iap.onProcessSuccess = onComplete;
        }

        public void TestBuyProduct() {
            iap.OnPurchaseClicked(inputSKU.text);
        }

        public void OnBuyComlete(string sku)
        {
            if (GameSystem.userdata.boughtItems == null)
            {
                GameSystem.userdata.boughtItems.Add(sku);
                GameSystem.SaveUserDataToLocal();
            } 
        }
    }

}
