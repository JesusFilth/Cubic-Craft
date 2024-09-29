using System;
using Source.Scripts.Core.Help.Events;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Core.Help.Clue
{
    [RequireComponent(typeof(CanvasGroup))]
    public class ClueView : MonoBehaviour
    {
        [SerializeField] private Button _clickBtn;

        private HelpEvent _currentHelpEvent;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();

            HideCanvasGroup();
        }

        private void OnValidate()
        {
            if (_clickBtn == null)
                throw new ArgumentNullException(nameof(_clickBtn));
        }

        private void OnEnable()
        {
            _clickBtn.onClick.AddListener(OnClick);
        }

        private void OnDisable()
        {
            _clickBtn.onClick.RemoveListener(OnClick);
        }

        public void Show(HelpEvent helpEvent)
        {
            _currentHelpEvent = helpEvent;
            ShowCanvasGroup();
        }

        public void Hide()
        {
            _currentHelpEvent = null;
            HideCanvasGroup();
        }

        private void ShowCanvasGroup()
        {
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        private void HideCanvasGroup()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
        }

        private void OnClick() => _currentHelpEvent.ShowHelpWindow();
    }
}
