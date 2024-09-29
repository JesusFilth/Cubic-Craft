using System;
using Reflex.Attributes;
using Source.Scripts.Core.Localization;
using Source.Scripts.Core.StateMashine;
using Source.Scripts.Core.Storage.Level;
using Source.Scripts.Core.Storage.Models;
using Source.Scripts.Core.Storage.User;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.Views.MainMenu
{
    public class LevelView : MonoBehaviour
    {
        private const int MaxLevelMode = 3;
        private const string EndGameSymbol = ";)";

        [SerializeField] private TMP_Text _levelNumber;
        [SerializeField] private TMP_Text _needStars;
        [SerializeField] private TMP_Text _allStars;
        [SerializeField] private Image _levelIcon;
        [SerializeField] private GameObject _needStarsObj;

        [SerializeField] private Button _play;
        [SerializeField] private Button _next;
        [SerializeField] private Button _prev;

        [SerializeField] private GameModeButton[] _modeButtons = new GameModeButton[MaxLevelMode];

        [Inject] private ILevelStorage _userLevelStorage;
        [Inject] private ILevelInfo _levelInfo;
        [Inject] private IFindLevel _findLevel;
        [Inject] private GameStateMashine _stateMashine;
        [Inject] private LocalizationTranslate _localizationTranslate;
        [Inject] private MessageBox _messageBox;

        private LevelTypeMode _currentTypeMode;
        private LevelModel _currentLevelMode;
        private int _currentLevelIndex = 0;

        private void OnEnable()
        {
            _play.onClick.AddListener(PlayGame);
            _next.onClick.AddListener(OnClickNext);
            _prev.onClick.AddListener(OnClickPrevios);

            for (int i = 0; i < _modeButtons.Length; i++)
                _modeButtons[i].Changed += ChangeTypeMode;

            Initialize();
        }

        private void OnDisable()
        {
            _play.onClick.RemoveListener(PlayGame);
            _next.onClick.RemoveListener(OnClickNext);
            _prev.onClick.RemoveListener(OnClickPrevios);

            for (int i = 0; i < _modeButtons.Length; i++)
                _modeButtons[i].Changed -= ChangeTypeMode;
        }

        private void OnValidate()
        {
            if (_modeButtons.Length != MaxLevelMode)
                Array.Resize(ref _modeButtons, MaxLevelMode);

            try
            {
                Validate();
            }
            catch (Exception ex)
            {
                enabled = false;
                throw ex;
            }
        }

        public void Initialize()
        {
            _allStars.text = _userLevelStorage.GetAllStars().ToString();

            InitCurrentLevel();
            ShowCurrentLevel();
        }

        public void PlayGame()
        {
            if (_currentLevelMode.IsOpen == false)
            {
                _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.NeedMoreStars));
                return;
            }

            if ((int)_currentTypeMode > (int)_currentLevelMode.OpenMode)
            {
                _messageBox.Show(_localizationTranslate.GetMessage(LocalizationMessageType.LevelModeClose));
                return;
            }

            if (_findLevel.TryGetLevel(_currentLevelMode.Id, _currentTypeMode, out LevelMode level))
            {
                _stateMashine.EnterIn<LoadGameSceneState, LevelMode>(level);
            }
        }

        private void Validate()
        {
            if (_levelNumber == null)
                throw new ArgumentNullException(nameof(_levelNumber));

            if (_needStars == null)
                throw new ArgumentNullException(nameof(_needStars));

            if (_play == null)
                throw new ArgumentNullException(nameof(_play));

            if (_next == null)
                throw new ArgumentNullException(nameof(_next));

            if (_prev == null)
                throw new ArgumentNullException(nameof(_prev));
        }

        private void InitCurrentLevel()
        {
            _currentLevelMode = _userLevelStorage.GetLastOpenLevel();

            if (_currentLevelMode == null)
                throw new ArgumentNullException(nameof(_currentTypeMode));

            _currentLevelIndex = _currentLevelMode.Id;
        }

        private void ChangeTypeMode(LevelTypeMode mode)
        {
            _currentTypeMode = mode;
            ChangeFocusMode(mode);
        }

        private void InitButtonMods()
        {
            ChangeCompletedMode();
            ChangeFocusMode(_currentLevelMode.OpenMode);
        }

        private void ChangeFocusMode(LevelTypeMode mode)
        {
            for (int i = 0; i < _modeButtons.Length; i++)
                _modeButtons[i].OnFocused(_currentLevelMode.IsOpen && mode == _modeButtons[i].Type);
        }

        private void ChangeCompletedMode()
        {
            for (int i = 0; i < _modeButtons.Length; i++)
                _modeButtons[i].OnCompleted(i < _currentLevelMode.Stars);
        }

        private void OnClickNext()
        {
            _currentLevelIndex++;
            ShowCurrentLevel();
        }

        private void OnClickPrevios()
        {
            _currentLevelIndex--;
            ShowCurrentLevel();
        }

        private void ShowCurrentLevel()
        {
            _currentLevelMode = _userLevelStorage.GetLevel(_currentLevelIndex);

            if (_currentLevelMode == null)
                throw new ArgumentNullException(nameof(_currentLevelMode));

            if (_currentLevelMode.IsOpen)
            {
                _needStarsObj.SetActive(false);
            }
            else
            {
                _needStarsObj.SetActive(true);
                _needStars.text = _currentLevelMode.NeedStarForOpen.ToString();
            }

            if (_currentLevelMode.IsEndGame)
                _levelNumber.text = EndGameSymbol;
            else
                _levelNumber.text = $"{_currentLevelMode.Id + 1}";

            _currentTypeMode = _currentLevelMode.OpenMode;
            _levelIcon.sprite = _levelInfo.GetIcon(_currentLevelIndex);

            InitButtonMods();
            CheckShowNavigationBtn();
        }

        private void CheckShowNavigationBtn()
        {
            if ((_currentLevelIndex + 1) == _userLevelStorage.GetLevelCount())
                _next.gameObject.SetActive(false);
            else
                _next.gameObject.SetActive(true);

            if ((_currentLevelIndex - 1) == -1)
                _prev.gameObject.SetActive(false);
            else
                _prev.gameObject.SetActive(true);
        }
    }
}