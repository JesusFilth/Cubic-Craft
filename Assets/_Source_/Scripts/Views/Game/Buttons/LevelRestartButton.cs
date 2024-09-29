using Reflex.Attributes;
using Source.Scripts.Core.StateMashine;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Game.Buttons
{
    [RequireComponent(typeof(Button))]
    public class LevelRestartButton : MonoBehaviour
    {
        [Inject] private GameStateMashine _stateMashine;

        private Button _button;

        private void Awake() => _button = GetComponent<Button>();

        private void OnEnable() => _button.onClick.AddListener(OnClick);

        private void OnDisable() => _button.onClick.RemoveListener(OnClick);

        private void OnClick() => _stateMashine.EnterIn<LoadGameSceneState>();
    }
}
