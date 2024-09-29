using System.Collections.Generic;
using Reflex.Attributes;
using Source.Scripts.Enviroment.Mineral;
using Source.Scripts.Enviroment.Truck;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.Game.Buttons
{
    [RequireComponent(typeof(CanvasGroup), typeof(Button))]
    public class RemoveAllMinerals : MonoBehaviour
    {
        private Button _button;
        private CanvasGroup _canvasGroup;

        [Inject] private IMineralView _mineralView;
        [Inject] private ITruckRemoveAllMinerals _truck;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _button = GetComponent<Button>();
            Hide();
        }

        private void OnEnable()
        {
            _mineralView.BagChanged += UpdateVisual;
            _button.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _mineralView.BagChanged -= UpdateVisual;
            _button.onClick.RemoveListener(OnClick);
        }

        private void UpdateVisual(IReadOnlyDictionary<MineralType, int> minerals)
        {
            if (minerals.Count > 0)
                Show();
            else
                Hide();
        }

        private void Show()
        {
            _canvasGroup.interactable = true;
        }

        private void Hide()
        {
            _canvasGroup.interactable = false;
        }

        private void OnClick() => _truck.ClearAllMinerals();
    }
}
