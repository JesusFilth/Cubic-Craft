using Reflex.Attributes;
using Source.Scripts.Core;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Yandex.Ad
{
    public class RewardAd : MonoBehaviour
    {
        [Inject] private IGoldStorage _goldStorage;

        private int _giftCoins = 50;

        public void Show(int coins)
        {
            _giftCoins = coins;
#if UNITY_WEBGL && !UNITY_EDITOR
         Agava.YandexGames.VideoAd.Show(OnOpenCallback,OnRevardCallback, OnCloseCallback);
#endif
        }

        private void OnOpenCallback()
        {
            FocusGame.Instance.Lock();
        }

        private void OnCloseCallback()
        {
            FocusGame.Instance.Unlock();
        }

        private void OnRevardCallback()
        {
            _goldStorage.AddGold(_giftCoins);
        }
    }
}
