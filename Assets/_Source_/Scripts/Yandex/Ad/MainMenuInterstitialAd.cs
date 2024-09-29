using Source.Scripts.Core;
using UnityEngine;

namespace Source.Scripts.Yandex.Ad
{
    public class MainMenuInterstitialAd : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        InterstitialAd.Show(OnOpenAdCallback, OnCloseAdCallback);
#endif
        }

        private void OnOpenAdCallback()
        {
            FocusGame.Instance.Lock();
        }

        private void OnCloseAdCallback(bool wasShown)
        {
            FocusGame.Instance.Unlock();
        }
    }
}
