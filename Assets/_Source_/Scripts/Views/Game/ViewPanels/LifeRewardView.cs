using Reflex.Attributes;
using Source.Scripts.Characters.Player;
using Source.Scripts.Core;
using Source.Scripts.Views.Game.InterfaceStateMashine;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Game.ViewPanels
{
    [RequireComponent(typeof(CanvasGroup))]
    public class LifeRewardView : MonoBehaviour, IGameLevelView
    {
        [SerializeField] private Button _rewardBtn;
        [SerializeField] private Button _continueBtn;

        private CanvasGroup _canvasGroup;
        private bool _isHasLife;

        [Inject] private UIStateMashine _gameUI;
        [Inject] private IPlayerStats _player;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        private void OnEnable()
        {
            _rewardBtn.onClick.AddListener(ShowReward);
            _continueBtn.onClick.AddListener(ToContinue);
        }

        private void OnDisable()
        {
            _rewardBtn.onClick.RemoveListener(ShowReward);
            _continueBtn.onClick.RemoveListener(ToContinue);
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            Time.timeScale = 1;
        }

        public void Show()
        {
            if (_isHasLife)
            {
                _gameUI.EnterIn<LoseWindowUIState>();
                return;
            }

            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            Time.timeScale = 0;
        }

        private void ShowReward()
        {
#if UNITY_WEBGL && !UNITY_EDITOR
        Agava.YandexGames.VideoAd.Show(OnOpenCallback, OnRevardCallback, OnCloseCallback);
#else
            OnRevardCallback();
#endif
        }

        private void ToContinue()
        {
            _gameUI.EnterIn<LoseWindowUIState>();
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
            _isHasLife = true;
            _player.Resurrect();

            _gameUI.EnterIn<GameLevelUIState>();
        }
    }
}
