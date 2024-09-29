using System;
using System.Collections;
using Agava.YandexGames;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Yandex.Shop
{
    public class ShopProduct : MonoBehaviour
    {
        [SerializeField] private RawImage _icon;
        [SerializeField] private TMP_Text _price;
        [SerializeField] private TMP_Text _coins;
        [SerializeField] private Button _buy;

        private CatalogProduct _product;
        private IPurchase _purchase;

        private void OnEnable()
        {
            _buy.onClick.AddListener(OnPurchaseButtonClick);
        }

        private void OnDisable()
        {
            _buy.onClick.RemoveListener(OnPurchaseButtonClick);
        }

        public void Init(CatalogProduct product, IPurchase purchase)
        {
            _purchase = purchase;

            _product = product;
            _price.text = product.price;
            _coins.text = GetCoinCount().ToString();

            if (Uri.IsWellFormedUriString(_product.imageURI, UriKind.Absolute))
                StartCoroutine(DownloadAndSetProductImage(_product.imageURI));
        }

        private IEnumerator DownloadAndSetProductImage(string imageUrl)
        {
            var remoteImage = new RemoteImage(imageUrl);
            remoteImage.Download();

            while (!remoteImage.IsDownloadFinished)
                yield return null;

            if (remoteImage.IsDownloadSuccessful)
                _icon.texture = remoteImage.Texture;
        }

        private void OnPurchaseButtonClick()
        {
            Billing.PurchaseProduct(_product.id, (purchaseProductResponse) =>
            {
                Debug.Log($"Purchased {purchaseProductResponse.purchaseData.productID}");

                Billing.ConsumeProduct(purchaseProductResponse.purchaseData.purchaseToken, () =>
                {
                    Debug.Log($"Consumed {purchaseProductResponse.purchaseData.productID}");
                    _purchase.AddCoins(GetCoinCount());
                });
            });
        }

        private int GetCoinCount()
        {
            const int CoinUpForId = 10;

            string[] paths = _product.id.Split('_');
            int coins = int.Parse(paths[1]);

            return coins * CoinUpForId;
        }
    }
}
