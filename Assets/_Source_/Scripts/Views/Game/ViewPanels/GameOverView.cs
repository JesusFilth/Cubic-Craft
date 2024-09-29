using Reflex.Attributes;
using Source.Scripts.Characters;
using Source.Scripts.Core.GameSession;
using Source.Scripts.Core.Storage.User;
using Source.Scripts.Sounds;
using TMPro;
using UnityEngine;

namespace Source.Scripts.Views.Game.ViewPanels
{
    [RequireComponent(typeof(CanvasGroup))]
    public abstract class GameOverView : MonoBehaviour, IGameLevelView
    {
        [SerializeField] protected TMP_Text Gold;
        [SerializeField] protected TMP_Text LevelNumber;

        [Inject] protected ICurrentLevelInfo LevellInfo;
        [Inject] protected IWallet Wallet;
        [Inject] protected IGoldStorage GoldStorage;
        [Inject] protected IFinalGameSounds Sounds;

        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        private void OnEnable()
        {
            Wallet.CoinChanged += UpdateCoin;
        }

        private void OnDisable()
        {
            Wallet.CoinChanged -= UpdateCoin;
        }

        public void Show()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            Time.timeScale = 0;
            Initialize();
        }

        public void Hide()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;

            Time.timeScale = 1;
        }

        protected abstract void Initialize();

        private void UpdateCoin(int coin)
        {
            Gold.text = coin.ToString();
        }
    }
}
