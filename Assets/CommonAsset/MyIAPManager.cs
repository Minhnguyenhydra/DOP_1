using UnityEngine;
using UnityEngine.Purchasing;
using System;

namespace DarkcupGames {
    public class MyIAPManager : IStoreListener {

        private IStoreController controller;
        private IExtensionProvider extensions;

        //public PopupConfirm popupConfirm;

        public static string currentBuySKU;

        public Action onProcessSuccess;

        public void Init() {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

            //if (Application.platform == RuntimePlatform.Android) {
            //    builder = ConfigurationBuilder.Instance(
            //        .);
            //} else {
            //    builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            //}

            //var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct("100_gold_coins", ProductType.Consumable, new IDs
            {
            {"100_gold_coins_google", GooglePlay.Name},
            {"100_gold_coins_mac", MacAppStore.Name}
        });

            UnityPurchasing.Initialize(this, builder);
        }

        public void Init(string sku) {
            var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
            builder.AddProduct(sku, ProductType.Consumable);

            UnityPurchasing.Initialize(this, builder);
        }

        /// <summary>
        /// Called when Unity IAP is ready to make purchases.
        /// </summary>
        public void OnInitialized(IStoreController controller, IExtensionProvider extensions) {
            this.controller = controller;
            this.extensions = extensions;

            //if (popupConfirm)
            //popupConfirm.ShowOK("Init Finished", "Init IAP finished, congratulation!!");
            Debug.Log("Init IAP finished, congratulation!!");
        }

        /// <summary>
        /// Called when Unity IAP encounters an unrecoverable initialization error.
        ///
        /// Note that this will not be called if Internet is unavailable; Unity IAP
        /// will attempt initialization until it becomes available.
        /// </summary>
        public void OnInitializeFailed(InitializationFailureReason error) {
            //if (popupConfirm)
            //    popupConfirm.ShowOK("Init Failed", error.ToString());
        }

        /// <summary>
        /// Called when a purchase completes.
        ///
        /// May be called at any time after OnInitialized().
        /// </summary>
        public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e) {
            Debug.Log("Buy product complete, congratulation!!");
            Debug.Log(e);
            //if (popupConfirm)
            //    popupConfirm.ShowOK("Buy completed", "Buy product complete, congratulation!!");

            if (onProcessSuccess != null)
            {
                onProcessSuccess();
            }

            return PurchaseProcessingResult.Complete;
        }

        /// <summary>
        /// Called when a purchase fails.
        /// </summary>
        public void OnPurchaseFailed(Product i, PurchaseFailureReason p) {
            //Debug.LogError("Purchase failed at product " + i + " for reason: " + p);

            //popupConfirm.ShowOK("Buy failed", "Purchase failed at product " + i + " for reason: " + p);
            //if (popupConfirm)
            //    popupConfirm.ShowOK("Buy failed", "Purchase failed at product ");
        }

        public void ShowAllProduct() {
            foreach (var product in controller.products.all) {
                Debug.Log(product.metadata.localizedTitle);
                Debug.Log(product.metadata.localizedDescription);
                Debug.Log(product.metadata.localizedPriceString);
            }
        }

        // Example method called when the user presses a 'buy' button
        // to start the purchase process.
        public void OnPurchaseClicked(string productId) {
            //if (popupConfirm)
            //    popupConfirm.ShowOK("Processing", productId);
            controller.InitiatePurchase(productId);
            currentBuySKU = productId;
        }
    }
}
