using System;
using Reflex.Attributes;
using Source.Scripts.Sounds;
using UnityEngine;

namespace Source.Scripts.Views.Game.ViewPanels
{
    [RequireComponent(typeof(CanvasGroup))]
    public class CompletedSceneView : MonoBehaviour, IGameLevelView
    {
        [SerializeField] private GameObject _mainCamera;
        [SerializeField] private GameObject _vicrorySceneCamera;
        [SerializeField] private GameObject _playerHealthView;
        [SerializeField] private GameObject _truckConteiner;

        private CanvasGroup _canvasGroup;

        [Inject] private IFinalGameSounds _finalGameSounds;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            Hide();
        }

        private void OnValidate()
        {
            if (_mainCamera == null)
                throw new ArgumentNullException(nameof(_mainCamera));

            if (_vicrorySceneCamera == null)
                throw new ArgumentNullException(nameof(_vicrorySceneCamera));

            if (_playerHealthView == null)
                throw new ArgumentNullException(nameof(_playerHealthView));

            if (_truckConteiner == null)
                throw new ArgumentNullException(nameof(_truckConteiner));
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
            _canvasGroup.alpha = 1;
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;

            _finalGameSounds.Completed();
            _playerHealthView.SetActive(false);
            _truckConteiner.SetActive(false);

            Time.timeScale = 0;

            ShowVictoryScene();
        }

        private void ShowVictoryScene()
        {
            _mainCamera.SetActive(false);
            _vicrorySceneCamera.SetActive(true);
        }
    }
}
