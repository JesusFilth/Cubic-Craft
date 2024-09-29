using System;
using Reflex.Attributes;
using Source.Scripts.Core.GameSession;
using Source.Scripts.Core.Help.Views;
using Source.Scripts.Core.Storage.Level;
using UnityEngine;
using UnityEngine.Events;

namespace Source.Scripts.Core.Help.Events
{
    public abstract class HelpEvent : MonoBehaviour
    {
        [SerializeField] private Transform _parantUI;
        [SerializeField] private HelpWindow _helpWindow;
        [SerializeField] private LevelTypeMode _levelMode;

        public UnityEvent OnShow;

        protected bool IsShow = true;

        [Inject] private ICurrentLevelInfo _levelInfo;

        private void Awake()
        {
            GameLevelConteinerDI.Instance.InjectRecursive(gameObject);
        }

        private void OnValidate()
        {
            if (_helpWindow == null)
                throw new ArgumentNullException(nameof(_helpWindow));
        }

        public void ShowHelpWindow()
        {
            OnShow?.Invoke();
            CreateWindow();
        }

        private void CreateWindow()
        {
            Instantiate(_helpWindow, _parantUI);
        }

        protected bool IsCurrentLevelMode() => _levelMode == _levelInfo.GetLevelType();
    }
}
