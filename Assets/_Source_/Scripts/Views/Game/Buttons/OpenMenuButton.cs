using Reflex.Attributes;
using Source.Scripts.Views.Game.InterfaceStateMashine;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Game.Buttons
{
    [RequireComponent(typeof(Button))]
    public class OpenMenuButton : MonoBehaviour
    {
        [Inject] private UIStateMashine _gameUI;

        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick() => _gameUI.EnterIn<GameMenuUIState>();
    }
}