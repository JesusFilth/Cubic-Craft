using System.Collections.Generic;
using Agava.YandexGames;
using Reflex.Attributes;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Yandex.Shop
{
    public class Shop : MonoBehaviour, IPurchase
    {
        private readonly List<ShopProduct> _products = new List<ShopProduct>();

        [SerializeField] private Transform _conteiner;
        [SerializeField] private ShopProduct _productTemplate;

        [Inject] private IGoldStorage _goldStorage;

        private void OnEnable()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Billing.GetProductCatalog(productCatalogReponse => UpdateProductCatalog(productCatalogReponse.products));
#endif
        }

        public void AddCoins(int coins)
        {
            _goldStorage.AddGold(coins);
        }

        private void UpdateProductCatalog(CatalogProduct[] products)
        {
            foreach (ShopProduct product in _products)
                Destroy(product.gameObject);

            _products.Clear();

            foreach (CatalogProduct product in products)
            {
                ShopProduct tempProduct = Instantiate(_productTemplate, _conteiner);
                tempProduct.Init(product, this);
                _products.Add(tempProduct);
            }
        }
    }
}