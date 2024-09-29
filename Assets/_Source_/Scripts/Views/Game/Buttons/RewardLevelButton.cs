using Reflex.Attributes;
using Source.Scripts.Characters;
using Source.Scripts.Core;
using Source.Scripts.Core.Storage.User;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Game.Buttons
{
    [RequireComponent(typeof(Button))]
    public class RewardLevelButton : MonoBehaviour
    {
        [Inject] private IWallet _wallet;
        [Inject] private IGoldStorage _goldStorage;

        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRevardCallback, OnCloseCallback);
#else
            OnRevardCallback();
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
            _goldStorage.AddGold(_wallet.GetCoin());
            _wallet.AddCoin(_wallet.GetCoin());
            gameObject.SetActive(false);
        }
    }
}
