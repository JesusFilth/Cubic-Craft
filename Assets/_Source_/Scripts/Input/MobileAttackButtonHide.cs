using Reflex.Attributes;
using UnityEngine;

namespace Source.Scripts.Input
{
    [RequireComponent(typeof(CanvasGroup))]
    public class MobileAttackButtonHide : MonoBehaviour
    {
        [Inject] private IPlayerNearEnemys _nearEnemys;

        private CanvasGroup _canvasGroup;
        private bool _isActive;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        private void Update()
        {
            ChangeActive(_nearEnemys.IsNear());
        }

        private void ChangeActive(bool isActive)
        {
            if (isActive == _isActive)
                return;

            _isActive = isActive;

            if (_isActive)
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
    }
}