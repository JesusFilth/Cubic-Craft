using Reflex.Attributes;
using Source.Scripts.Characters;
using Source.Scripts.Core.Storage.User;
using UnityEngine;

namespace Source.Scripts.Views.Game.ViewPanels
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WinWindowView : GameOverView
    {
        [Inject] private ILevelStorage _levelStorage;
        [Inject] private IWallet _wallet;

        protected override void Initialize()
        {
            Sounds.Win();

            _wallet.AddCoin(LevellInfo.GetPrice());
            Gold.text = _wallet.GetCoin().ToString();
            LevelNumber.text = LevellInfo.GetLevelNumber().ToString();

            _levelStorage.AddStar(LevellInfo.GetLevelNumber() - 1, LevellInfo.GetLevelType());

            GoldStorage.AddGold(_wallet.GetCoin());
        }
    }
}
